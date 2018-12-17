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
    public partial class home : System.Web.UI.Page
    {
        private List<int> _controlIds;
      
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

        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            int userid = (int)(Session["userid"]);
            btnSave.Text = "Post Tweet";
            btnSave.Click += (se, ev) =>
            {
                DataTable d = new DataTable();
                int g = obj.InsertTweet(userid, Textbox5.Text, ref d);

                if (g != -1)
                {
                      if (FileUpload1.HasFile)
                      {
                          string FileName = FileUpload1.FileName;
                          FileUpload1.SaveAs(Server.MapPath("tweet_images/" + FileName));
                          int tid = (int)d.Rows[0].Field<decimal>(0);
                          int x = obj.Add_Image(FileName, tid);
                          if (x == -1)
                          {
                              tweets.Text += "image upload error \n";
                          }
                      }
                    tweets.Attributes.Add("style","width: 500px; height: 200px; padding: 10px; border: 2px solid #000000; font-family: Times New Roman; font-size: 25px; background-color: #ffffff");
                    tweets.Text += "Your Tweet has been posted successfully.";
                    System.Threading.Thread.Sleep(1500);
                    Response.Redirect("home.aspx?val=" + userid);
                }
                else
                {
                    tweets.Attributes.Add("style", "width: 500px; height: 200px; padding: 10px; border: 2px solid #000000; font-family: Times New Roman; font-size: 25px; background-color: #ffffff");
                    tweets.Text = "There was an error. Try again!";
                }
            };

            DataTable DT = new DataTable();
            int check = obj.newsfeed(userid, ref DT);
            if (check == -1)
            {
                tweets.Text = "Start Following some users to see new posts!";
            }
            else
            {
                foreach (DataRow row in DT.Rows)
                {
                    DataTable t1 = new DataTable();
                    int tweetid = row.Field<int>(0);
                    bool checklike = false;
                    int get1 = obj.readTweet(tweetid, ref t1);
                    int get2 = obj.numOfLikes(userid, tweetid, ref checklike);
                    int get3 = obj.numOfcomments(tweetid);
                    int get4 = obj.numOfretweets(tweetid);

                    DataTable path = new DataTable();
                    int get5 = obj.Get_Image(tweetid, ref path);

                    string name = obj.GetUser(row.Field<int>(1));
                    if (get1 != -1 && get2 != -1 && get3 != -1 && get4 != -1)
                    {
                        UpdatePanel u1 = new UpdatePanel();
                        u1.ID = "Update"+tweetid;
                        u1.Attributes.Add("runat", "server");
                       
                        UpdatePanel u2 = new UpdatePanel();
                        u2.ID = "Update2" + tweetid;
                        u2.Attributes.Add("runat", "server");

                        Button bu = new Button();
                        bu.ID = bu.UniqueID;
                        bu.Attributes.Add("runat", "server");
                        bu.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(255, 216, 0,0.6);");
                        bu.Text = name;

                        int u = obj.GetUserID(name);
                        if (u == userid)
                        {
                            bu.Click += (se, ev) =>
                            {
                                Response.Redirect("profile.aspx?val=" + u);
                            };
                        }
                        else
                        {
                            bu.Click += (se, ev) =>
                            {
                                Session["uid"] = u;
                                Response.Redirect("user.aspx?val=" + u);
                            };
                        }
                       
                        TextBox tw = new TextBox();
                        tw.ID = "Writer"+tweetid;
                        tw.TextMode = TextBoxMode.MultiLine;
                        tw.Rows = 3;
                        tw.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(18, 12, 85, 0.6); color: white; text-align: left;width:900px ");
                        tw.Text = t1.Rows[0].Field<string>(0);
                        tw.Attributes.Add("runat", "server");
                        tw.ReadOnly = true;
                        Label h = new Label();
                        h.ID = h.UniqueID;
                        h.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:720px;font-family: Times New Roman;border: solid; border-color: azure; background-color: white; color: black; ");
                        h.Attributes.Add("runat", "server");
                        h.Text = "<sub>" + t1.Rows[0].Field<DateTime>(1) + "</sub>";
                        
                        Image ima = new Image();
                        ima.ID = "me"+tweetid;

                        Button remove = new Button();
                        remove.ID = remove.UniqueID;
                        remove.Attributes.Add("runat", "server");
                        remove.Text = "Remove Image";
                        remove.Visible = false;
                        remove.Click += (s, ev) =>
                        {
                            int x = obj.Remove_Image(tweetid);
                            ima.Height = 0;
                            ima.ImageUrl = "";
                        };

                        DropDownList d = new DropDownList();
                        Button save = new Button();
                        d.ID = "format" + tweetid;
                        d.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:790px ");
                        d.Attributes.Add("runat", "server");
                        d.Items.Add("");
                        d.Items.Add("EDIT");
                        d.Items.Add("DELETE");
                        d.AutoPostBack = true;
                        d.SelectedIndexChanged += (s, ev) =>
                        {
                            if (d.SelectedIndex == 1)
                            {
                                remove.Visible = true;
                                tw.ReadOnly = false;
                                save.Text = "SAVE";
                                save.Visible = true;
                            }
                            if (d.SelectedIndex == 2)
                            {
                                save.Text = "CONFIRM";
                                save.Visible = true;
                            }
                        };

                        save.ID = "save" + tweetid;
                        save.Attributes.Add("runat", "server");
                        save.Attributes.Add("style", "font-family:'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;margin-left:790px");
                        save.Visible = false;
                        save.Click += (s, ev) =>
                        {
                            if (d.SelectedIndex == 1)
                            {
                                int select = obj.editTweet(tweetid, tw.Text);
                                if (select != -1)
                                {
                                    tw.ReadOnly = true;
                                    d.SelectedIndex = 0;
                                    save.Visible = false;
                                    remove.Visible = false;
                                }
                            }
                            else if (d.SelectedIndex == 2)
                            {
                                int select = obj.DeleteTweet(tweetid.ToString());
                                if (select != -1)
                                {
                                    var cis = ControlIds;
                                    if (cis.Contains(tweetid))
                                        cis.Remove(tweetid);
                                    ControlIds = cis;
                                    if (ph.Controls.Contains(u2))
                                        ph.Controls.Remove(u2);
                                    if (ph.Controls.Contains(u1))
                                        ph.Controls.Remove(u1);
                                }
                            }
                        };

                        if (u != userid)
                        {
                            d.Visible = false;
                        }

                        u1.ContentTemplateContainer.Controls.Add(bu);
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("<br/>"));
                        u1.ContentTemplateContainer.Controls.Add(d);
                        u1.ContentTemplateContainer.Controls.Add(save);
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        u1.ContentTemplateContainer.Controls.Add(tw);
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        u1.ContentTemplateContainer.Controls.Add(h);
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("<div style=\"text-align: center;\">"));
                        u1.ContentTemplateContainer.Controls.Add(ima);
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</div>"));

                        if (get5 != -1)
                        {
                            string imagename = path.Rows[0].Field<string>(0);
                            ima.Attributes.Add("style", "width: 500px; height: 350px;");
                            ima.ImageUrl = "tweet_images/" + imagename;
                            if(u==userid)
                            {
                                u1.ContentTemplateContainer.Controls.Add(remove);
                            }
                        }

                        Button b1 = new Button();
                        b1.ID = b1.UniqueID;
                        b1.Attributes.Add("runat", "server");
                        b1.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                        if (checklike == false)
                            b1.Text = "LIKE ";
                        else
                            b1.Text = "UNLIKE ";
                        b1.Text += get2;
                        b1.Click += (s, ev) =>
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
                        u1.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        u1.ContentTemplateContainer.Controls.Add(b1);

                        Button b2 = new Button();
                        b2.ID = "cmnt" + tweetid;
                        b2.Attributes.Add("runat", "server");
                        b2.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                        b2.Text = "COMMENT ";
                        b2.Text += get3;
                        b2.Click += (s, ev) =>
                        {
                            DataTable c = new DataTable();

                            var cis = ControlIds;
                            int id = tweetid;
                            cis.Add(id);
                            ControlIds = cis;

                            int cmnts = obj.GetComments(tweetid, ref c);
                            if (cmnts > 0)
                            {
                                for (int i = 0; i < cmnts; i++)
                                {
                                    UpdatePanel u3 = new UpdatePanel();
                                    u3.ID = "Update3" + tweetid + i;
                                    u3.Attributes.Add("runat", "server");

                                    int cmntid = c.Rows[i].Field<int>(0);
                                    string cmntstr = c.Rows[i].Field<string>(1);
                                    int commuserid = c.Rows[i].Field<int>(2);
                                    string commname = obj.GetUser(commuserid);
                                    Button mybutton = new Button();
                                    mybutton.ID = mybutton.UniqueID;
                                    mybutton.Attributes.Add("runat", "server");
                                    mybutton.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");
                                    mybutton.Text = commname;
                                    if (commuserid == userid)
                                    {
                                        mybutton.Click += (se, evg) =>
                                        {
                                            Response.Redirect("profile.aspx?val=" + commuserid);

                                        };
                                    }
                                    else
                                    {
                                        mybutton.Click += (se, evg) =>
                                        {

                                            Session["uid"] = commuserid;
                                            Response.Redirect("user.aspx?val=" + commuserid);

                                        };
                                    }

                                    
                                    TextBox l = new TextBox();
                                    l.ID = "cno" + tweetid + i;
                                    l.TextMode = TextBoxMode.MultiLine;
                                    l.Rows = 3;
                                    l.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                                    l.Attributes.Add("runat", "server");
                                    l.Text = cmntstr;
                                    l.ReadOnly = true;
                                    Label t = new Label();
                                    t.ID = "tno" + tweetid + i;
                                    t.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:720px;font-family: Times New Roman;border: solid; border-color: azure; background-color: white; color: black; ");
                                    t.Attributes.Add("runat", "server");
                                    t.Text = "<sub>" + c.Rows[i].Field<DateTime>(3) + "</sub>";

                                    DropDownList cd = new DropDownList();
                                    Button g = new Button();

                                    cd.ID = "ddlno" + tweetid + i;
                                    cd.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:790px ");
                                    cd.Attributes.Add("runat", "server");
                                    cd.Items.Add("");
                                    cd.Items.Add("EDIT");
                                    cd.Items.Add("DELETE");
                                    cd.AutoPostBack = true;
                                    cd.SelectedIndexChanged += (se, eve) =>
                                    {
                                        if (cd.SelectedIndex == 1)
                                        {
                                            l.ReadOnly = false;
                                            g.Text = "SAVE";
                                            g.Visible = true;
                                        }
                                        else if (cd.SelectedIndex == 2)
                                        {
                                            g.Text = "CONFIRM";
                                            g.Visible = true;
                                        }
                                    };

                                    g.ID = "sno" + tweetid + i;
                                    g.Attributes.Add("runat", "server");
                                    g.Attributes.Add("style", "font-family:'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;margin-left:790px");
                                    g.Visible = false;
                                    g.Click += (se, eve) =>
                                    {
                                        if (cd.SelectedIndex == 1)
                                        {
                                            int select = obj.editTweet(cmntid, t.Text);
                                            if (select != -1)
                                            {
                                                l.ReadOnly = true;
                                                g.Visible = false;
                                                cd.SelectedIndex = 0;
                                            }
                                        }
                                        else if (cd.SelectedIndex == 2)
                                        {
                                            int select = obj.DeleteTweet(cmntid.ToString());
                                            if (select != -1)
                                            {
                                                if (u1.ContentTemplateContainer.Controls.Contains(u3))
                                                    u1.ContentTemplateContainer.Controls.Remove(u3);
                                                b2.Text = "";
                                                b2.Text = "COMMENT" + get3--;
                                            }
                                        }
                                    };

                                    if (commuserid != userid)
                                    {
                                        cd.Visible = false;
                                    }

                                    bool ccl = false;
                                    int num = obj.numOfLikes(userid, cmntid, ref ccl);
                                    Button like = new Button();
                                    like.ID = "like" + tweetid + i;
                                    like.Attributes.Add("runat", "server");
                                    like.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                                    if (ccl == false)
                                        like.Text = "LIKE ";
                                    else
                                        like.Text = "UNLIKE ";
                                    like.Text += num;
                                    like.Click += (se, eve) =>
                                    {
                                        bool ret = false;
                                        int found = obj.EnterLike(userid, cmntid, ref ret);
                                        if (found != -1)
                                            if (!ret)
                                            {
                                                like.Text = "UNLIKE ";
                                                like.Text += found;
                                            }
                                            else
                                            {
                                                like.Text = "LIKE ";
                                                like.Text += found;
                                            }
                                    };

                                    int rss = obj.numOfretweets(cmntid);
                                    Button rt = new Button();
                                    rt.ID = "rtwt" + tweetid + i;
                                    rt.Attributes.Add("runat", "server");
                                    rt.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                                    rt.Text = "RETWEET ";
                                    rt.Text += rss;
                                    rt.Click += (se, eve) =>
                                    {
                                        Session["retweetid"] = cmntid;
                                        Session["retweetstr"] = "Retweet : " + cmntstr;
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('retweet.aspx?val=" + cmntid + "','_newtab');</script>", false);
                                    };

                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(mybutton);
                                    u3.ContentTemplateContainer.Controls.Add(cd);
                                    u3.ContentTemplateContainer.Controls.Add(g);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(l);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(t);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(like);
                                    u3.ContentTemplateContainer.Controls.Add(rt);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u1.ContentTemplateContainer.Controls.Add(u3);
                                }
                            }

                            TextBox tb = new TextBox();
                            tb.ID = "tb" + tweetid;
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Rows = 3;
                            tb.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                            tb.BorderStyle = BorderStyle.Solid;
                            tb.Attributes.Add("runat", "server");

                            Button b4 = new Button();
                            b4.ID = "tw" + tweetid;
                            b4.Attributes.Add("runat", "server");
                            b4.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                            b4.Text = "~Tweet";

                            b4.Click += (se, eve) =>
                            {
                                int insert = obj.InsertComment(userid, tweetid, tb.Text);
                                if (insert == -1)
                                {
                                    Label error = new Label();
                                    error.Attributes.Add("runat", "server");
                                    error.Attributes.Add("style", "color: red;");
                                    error.Text = "Connection failed. Try Again.";
                                    u1.ContentTemplateContainer.Controls.Add(error);
                                }
                                else
                                {
                                    UpdatePanel u3 = new UpdatePanel();
                                    u3.ID = "Update3" + tweetid + cmnts;
                                    u3.Attributes.Add("runat", "server");
                                    TextBox l = new TextBox();
                                    l.ID = "cno" + tweetid + cmnts;
                                    l.TextMode = TextBoxMode.MultiLine;
                                    l.Rows = 3;
                                    l.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                                    l.Attributes.Add("runat", "server");
                                    l.Text = tb.Text;
                                    l.ReadOnly = true;
                                    Label t = new Label();
                                    t.ID = "tno" + tweetid + cmnts;
                                    t.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:720px;font-family: Times New Roman;border: solid; border-color: azure; background-color: white; color: black; ");
                                    t.Attributes.Add("runat", "server");
                                    t.Text = "<sub>" + DateTime.Now + "</sub>";
                                    Button mybutton2 = new Button();
                                    mybutton2.ID = mybutton2.UniqueID;
                                    mybutton2.Attributes.Add("runat", "server");
                                    mybutton2.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");
                                    string myusername = obj.GetUser(userid);
                                    mybutton2.Text = myusername;
                                    mybutton2.Click += (seo, evg) =>
                                    {
                                        Response.Redirect("profile.aspx?val=" + userid);

                                    };

                                    DropDownList cd = new DropDownList();
                                    Button g = new Button();

                                    cd.ID = "ddlno" + tweetid + cmnts;
                                    cd.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:790px ");
                                    cd.Attributes.Add("runat", "server");
                                    cd.Items.Add("");
                                    cd.Items.Add("EDIT");
                                    cd.Items.Add("DELETE");
                                    cd.AutoPostBack = true;
                                    cd.SelectedIndexChanged += (sen, even) =>
                                    {
                                        if (cd.SelectedIndex == 1)
                                        {
                                            l.ReadOnly = false;
                                            g.Text = "SAVE";
                                            g.Visible = true;
                                        }
                                        else if (cd.SelectedIndex == 2)
                                        {
                                            g.Text = "CONFIRM";
                                            g.Visible = true;
                                        }

                                    };

                                    g.ID = "sno" + tweetid + cmnts;
                                    g.Attributes.Add("runat", "server");
                                    g.Attributes.Add("style", "font-family:'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;margin-left:790px");
                                    g.Visible = false;
                                    g.Click += (sen, even) =>
                                    {
                                        if (cd.SelectedIndex == 1)
                                        {
                                            int select = obj.editTweet(insert, l.Text);
                                            if (select != -1)
                                            {
                                                l.ReadOnly = true;
                                                g.Visible = false;
                                                cd.SelectedIndex = 0;
                                            }
                                        }
                                        else if (cd.SelectedIndex == 2)
                                        {
                                            int select = obj.DeleteTweet(insert.ToString());
                                            if (select != -1)
                                            {
                                                if (u1.ContentTemplateContainer.Controls.Contains(u3))
                                                    u1.ContentTemplateContainer.Controls.Remove(u3);
                                                b2.Text = "";
                                                b2.Text = "COMMENT" + get3--;
                                            }
                                        }
                                    };

                                    bool ccl = false;
                                    int num = obj.numOfLikes(userid, insert, ref ccl);
                                    Button like = new Button();
                                    like.ID = "like" + tweetid + cmnts;
                                    like.Attributes.Add("runat", "server");
                                    like.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                                    if (ccl == false)
                                        like.Text = "LIKE ";
                                    else
                                        like.Text = "UNLIKE ";
                                    like.Text += num;
                                    like.Click += (sen, even) =>
                                    {
                                        bool ret = false;
                                        int found = obj.EnterLike(userid, insert, ref ret);
                                        if (found != -1)
                                            if (!ret)
                                            {
                                                like.Text = "UNLIKE ";
                                                like.Text += found;
                                            }
                                            else
                                            {
                                                like.Text = "LIKE ";
                                                like.Text += found;
                                            }
                                    };

                                    int rss = obj.numOfretweets(insert);
                                    Button rt = new Button();
                                    rt.ID = "rtwt" + tweetid + cmnts;
                                    rt.Attributes.Add("runat", "server");
                                    rt.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                                    rt.Text = "RETWEET ";
                                    rt.Text += rss;
                                    rt.Click += (sen, even) =>
                                    {
                                        Session["retweetid"] = insert;
                                        Session["retweetstr"] = "Retweet : " + l.Text;
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('retweet.aspx?val=" + insert + "','_newtab');</script>", false);
                                    };

                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(mybutton2);
                                    u3.ContentTemplateContainer.Controls.Add(cd);
                                    u3.ContentTemplateContainer.Controls.Add(g);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(l);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(t);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u3.ContentTemplateContainer.Controls.Add(like);
                                    u3.ContentTemplateContainer.Controls.Add(rt);
                                    u3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                                    u1.ContentTemplateContainer.Controls.Add(u3);
                                    tb.Text = "";
                                    b2.Text = "";
                                    b2.Text = "COMMENT" + get3++;
                                }
                            };

                            u2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            u2.ContentTemplateContainer.Controls.Add(tb);
                            u2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            u2.ContentTemplateContainer.Controls.Add(b4);
                            u2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                        };
                        u1.ContentTemplateContainer.Controls.Add(b2);
                        
                        Button b3 = new Button();
                        b3.ID = b3.UniqueID;
                        b3.Attributes.Add("runat", "server");
                        b3.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                        b3.Text = "RETWEET ";
                        b3.Text += get4;
                        b3.Click += (s, ev) =>
                        {
                            Session["retweetid"] = tweetid;
                            Session["retweetstr"] = "Retweet : " + tw.Text;
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('retweet.aspx?val=" + tweetid + "','_newtab');</script>", false);
                        };
                        u1.ContentTemplateContainer.Controls.Add(b3);

                        ph.Controls.Add(u1);
                        ph.Controls.Add(u2);
                        ph.Controls.Add(new LiteralControl("<br /><br /><br />"));
                    }
                    else
                    {
                        Label t = new Label();
                        t.ID = t.UniqueID;
                        t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                        t.BorderStyle = BorderStyle.Solid;
                        t.Text = "An error occured. Please reload!";
                        ph.Controls.Add(new LiteralControl("<br />"));
                        ph.Controls.Add(t);
                        ph.Controls.Add(new LiteralControl("<br /><br /><br />"));
                    }
                }
            }

            if (IsPostBack)
            {
                for (int i = 0; i < ControlIds.Count; i++)
                {
                    var update = (UpdatePanel)ph.FindControl("Update" + ControlIds[i]);
                    var b2 = (Button)update.FindControl("cmnt" + ControlIds[i]);
                    var update2 = (UpdatePanel)ph.FindControl("Update2" + ControlIds[i]);
                    int tweetid = ControlIds[i];
                    DataTable c = new DataTable();
                    int cmnts = obj.GetComments(tweetid, ref c);

                    if (cmnts > 0)
                    {
                        for (int j = 0; j < cmnts; j++)
                        {
                            UpdatePanel update3 = new UpdatePanel();
                            update3.ID = "Update3" + tweetid + j;
                            update3.Attributes.Add("runat", "server");

                            int here = c.Rows[j].Field<int>(0);
                            string write = c.Rows[j].Field<string>(1);

                            TextBox l = new TextBox();
                            l.ID = "cno" + tweetid + j;
                            l.TextMode = TextBoxMode.MultiLine;
                            l.Rows = 3;
                            l.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                            l.Attributes.Add("runat", "server");
                            l.Text = write;
                            l.ReadOnly = true;

                            Label t = new Label();
                            t.ID = "tno" + tweetid + j;
                            t.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:720px;font-family: Times New Roman;border: solid; border-color: azure; background-color: white; color: black; ");
                            t.Attributes.Add("runat", "server");
                            t.Text = "<sub>" + c.Rows[j].Field<DateTime>(3) + "</sub>";
                            int commuserid = c.Rows[i].Field<int>(2);
                            string commname = obj.GetUser(commuserid);
                            Button mybutton = new Button();
                            mybutton.ID = mybutton.UniqueID;
                            mybutton.Attributes.Add("runat", "server");
                            mybutton.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");
                            mybutton.Text = commname;
                            if (commuserid == userid)
                            {
                                mybutton.Click += (se, evg) =>
                                {
                                    Response.Redirect("profile.aspx?val=" + commuserid);

                                };
                            }
                            else
                            {
                                mybutton.Click += (se, evg) =>
                                {

                                    Session["uid"] = commuserid;
                                    Response.Redirect("user.aspx?val=" + commuserid);

                                };
                            }

                            DropDownList cd = new DropDownList();
                            Button save = new Button();

                            cd.ID = "ddlno" + tweetid + j;
                            cd.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:790px ");
                            cd.Attributes.Add("runat", "server");
                            cd.Items.Add("");
                            cd.Items.Add("EDIT");
                            cd.Items.Add("DELETE");
                            cd.AutoPostBack = true;
                            cd.SelectedIndexChanged += (s, ev) =>
                            {
                                if (cd.SelectedIndex == 1)
                                {
                                    l.ReadOnly = false;
                                    save.Text = "SAVE";
                                    save.Visible = true;
                                }
                                else if (cd.SelectedIndex == 2)
                                {
                                    save.Text = "CONFIRM";
                                    save.Visible = true;
                                }
                            };

                            save.ID = "sno" + tweetid + j;
                            save.Attributes.Add("runat", "server");
                            save.Attributes.Add("style", "font-family:'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;margin-left:790px");
                            save.Visible = false;
                            save.Click += (se, eve) =>
                            {
                                if (cd.SelectedIndex == 1)
                                {
                                    int select = obj.editTweet(here, l.Text);
                                    if (select != -1)
                                    {
                                        l.ReadOnly = true;
                                        save.Visible = false;
                                        cd.SelectedIndex = 0;
                                    }
                                }
                                else if (cd.SelectedIndex == 2)
                                {
                                    int select = obj.DeleteTweet(here.ToString());
                                    if (select != -1)
                                    {
                                        if (update.ContentTemplateContainer.Controls.Contains(update3))
                                            update.ContentTemplateContainer.Controls.Remove(update3);
                                        b2.Text = "";
                                        b2.Text = "COMMENT" + (cmnts - 1);
                                    }
                                }
                            };

                            if (commuserid != userid)
                            {
                                cd.Visible = false;
                            }
                            bool ccl = false;
                            int num = obj.numOfLikes(userid, here, ref ccl);
                            Button like = new Button();
                            like.ID = "like" + tweetid + j;
                            like.Attributes.Add("runat", "server");
                            like.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                            if (ccl == false)
                                like.Text = "LIKE ";
                            else
                                like.Text = "UNLIKE ";
                            like.Text += num;
                            like.Click += (se, eve) =>
                            {
                                bool ret = false;
                                int found = obj.EnterLike(userid, here, ref ret);
                                if (found != -1)
                                    if (!ret)
                                    {
                                        like.Text = "UNLIKE ";
                                        like.Text += found;
                                    }
                                    else
                                    {
                                        like.Text = "LIKE ";
                                        like.Text += found;
                                    }
                            };
                            int rss = obj.numOfretweets(here);
                            Button rt = new Button();
                            rt.ID = "rtwt" + tweetid + j;
                            rt.Attributes.Add("runat", "server");
                            rt.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                            rt.Text = "RETWEET ";
                            rt.Text += rss;
                            rt.Click += (se, eve) =>
                            {
                                Session["retweetid"] = here;
                                Session["retweetstr"] = "Retweet : " + write;
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('retweet.aspx?val=" + here + "','_newtab');</script>", false);
                            };

                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(mybutton);
                            update3.ContentTemplateContainer.Controls.Add(cd);
                            update3.ContentTemplateContainer.Controls.Add(save);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(l);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(t);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(like);
                            update3.ContentTemplateContainer.Controls.Add(rt);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update.ContentTemplateContainer.Controls.Add(update3);
                        }
                    }

                    TextBox tb = new TextBox();
                    tb.ID = "tb" + tweetid;
                    tb.TextMode = TextBoxMode.MultiLine;
                    tb.Rows = 3;
                    tb.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                    tb.BorderStyle = BorderStyle.Solid;
                    tb.Attributes.Add("runat", "server");

                    Button b4 = new Button();
                    b4.ID = "tw" + tweetid;
                    b4.Attributes.Add("runat", "server");
                    b4.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                    b4.Text = "~Tweet";

                    b4.Click += (se, eve) =>
                    {
                        int insert = obj.InsertComment(userid, tweetid, tb.Text);
                        if (insert == -1)
                        {
                            Label error = new Label();
                            error.Attributes.Add("runat", "server");
                            error.Attributes.Add("style", "color: red;");
                            error.Text = "Connection failed. Try Again.";
                            update.ContentTemplateContainer.Controls.Add(error);
                        }
                        else
                        {
                            UpdatePanel update3 = new UpdatePanel();
                            update3.ID = "Update3" + tweetid + cmnts;
                            update3.Attributes.Add("runat", "server");

                            TextBox l = new TextBox();
                            l.ID = "cno" + tweetid + cmnts;
                            l.TextMode = TextBoxMode.MultiLine;
                            l.Rows = 3;
                            l.Attributes.Add("style", "font-family: Times New Roman;font-size: 20px; border: solid; border-color: black; background-color: rgba(255, 216, 0,0.6); color: black; text-align: left;width:900px ");
                            l.Attributes.Add("runat", "server");
                            l.Text = tb.Text;
                            l.ReadOnly = true;
                            Label t = new Label();
                            t.ID = "tno" + tweetid + cmnts;
                            t.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:720px;font-family: Times New Roman;border: solid; border-color: azure; background-color: white; color: black; ");
                            t.Attributes.Add("runat", "server");
                            t.Text = "<sub>" + DateTime.Now + "</sub>";
                            Button mybutton2 = new Button();
                            mybutton2.ID = mybutton2.UniqueID;
                            mybutton2.Attributes.Add("runat", "server");
                            mybutton2.Attributes.Add("style", "font-family: Broadway;  font-size: 25px; color: black; background-color:rgba(18, 12, 85, 0.6);");
                            string myusername = obj.GetUser(userid);
                            mybutton2.Text = myusername;
                            mybutton2.Click += (seo, evg) =>
                            {
                                Response.Redirect("profile.aspx?val=" + userid);
                                
                            };
                            DropDownList cd = new DropDownList();
                            Button g = new Button();

                            cd.ID = "ddlno" + tweetid + cmnts;
                            cd.Attributes.Add("style", "font-size: 20px; text-align: right;margin-left:790px ");
                            cd.Attributes.Add("runat", "server");
                            cd.Items.Add("");
                            cd.Items.Add("EDIT");
                            cd.Items.Add("DELETE");
                            cd.AutoPostBack = true;
                            cd.SelectedIndexChanged += (s, ev) =>
                            {
                                if (cd.SelectedIndex == 1)
                                {
                                    l.ReadOnly = false;
                                    g.Visible = true;
                                    l.Text = "SAVE";
                                }
                                else if (cd.SelectedIndex == 2)
                                {
                                    g.Text = "CONFIRM";
                                    g.Visible = true;
                                }
                            };

                            g.ID = "sno" + tweetid + cmnts;
                            g.Attributes.Add("runat", "server");
                            g.Attributes.Add("style", "font-family:'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;margin-left:790px");
                            g.Visible = false;
                            g.Click += (sev, even) =>
                            {
                                if (cd.SelectedIndex == 1)
                                {
                                    int select = obj.editTweet(insert, l.Text);
                                    if (select != -1)
                                    {
                                        l.ReadOnly = true;
                                        g.Visible = false;
                                        cd.SelectedIndex = 0;
                                    }
                                }
                                else if (cd.SelectedIndex == 2)
                                {
                                    int select = obj.DeleteTweet(insert.ToString());
                                    if (select != -1)
                                    {
                                        if (update.ContentTemplateContainer.Controls.Contains(update3))
                                            update.ContentTemplateContainer.Controls.Remove(update3);
                                        b2.Text = "";
                                        b2.Text = "COMMENT" + (cmnts - 1);
                                    }
                                }
                            };

                            bool ccl = false;
                            int num = obj.numOfLikes(userid, insert, ref ccl);
                            Button like = new Button();
                            like.ID = "like" + tweetid + cmnts;
                            like.Attributes.Add("runat", "server");
                            like.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                            if (ccl == false)
                                like.Text = "LIKE ";
                            else
                                like.Text = "UNLIKE ";
                            like.Text += num;
                            like.Click += (sen, even) =>
                            {
                                bool ret = false;
                                int found = obj.EnterLike(userid, insert, ref ret);
                                if (found != -1)
                                    if (!ret)
                                    {
                                        like.Text = "UNLIKE ";
                                        like.Text += found;
                                    }
                                    else
                                    {
                                        like.Text = "LIKE ";
                                        like.Text += found;
                                    }
                            };

                            int rss = obj.numOfretweets(insert);
                            Button rt = new Button();
                            rt.ID = "rtwt" + tweetid + cmnts;
                            rt.Attributes.Add("runat", "server");
                            rt.Attributes.Add("style", "font-family: 'Times New Roman';  font-size: 20px; color: white; background-color:midnightblue;");
                            rt.Text = "RETWEET ";
                            rt.Text += rss;
                            rt.Click += (sen, even) =>
                            {
                                Session["retweetid"] = insert;
                                Session["retweetstr"] = "Retweet : " + l.Text;
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('retweet.aspx?val=" + insert + "','_newtab');</script>", false);
                            };

                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(mybutton2);
                            update3.ContentTemplateContainer.Controls.Add(cd);
                            update3.ContentTemplateContainer.Controls.Add(g);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(l);
                            update3.ContentTemplateContainer.Controls.Add(t);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update3.ContentTemplateContainer.Controls.Add(like);
                            update3.ContentTemplateContainer.Controls.Add(rt);
                            update3.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                            update.ContentTemplateContainer.Controls.Add(update3);
                            tb.Text = "";
                            b2.Text = "";
                            b2.Text = "COMMENT" + cmnts+1;
                        }
                    };

                    update2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                    update2.ContentTemplateContainer.Controls.Add(tb);
                    update2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                    update2.ContentTemplateContainer.Controls.Add(b4);
                    update2.ContentTemplateContainer.Controls.Add(new LiteralControl("</br>"));
                }
            }
        }
    }
}