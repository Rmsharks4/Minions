using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using test.Entry;
using System.Data;


namespace test
{
    public partial class message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            DataTable d1 = new DataTable();
            int userid = (int)Session["userid"];
            int check = obj.MsgPreview(userid, ref d1);
            if (check <= 0)
            {
                Label l = new Label();
                l.Text = "No messages to show yet";
                l.Attributes.Add("runat", "server");
                p.Controls.Add(l);
            }
            else
            {
                for (int i = 0; i < d1.Rows.Count; i++)
                {
                    DataTable temp = new DataTable();
                    int x = obj.Convo(userid, d1.Rows[i].Field<int>(0), ref temp);
                    if (x > 0)
                    {
                        LinkButton button = new LinkButton();
                        button.ID = button.UniqueID;
                        button.Attributes.Add("style", "font-size: 23px; color: white;");
                        int user2 = d1.Rows[i].Field<int>(0);
                        button.Text = obj.GetUser(user2);
                        DataTable convo = new DataTable();
                        convo = temp;
                        button.Click += (se, ev) =>
                        {
                            Session["msgid"] = user2;
                            Session["Convo"] = convo;
                            Response.Redirect("talk.aspx?val="+ user2);
                        };
                        button.Attributes.Add("runat", "server");
                        int j = 0;
                        while (temp.Rows[j].Field<DateTime>(2).Subtract(d1.Rows[i].Field<DateTime>(1)).TotalMilliseconds > 0)
                        {
                            if (temp.Rows[j].Field<int>(0) == userid)
                                break;
                            j++;
                        }
                        Label notify = new Label();
                        notify.ID = notify.UniqueID;
                        Label message = new Label();
                        message.ID = message.UniqueID;
                        message.Text = temp.Rows[0].Field<string>(1);
                        message.Attributes.Add("style", "font-size: 20px;");
                        message.Attributes.Add("runat", "server");
                        Label time = new Label();
                        time.ID = time.UniqueID;
                        time.Attributes.Add("style", "text-align: right;");
                        time.Text = "<sub>" + temp.Rows[0].Field<DateTime>(2) + "</sub>";
                        if (j > 0)
                        {
                            notify.Attributes.Add("style", "content:attr(data-badge); position: absolute; font-size:.7em; background: red; color: white; width: 18px; height: 18px; text-align:center; line-height:18px; border-radius:50%; box-shadow:0 0 1px #333;");
                            notify.Text += j;
                            p.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(255, 216, 0,0.6); color: white; \">"));
                        }
                        else
                        {
                            p.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(18, 12, 85, 0.6); color: white; \">"));
                        }
                        p.Controls.Add(button);
                        p.Controls.Add(notify);
                        p.Controls.Add(new LiteralControl("<br/>"));
                        p.Controls.Add(message);
                        p.Controls.Add(new LiteralControl("<br/>"));
                        p.Controls.Add(time);
                        p.Controls.Add(new LiteralControl("</div>"));
                        p.Controls.Add(new LiteralControl("<br/><br/>"));
                    }
                }
            }
        }
    }
}