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
    public partial class notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            DataTable d1 = new DataTable();
            int userid = (int)Session["userid"];
            int y = obj.UpdateLastSeen_Notif(userid);
            int check = obj.GetNotif(userid, ref d1);
            if (check <= 0)
            {
                Label l = new Label();
                l.Text = "No Notifications to show"+check;
                l.Attributes.Add("runat", "server");
                p.Controls.Add(l);
            }
            else
            {
                for (int i = 0; i < d1.Rows.Count; i++)
                {
                    Label notify = new Label();
                    notify.ID = notify.UniqueID;
                    int uid = d1.Rows[i].Field<int>(0);
                    string username = obj.GetUser(uid);
                    LinkButton button1 = new LinkButton();
                    button1.ID = button1.UniqueID;
                    button1.Attributes.Add("runat","server");
                    p.Controls.Add(new LiteralControl("<div style=\"border: solid; border-color: azure; background-color: rgba(255, 216, 0,0.6); color: white; \">"));
                    notify.Attributes.Add("style", "font-size: 30px;");
                    notify.Attributes.Add("runat", "server");
                    //notify.Attributes.Add("Width", "561px");
                    button1.Attributes.Add("style", "font-size: 30px;");
                    //button1.Attributes.Add("Width", "561px");
                    int tid = d1.Rows[i].Field<int>(1);
                    if(d1.Rows[i].Field<string>(3)=="like"){
                        button1.Text ="@" + username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text += " liked your ";
                        LinkButton button2 = new LinkButton();
                        button2.ID = button2.UniqueID;
                        button2.Attributes.Add("runat", "server");
                        button2.Attributes.Add("style", "font-size: 30px;");
                      //  button2.Attributes.Add("Width", "561px");
                        button2.Text = "tweet.";
                        button2.Click += (se, ev) =>
                        {
                            Session["tweetid"] = tid;
                            Response.Redirect("tweet.aspx?val=" + uid);
                        };
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                        p.Controls.Add(button2);    
                    }
                    else if (d1.Rows[i].Field<string>(3) == "you followed"){
                        button1.Text = "@"+username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = "You started Following ";
                        
                        p.Controls.Add(notify);
                        p.Controls.Add(button1);
                    }
                    else if (d1.Rows[i].Field<string>(3) == "followed you"){
                        button1.Text ="@" + username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = " started Following you.";
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                    }
                    else if (d1.Rows[i].Field<string>(3) == "Comment"){
                        button1.Text = "@" + username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = " Commented on your ";
                        LinkButton button2 = new LinkButton();
                        button2.ID = button2.UniqueID;
                        button2.Attributes.Add("runat", "server");
                        button2.Attributes.Add("style", "font-size: 30px;");
                        //  button2.Attributes.Add("Width", "561px");
                        button2.Text = "tweet.";
                        button2.Click += (se, ev) =>
                        {
                            Session["tweetid"] = tid;
                            Response.Redirect("tweet.aspx?val=" + uid);
                        };
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                        p.Controls.Add(button2);    
              
                    }
                    else if (d1.Rows[i].Field<string>(3) == "ReTweet"){
                        button1.Text = username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = " Retweeted your ";
                        LinkButton button2 = new LinkButton();
                        button2.ID = button2.UniqueID;
                        button2.Attributes.Add("runat", "server");
                        button2.Attributes.Add("style", "font-size: 30px;");
                        //  button2.Attributes.Add("Width", "561px");
                        button2.Text = "tweet.";
                        button2.Click += (se, ev) =>
                        {
                            Session["tweetid"] = tid;
                            Response.Redirect("tweet.aspx?val=" + uid);
                        };
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                        p.Controls.Add(button2);    
              
                    }
                    else if (d1.Rows[i].Field<string>(3) == "Tag"){
                        button1.Text = username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = " Tagged you in their ";
                        LinkButton button2 = new LinkButton();
                        button2.ID = button2.UniqueID;
                        button2.Attributes.Add("runat", "server");
                        button2.Attributes.Add("style", "font-size: 30px;");
                        //  button2.Attributes.Add("Width", "561px");
                        button2.Text = "tweet.";
                        button2.Click += (se, ev) =>
                        {
                            Session["tweetid"] = tid;
                            Response.Redirect("tweet.aspx?val=" + uid);
                        };
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                        p.Controls.Add(button2);    
              
                    }
                    else if(d1.Rows[i].Field<string>(3)=="Message"){
                        button1.Text = username;
                        button1.Click += (se, ev) =>
                        {
                            Session["uid"] = uid;
                            Response.Redirect("user.aspx?val=" + uid);
                        };
                        notify.Text = " sent you a secret Message.";
                        p.Controls.Add(button1);
                        p.Controls.Add(notify);
                    }
                    p.Controls.Add(new LiteralControl("</div>"));
                    p.Controls.Add(new LiteralControl("<br/><br/>"));
                }
                
            }
        }
    }
}