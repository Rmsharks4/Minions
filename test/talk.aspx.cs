using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using test.Entry;
using System.Data;
using System.Web.Services;

namespace test
{

    public partial class talk : System.Web.UI.Page
    {
        private List<int> _controlIds;
        private List<string> _text;
        private List<DateTime> _time;

        private List<int> ControlIds
        {
            get
            {
                if (_controlIds == null)
                {
                    if (ViewState["ControlIds"] != null)
                        _controlIds = (List<int>)ViewState["ControlIds"];
                    else
                        _controlIds = new List<int>();
                }
                return _controlIds;
            }
            set { ViewState["ControlIds"] = value; }
        }

        private List<string> Texts
        {
            get
            {
                if (_text == null)
                {
                    if (ViewState["Texts"] != null)
                        _text = (List<string>)ViewState["Texts"];
                    else
                        _text = new List<string>();
                }
                return _text;
            }
            set { ViewState["Texts"] = value; }
        }

        private List<DateTime> Times
        {
            get
            {
                if (_time == null)
                {
                    if (ViewState["Times"] != null)
                        _time = (List<DateTime>)ViewState["Times"];
                    else
                        _time = new List<DateTime>();
                }
                return _time;
            }
            set { ViewState["Times"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = (int)Session["userid"];
            int user2 = (int)Session["msgid"];
            DataTable convo = (DataTable)Session["convo"];

            enter obj = new enter();
            int rem = obj.UpdateLastSeen(userid, user2);

            heading.Text = obj.getName(user2);
            heading.Click += (se, ev) =>
            {
                Session["uid"] = user2;
                Response.Redirect("user.aspx?val=" + user2);
            };

            if (rem == -1)
            {
                msgtxt.Text = "An error occured. Please reload the page!";
            }
            else
            {
                foreach (DataRow row in convo.Rows)
                {
                    Label message = new Label();
                    message.ID = message.UniqueID;
                    message.Text = row.Field<string>(1);
                    message.Attributes.Add("style", "font-size: 20px;");
                    message.Attributes.Add("runat", "server");
                    Label time = new Label();
                    time.ID = time.UniqueID;
                    time.Attributes.Add("style", "text-align: right;");
                    time.Text = "<sub>" + row.Field<DateTime>(2) + "</sub>";

                    if (row.Field<int>(0) == userid)
                    {
                        p.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left \">"));
                    }
                    else
                    {
                        p.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(255, 216, 0,0.6); color: white; text-align: right \">"));
                    }

                    p.Controls.Add(message);
                    p.Controls.Add(new LiteralControl("<br/>"));
                    p.Controls.Add(time);
                    p.Controls.Add(new LiteralControl("</div>"));
                    p.Controls.Add(new LiteralControl("<br/><br/>"));
                }
            }

            if (IsPostBack)
            {
                for (int i = ControlIds.Count-1; i >= 0; i--)
                {
                    Label message = new Label();
                    message.ID = "msg" + ControlIds[i];
                    message.Text = Texts[i];
                    message.Attributes.Add("style", "font-size: 20px;");
                    message.Attributes.Add("runat", "server");
                    Label time = new Label();
                    time.ID = "time" + ControlIds[i];
                    time.Attributes.Add("style", "text-align: right;");
                    time.Text = "<sub>" + Times[i] + "</sub>";
                    place.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left \">"));
                    place.Controls.Add(message);
                    place.Controls.Add(new LiteralControl("<br/>"));
                    place.Controls.Add(time);
                    place.Controls.Add(new LiteralControl("</div>"));
                    place.Controls.Add(new LiteralControl("<br/><br/>"));
                }
            }

        }

        [WebMethod]
        public static void GetData()
        {
            //convo c = new convo();
            //Message m = new Message();
            //enter e = new enter();
            //int user1 = (int)HttpContext.Current.Session["userid"];
            //int user2 = (int)HttpContext.Current.Session["msgid"];
            //DateTime time = new DateTime();
            //e.GetLastSeen(user1, user2, ref time);
            //convo.GetMsg(user2, user1, time, ref m);
            //if (m.user1id != -1)
            //{
            //    talk t = new talk();
            //    talk.testDelegate = new Action(t.RecieveMsg);
            //}
        }

        private static Action testDelegate;
     
        public void RecieveMsg()
        {
            enter obj = new enter();
            DataTable d = new DataTable();
            int suc = obj.GetLatest((int)Session["msgid"],(int)Session["userid"], ref d);

            string msg = d.Rows[0].Field<string>(0);
            DateTime time = d.Rows[0].Field<DateTime>(1);

            var cis = ControlIds;
            int id = ControlIds.Count + 1;
            cis.Add(id);
            ControlIds = cis;

            var ts = Texts;
            ts.Add(msg);
            Texts = ts;

            var dts = Times;
            dts.Add(time);
            Times = dts;

            Label message = new Label();
            message.ID = "msg" + id;
            message.Text = msg;
            message.Attributes.Add("style", "font-size: 20px;");
            message.Attributes.Add("runat", "server");
            Label t = new Label();
            t.ID = "time" + id;
            t.Attributes.Add("style", "text-align: right;");
            t.Text = "<sub>" + time + "</sub>";
            news.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(255, 216, 0, 0.6); color: white; text-align: right \">"));
            news.Controls.Add(message);
            news.Controls.Add(new LiteralControl("<br/>"));
            news.Controls.Add(t);
            news.Controls.Add(new LiteralControl("</div>"));
            news.Controls.Add(new LiteralControl("<br/><br/>"));
            t.Text = "";
        }

            protected void CreateMsgs(object sender, EventArgs e)
        {
            enter obj = new enter();
            int success = obj.InsertMessage((int)Session["userid"], (int)Session["msgid"], t.Text);
            if (success == -1)
            {
                msgtxt.Text = "Connection Failure! Try Again!";
            }
            else
            {
                var cis = ControlIds;
                int id = ControlIds.Count + 1;
                cis.Add(id);
                ControlIds = cis;

                var ts = Texts;
                ts.Add(t.Text);
                Texts = ts;

                var dts = Times;
                var now = DateTime.Now;
                dts.Add(now);
                Times = dts;

                Label message = new Label();
                message.ID = "msg" + id;
                message.Text = t.Text;
                message.Attributes.Add("style", "font-size: 20px;");
                message.Attributes.Add("runat", "server");
                Label time = new Label();
                time.ID = "time" + id;
                time.Attributes.Add("style", "text-align: right;");
                time.Text = "<sub>" + now + "</sub>";
                news.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left \">"));
                news.Controls.Add(message);
                news.Controls.Add(new LiteralControl("<br/>"));
                news.Controls.Add(time);
                news.Controls.Add(new LiteralControl("</div>"));
                news.Controls.Add(new LiteralControl("<br/><br/>"));
                t.Text = "";
            }
        }

    }
}