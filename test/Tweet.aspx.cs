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
    public partial class Tweet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            DataTable t1 = new DataTable();
            int tweetid = (int)Session["tweetid"];
            int userid = (int)(Session["userid"]);
            bool checklike = false;
            int get1 = obj.readTweet(tweetid, ref t1);
            int get2 = obj.numOfLikes(userid, tweetid, ref checklike);  ///check
            int get3 = obj.numOfcomments(tweetid);
            int get4 = obj.numOfretweets(tweetid);
            if (get1 != -1 && get2 != -1 && get3 != -1 && get4 != -1)
            {
                PlaceHolder np = new PlaceHolder();
                np.ID = np.UniqueID;
                Label t = new Label();
                t.ID = t.UniqueID;
                t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                t.BorderStyle = BorderStyle.Solid;
                t.Text = t1.Rows[0].Field<string>(0);
                t.Attributes.Add("runat", "server");
                Button b1 = new Button();
                b1.ID = b1.UniqueID;
                b1.Attributes.Add("runat", "server");
                b1.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                if (checklike == false)
                    b1.Text = "LIKE ";
                else
                    b1.Text = "UNLIKE ";
                b1.Text += get2;
                b1.Click += (se, ev) =>
                {
                    bool ret = false;
                    int found = obj.EnterLike(userid, tweetid, ref ret);
                    if (found != -1)
                        if (!ret)
                        {
                            b1.Text = "UNLIKE ";
                            b1.Text += found;
                        }
                        else
                        {
                            b1.Text = "LIKE ";
                            b1.Text += found;
                        }
                };
                //b1.OnClientClick = "return false;";
                Button b2 = new Button();
                b2.ID = b2.UniqueID;
                b2.Attributes.Add("runat", "server");
                b2.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                b2.Text = "COMMENT ";
                b2.Text += get3;
                Button b3 = new Button();
                b3.ID = b3.UniqueID;
                b3.Attributes.Add("runat", "server");
                b3.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                b3.Text = "RETWEET ";
                b3.Text += get4;
                b2.Click += (se, ev) =>
                {
                    DataTable d = new DataTable();
                    int f1 = obj.GetComments(tweetid, ref d);
                    if (f1 == -1)
                    {
                        TextBox tb = new TextBox();
                        tb.ID = tb.UniqueID;
                        tb.Attributes.Add("style", "font-size: 20px;");
                        tb.BorderStyle = BorderStyle.Solid;
                        tb.Attributes.Add("runat", "server");
                        Button b4 = new Button();
                        b4.ID = b4.UniqueID;
                        b4.Attributes.Add("runat", "server");
                        b4.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                        b4.Text = "~Tweet";
                        b4.Click += (ser, eve) =>
                        {
                            int cmnt = obj.InsertComment(userid, tweetid, tb.Text);
                        };
                        np.Controls.Add(tb);
                        np.Controls.Add(new LiteralControl("&nbsp"));
                        np.Controls.Add(b4);
                    }
                    else
                    {
                        np.Controls.Add(MakeDraft(d));
                        TextBox tb = new TextBox();
                        tb.ID = tb.UniqueID;
                        tb.Attributes.Add("style", "font-size: 20px;");
                        tb.BorderStyle = BorderStyle.Solid;
                        tb.Attributes.Add("runat", "server");
                        Button b4 = new Button();
                        b4.ID = b4.UniqueID;
                        b4.Attributes.Add("runat", "server");
                        b4.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                        b4.Text = "~Tweet";
                        b4.Click += (ser, eve) =>
                        {
                            int cmnt = obj.InsertComment(userid, tweetid, tb.Text);
                        };
                        np.Controls.Add(tb);
                        np.Controls.Add(new LiteralControl("&nbsp"));
                        np.Controls.Add(b4);
                    }
                };
                np.Controls.Add(t);
                np.Controls.Add(new LiteralControl("<br />"));
                np.Controls.Add(b1);
                np.Controls.Add(new LiteralControl("&nbsp"));
                np.Controls.Add(b2);
                np.Controls.Add(new LiteralControl("&nbsp"));
                np.Controls.Add(b3);
                np.Controls.Add(new LiteralControl("<br />"));
                p.Controls.Add(np);
                p.Controls.Add(new LiteralControl("<br /><br /><br />"));
            }
            else
            {
                Label t = new Label();
                t.ID = t.UniqueID;
                t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                t.BorderStyle = BorderStyle.Solid;
                t.Text = "An error occured. Please reload!";
                p.Controls.Add(new LiteralControl("<br />"));
                p.Controls.Add(t);
                p.Controls.Add(new LiteralControl("<br /><br /><br />"));
            }
        }
        protected PlaceHolder MakeDraft(DataTable DT)
        {
            PlaceHolder p = new PlaceHolder();
            enter obj = new enter();
            int userid = (int)(Session["userid"]);
            foreach (DataRow row in DT.Rows)
            {
                DataTable t1 = new DataTable();
                int tweetid = row.Field<int>(0);
                bool checklike = false;
                int get1 = obj.readTweet(tweetid, ref t1);
                int get2 = obj.numOfLikes(userid, tweetid, ref checklike);
                int get3 = obj.numOfcomments(tweetid);
                int get4 = obj.numOfretweets(tweetid);
                if (get1 != -1 && get2 != -1 && get3 != -1 && get4 != -1)
                {
                    PlaceHolder np = new PlaceHolder();
                    np.ID = np.UniqueID;
                    Label t = new Label();
                    t.ID = t.UniqueID;
                    t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                    t.BorderStyle = BorderStyle.Solid;
                    t.Text = t1.Rows[0].Field<string>(0);
                    t.Attributes.Add("runat", "server");
                    Button b1 = new Button();
                    b1.ID = b1.UniqueID;
                    b1.Attributes.Add("runat", "server");
                    b1.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                    if (checklike == false)
                        b1.Text = "LIKE ";
                    else
                        b1.Text = "UNLIKE ";
                    b1.Text += get2;
                    b1.Click += (se, ev) =>
                    {
                        bool ret = false;
                        int found = obj.EnterLike(userid, tweetid, ref ret);
                        if (found != -1)
                            if (!ret)
                            {
                                b1.Text = "UNLIKE ";
                                b1.Text += found;
                            }
                            else
                            {
                                b1.Text = "LIKE ";
                                b1.Text += found;
                            }
                    };
                    //b1.OnClientClick = "return false;";
                    Button b2 = new Button();
                    b2.ID = b2.UniqueID;
                    b2.Attributes.Add("runat", "server");
                    b2.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                    b2.Text = "COMMENT ";
                    b2.Text += get3;
                    Button b3 = new Button();
                    b3.ID = b3.UniqueID;
                    b3.Attributes.Add("runat", "server");
                    b3.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                    b3.Text = "RETWEET ";
                    b3.Text += get4;
                    b2.Click += (se, ev) =>
                    {
                        DataTable d = new DataTable();
                        int f1 = obj.GetComments(tweetid, ref d);
                        if (f1 == -1)
                        {
                            TextBox tb = new TextBox();
                            tb.ID = tb.UniqueID;
                            tb.Attributes.Add("style", "font-size: 20px;");
                            tb.BorderStyle = BorderStyle.Solid;
                            tb.Attributes.Add("runat", "server");
                            Button b4 = new Button();
                            b4.ID = b4.UniqueID;
                            b4.Attributes.Add("runat", "server");
                            b4.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                            b4.Text = "~Tweet";
                            b4.Click += (ser, eve) =>
                            {
                                int cmnt = obj.InsertComment(userid, tweetid, tb.Text);
                            };
                            np.Controls.Add(tb);
                            np.Controls.Add(new LiteralControl("&nbsp"));
                            np.Controls.Add(b4);
                        }
                        else
                        {
                            np.Controls.Add(MakeDraft(d));
                            TextBox tb = new TextBox();
                            tb.ID = tb.UniqueID;
                            tb.Attributes.Add("style", "font-size: 20px;");
                            tb.BorderStyle = BorderStyle.Solid;
                            tb.Attributes.Add("runat", "server");
                            Button b4 = new Button();
                            b4.ID = b4.UniqueID;
                            b4.Attributes.Add("runat", "server");
                            b4.Attributes.Add("style", "height: 35px; width: 100px; font-family: Times New Roman; font-size: 10px; background-color: #ffffff;");
                            b4.Text = "~Tweet";
                            b4.Click += (ser, eve) =>
                            {
                                int cmnt = obj.InsertComment(userid, tweetid, tb.Text);
                            };
                            np.Controls.Add(tb);
                            np.Controls.Add(new LiteralControl("&nbsp"));
                            np.Controls.Add(b4);
                        }
                    };
                    np.Controls.Add(t);
                    np.Controls.Add(new LiteralControl("<br />"));
                    np.Controls.Add(b1);
                    np.Controls.Add(new LiteralControl("&nbsp"));
                    np.Controls.Add(b2);
                    np.Controls.Add(new LiteralControl("&nbsp"));
                    np.Controls.Add(b3);
                    np.Controls.Add(new LiteralControl("<br />"));
                    p.Controls.Add(np);
                    p.Controls.Add(new LiteralControl("<br /><br /><br />"));
                }
                else
                {
                    Label t = new Label();
                    t.ID = t.UniqueID;
                    t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                    t.BorderStyle = BorderStyle.Solid;
                    t.Text = "An error occured. Please reload!";
                    p.Controls.Add(new LiteralControl("<br />"));
                    p.Controls.Add(t);
                    p.Controls.Add(new LiteralControl("<br /><br /><br />"));
                }
            }
            return p;
        }
    }
}