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
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            string search = (string)Session["searchstring"];
            string myname = (string)Session["Uname"];
            int myown = (int)Session["userid"];
            DataTable DT = new DataTable();
            DataTable DT2 = new DataTable();
            int found = obj.search(search, ref DT, ref DT2);
            if (found == -1)
            {
                Error.Text = "Your text did not yield any result";
            }
            else
            {
                foreach (DataRow row in DT2.Rows)
                {
                    if (search[0] == '#')
                    {                        Label b1 = new Label();
                        b1.ID = b1.UniqueID;
                        b1.Attributes.Add("runat", "server");
                        b1.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: #ffffff; background-color: midnightblue;");
                        b1.Text = row.Field<string>(0) + " (" + row.Field<int>(1) + ")";
                        ph.Controls.Add(b1); 
                        ph.Controls.Add(new LiteralControl("<br/><br/>"));
                    }
                }
                ph.Controls.Add(new LiteralControl("<br/><br/>"));
                foreach (DataRow row in DT.Rows)
                {
                    if (search[0] == '#')
                    {
                        Label b2 = new Label();
                        string thetweet = obj.GetATweet(row.Field<int>(0));
                        int idofuser = obj.GetUserIDfromTweet(row.Field<int>(0));
                        string name = obj.GetUser(idofuser);
                        Button bu = new Button();
                        bu.ID = bu.UniqueID;
                        bu.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");

                        bu.Attributes.Add("runat", "server");

                        bu.Text = name;
                        if (bu.Text == myname)
                        {
                            bu.Click += (se, ev) =>
                            {
                                Response.Redirect("profile.aspx?val=" + myown);
                            };
                        }
                        else
                        {
                            bu.Click += (se, ev) =>
                            {
                                int u = obj.GetUserID(name);
                                Session["uid"] = u;
                                Response.Redirect("user.aspx?val=" + u);
                            };
                        }
                        ph.Controls.Add(bu);
                        b2.ID = b2.UniqueID;
                        b2.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");

                        b2.Attributes.Add("runat", "server");
                        b2.Text = thetweet;
                        ph.Controls.Add(b2);
                        ph.Controls.Add(new LiteralControl("<br/><br/><br/><br/><br/>"));

                    }
                    else
                    {
                            bool checkfollow = false;
                            Button b1 = new Button();
                            b1.ID = b1.UniqueID;
                            b1.Attributes.Add("runat", "server");
                          b1.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");

                        b1.Text = row.Field<string>(0);
                            int u = obj.GetUserID(b1.Text);
                        if (u == myown)
                        {
                            b1.Click += (ser, eve) =>
                            {
                                Response.Redirect("profile.aspx?val=" + myown);
                            };
                            ph.Controls.Add(b1);

                        }
                        else
                        {
                            ph.Controls.Add(new LiteralControl("<br/>"));
                            ph.Controls.Add(new LiteralControl("<br/>"));

                            b1.Click += (ser, eve) =>
                                {
                                    Session["uid"] = u;
                                    Response.Redirect("user.aspx?val=" + u);
                                };

                            ph.Controls.Add(b1);
                            ph.Controls.Add(new LiteralControl("&nbsp &nbsp &nbsp"));

                            Button b2 = new Button();
                            b2.ID = b2.UniqueID;
                            b2.Attributes.Add("runat", "server");
                            b2.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:130px ");

                            obj.checkifcollowing(myown, u, ref checkfollow);
                            if (checkfollow == false)
                                b2.Text = "FOLLOW ";
                            else
                                b2.Text = "UNFOLLOW ";

                            b2.Click += (se, ev) =>
                            {
                                bool ret = false;
                                int f = obj.enterfollow(myown, u, ref ret);
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

                            ph.Controls.Add(b2);
                            ph.Controls.Add(new LiteralControl("<br/>"));
                        }
                    }
                }

            }
        }
    }
}