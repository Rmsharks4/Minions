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
    public partial class AboutUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            int id = (int)(Session["aboutid"]);
            int userid = (int)(Session["userid"]);
            DataTable DT = new DataTable();
            int check = obj.GetInformation(id,ref DT);
            if (check != -1)
            {
                string name = DT.Rows[0].Field<string>(0);
                string uname = DT.Rows[0].Field<string>(1);
                string email = DT.Rows[0].Field<string>(2);
                DateTime date = DT.Rows[0].Field<DateTime>(3);
                string country = DT.Rows[0].Field<string>(4);
                string cellno = DT.Rows[0].Field<string>(5);
                string password = DT.Rows[0].Field<string>(6);

                TextBox about = new TextBox();
                about.ID = about.UniqueID;
                about.TextMode = TextBoxMode.MultiLine;
                about.Rows = 1;
                about.Attributes.Add("runat", "server");
                about.Attributes.Add("style", "font-family: Broadway;font-size: 30px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                about.Text = "ABOUT @" + uname;
                about.ReadOnly = true;

                //-------------------NAME----------------
                TextBox t1 = new TextBox();
                t1.ID = t1.UniqueID;
                t1.TextMode = TextBoxMode.MultiLine;
                t1.Rows = 1;
                t1.Attributes.Add("runat", "server");
                t1.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t1.Text = "Name";
                t1.ReadOnly = true;

                TextBox l1 = new TextBox();
                l1.ID = l1.UniqueID;
                l1.TextMode = TextBoxMode.MultiLine;
                l1.Rows = 1;
                l1.Attributes.Add("runat", "server");
                l1.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l1.Text = name;
                l1.ReadOnly = true;

                //------------USERNAME----------
                TextBox t2 = new TextBox();
                t2.ID = t2.UniqueID;
                t2.TextMode = TextBoxMode.MultiLine;
                t2.Rows = 1;
                t2.Attributes.Add("runat", "server");
                t2.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t2.Text = "Username";
                t2.ReadOnly = true;

                TextBox l2 = new TextBox();
                l2.ID = l2.UniqueID;
                l2.TextMode = TextBoxMode.MultiLine;
                l2.Rows = 1;
                l2.Attributes.Add("runat", "server");
                l2.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l2.Text = "@" + uname;
                l2.ReadOnly = true;

                //----------------EMAIL-----------
                TextBox t3 = new TextBox();
                t3.ID = t3.UniqueID;
                t3.TextMode = TextBoxMode.MultiLine;
                t3.Rows = 1;
                t3.Attributes.Add("runat", "server");
                t3.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t3.Text = "Email";
                t3.ReadOnly = true;

                TextBox l3 = new TextBox();
                l3.ID = l3.UniqueID;
                l3.TextMode = TextBoxMode.MultiLine;
                l3.Rows = 1;
                l3.Attributes.Add("runat", "server");
                l3.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l3.Text = email;
                l3.ReadOnly = true;

                //----------------BIRTHDATE--------
                TextBox t4 = new TextBox();
                t4.ID = t4.UniqueID;
                t4.TextMode = TextBoxMode.MultiLine;
                t4.Rows = 1;
                t4.Attributes.Add("runat", "server");
                t4.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t4.Text = "Birthdate";
                t4.ReadOnly = true;

                TextBox l4 = new TextBox();
                l4.ID = l4.UniqueID;
                l4.TextMode = TextBoxMode.MultiLine;
                l4.Rows = 1;
                l4.Attributes.Add("runat", "server");
                l4.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l4.Text += date;
                l4.ReadOnly = true;

                //-------------------COUNTRY---------------
                TextBox t5 = new TextBox();
                t5.ID = t5.UniqueID;
                t5.TextMode = TextBoxMode.MultiLine;
                t5.Rows = 1;
                t5.Attributes.Add("runat", "server");
                t5.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t5.Text = "Country";
                t5.ReadOnly = true;

                TextBox l5 = new TextBox();
                l5.ID = l5.UniqueID;
                l5.TextMode = TextBoxMode.MultiLine;
                l5.Rows = 1;
                l5.Attributes.Add("runat", "server");
                l5.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l5.Text = country;
                l5.ReadOnly = true;

                //---------------CEll NO-----------------
                TextBox t6 = new TextBox();
                t6.ID = t6.UniqueID;
                t6.TextMode = TextBoxMode.MultiLine;
                t6.Rows = 1;
                t6.Attributes.Add("runat", "server");
                t6.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                t6.Text = "Cell No";
                t6.ReadOnly = true;

                TextBox l6 = new TextBox();
                l6.ID = l6.UniqueID;
                l6.TextMode = TextBoxMode.MultiLine;
                l6.Rows = 1;
                l6.Attributes.Add("runat", "server");
                l6.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                l6.Text = cellno;
                l6.ReadOnly = true;
          
                ph.Controls.Add(about);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t1);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l1);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t2);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l2);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t3);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l3);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t4);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l4);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t5);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l5);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(t6);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(l6);
                ph.Controls.Add(new LiteralControl("<br />"));
                ph.Controls.Add(new LiteralControl("<br />"));

                if(id==userid)
                {
                    TextBox t7 = new TextBox();
                    t7.ID = t6.UniqueID;
                    t7.TextMode = TextBoxMode.MultiLine;
                    t7.Rows = 1;
                    t7.Attributes.Add("runat", "server");
                    t7.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                    t7.Text = "Password";
                    t7.ReadOnly = true;

                    TextBox l7 = new TextBox();
                    l7.ID = l6.UniqueID;
                    l7.Rows = 1;
                    l7.Attributes.Add("runat", "server");
                    l7.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                    l7.Text = password;
                    l7.ReadOnly = true;
                    l7.TextMode = TextBoxMode.Password;

                    Button edit = new Button();
                    edit.ID = edit.UniqueID;
                    edit.Attributes.Add("runat", "server");
                    edit.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                    edit.Text = "EDIT INFO";

                    Label error = new Label();
                    error.ID = error.UniqueID;
                    error.Attributes.Add("runat", "server");

                    edit.Click += (s, ev) =>
                    {
                        if (edit.Text == "EDIT INFO")
                        {
                            l1.ReadOnly = false;
                            l2.ReadOnly = false;
                            l3.ReadOnly = false;
                            l4.ReadOnly = false;
                            l5.ReadOnly = false;
                            l6.ReadOnly = false;
                            l7.ReadOnly = false;
                            edit.Text = "SAVE CHANGES";
                        }
                        else
                        {
                            if (l1.Text == "" || l2.Text == "" || l3.Text == "" || l4.Text == "" || l5.Text == "" || l7.Text == "")
                            {
                                error.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                                error.Text = "A Field cannot be left empty";
                            }
                            else
                            {
                                int x = obj.EditUser(userid, l1.Text, l2.Text, l3.Text, l4.Text, l5.Text, l6.Text, l7.Text);
                                if (x != -1)
                                {
                                    l1.ReadOnly = true;
                                    l2.ReadOnly = true;
                                    l3.ReadOnly = true;
                                    l4.ReadOnly = true;
                                    l5.ReadOnly = true;
                                    l6.ReadOnly = true;
                                    l7.ReadOnly = true;
                                    edit.Text = "EDIT INFO";
                                }
                                else
                                {
                                    error.Attributes.Add("style", "font-family: Times New Roman;font-size: 25px; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:200px ");
                                    error.Text = "Invalid Username or Password! Try Again!";
                                }
                            }
                        }
                    };

                    ph.Controls.Add(t7);
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(l7);
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(edit);
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(new LiteralControl("<br />"));

                }
            }
            else
            {
                Label error = new Label();
                error.ID = error.UniqueID;
                error.Attributes.Add("runat", "server");
                error.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                error.Text = "There was some error";
                ph.Controls.Add(error);
                ph.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }
}


