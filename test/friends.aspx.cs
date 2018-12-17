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
    public partial class friends : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            string username = (string)(Session["Uname"]);
            int userid = obj.GetUserID(username);
            DataTable DT = new DataTable();
            DataTable DT2 = new DataTable();
            int following = obj.GetFollowingID(userid, ref DT);
            int followers = obj.GetFollowerID(userid, ref DT2);
            if (following != -1)
            {
                Label youfollow = new Label();
                youfollow.ID = youfollow.UniqueID;
                youfollow.Attributes.Add("runat", "server");
                youfollow.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                youfollow.Text = "You are Following these Users! :D";
                p.Controls.Add(youfollow);
                p.Controls.Add(new LiteralControl("<br/><br/>"));
                foreach (DataRow row in DT.Rows)
                {
                    bool checkfollow = false;
                    Button b1 = new Button();
                    Button b2 = new Button();
                    b1.ID = b1.UniqueID;
                    b2.ID = b2.UniqueID;
                    b1.Attributes.Add("runat", "server");
                    b1.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: #ffffff; background-color: midnightblue;");
                    b2.Attributes.Add("runat", "server");
                    b2.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                    string name = obj.GetUser(row.Field<int>(0));
                    b1.Text = name;
                    b1.Click += (ser, eve) =>
                    {
                        int u = obj.GetUserID(b1.Text);
                        Session["uid"] = u;
                        Response.Redirect("user.aspx?val=" + u);
                    };
                    p.Controls.Add(b1);
                    obj.checkifcollowing(userid, row.Field<int>(0), ref checkfollow);
                    if (checkfollow == false)
                        b2.Text = "FOLLOW ";
                    else
                        b2.Text = "UNFOLLOW ";

                    b2.Click += (se, ev) =>
                    {
                        bool ret = false;
                        int f = obj.enterfollow(userid, row.Field<int>(0), ref ret);
                        if (f != -1)
                            if (!ret)
                            {
                                b2.Text = "UNFOLLOW ";
                            }
                            else
                            {
                                b2.Text = "FOLLOW ";
                            }
                    };
                    p.Controls.Add(b2);
                    p.Controls.Add(new LiteralControl("<br/><br/>"));
                }
            }
            else if (following == -1)
            {
                Label youfollow = new Label();
                youfollow.ID = youfollow.UniqueID;
                youfollow.Attributes.Add("runat", "server");
                youfollow.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                youfollow.Text = "You are following No User! :(";
                p.Controls.Add(youfollow);
                p.Controls.Add(new LiteralControl("<br/><br/>"));
            }
            if (followers != -1)
            {
                p.Controls.Add(new LiteralControl("<br/><br/>"));
                Label youfollow = new Label();
                youfollow.ID = youfollow.UniqueID;
                youfollow.Attributes.Add("runat", "server");
                youfollow.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                youfollow.Text = "These Users are Following You! :D";
                p.Controls.Add(youfollow);
                p.Controls.Add(new LiteralControl("<br/><br/>"));
                foreach (DataRow row in DT2.Rows)
                {
                    bool checkfollow = false;
                    Button b1 = new Button();
                    Button b2 = new Button();
                    b1.ID = b1.UniqueID;
                    b2.ID = b2.UniqueID;
                    b1.Attributes.Add("runat", "server");
                    b1.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: #ffffff; background-color: midnightblue;");
                    b2.Attributes.Add("runat", "server");
                    b2.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                    string name = obj.GetUser(row.Field<int>(0));
                    b1.Text = name;
                    b1.Click += (ser, eve) =>
                    {
                        int u = obj.GetUserID(b1.Text);
                        Session["uid"] = u;
                        Response.Redirect("user.aspx?val=" + u);
                    };
                    p.Controls.Add(b1);
                    obj.checkifcollowing(userid, row.Field<int>(0), ref checkfollow);
                    if (checkfollow == false)
                        b2.Text = "FOLLOW ";
                    else
                        b2.Text = "UNFOLLOW ";

                    b2.Click += (se, ev) =>
                    {
                        bool ret = false;
                        int f = obj.enterfollow(userid, row.Field<int>(0), ref ret);
                        if (f != -1)
                            if (!ret)
                            {
                                b2.Text = "UNFOLLOW ";
                            }
                            else
                            {
                                b2.Text = "FOLLOW ";
                            }
                    };
                    p.Controls.Add(b2);
                    p.Controls.Add(new LiteralControl("<br/><br/>"));
                }
            }
            else if (followers == -1)
            {
                Label youfollow = new Label();
                youfollow.ID = youfollow.UniqueID;
                youfollow.Attributes.Add("runat", "server");
                youfollow.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                youfollow.Text = "No one is Following you! :(";
                p.Controls.Add(youfollow);
                p.Controls.Add(new LiteralControl("<br/><br/>"));
            }

        }
    }
}