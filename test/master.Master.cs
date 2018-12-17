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
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            int userid = (int)(Session["userid"]);
            string username = (string)Session["username"];
            DataTable DT = new DataTable();
            Name.Text = obj.getName(userid);
            Uname.Text = username;
            int k = obj.GetMsgs(userid);
            if (k > 0)
            {
                notify.Attributes.Add("style", "content:attr(data-badge); position: absolute; font-size:.7em; background: red; color: white; width: 18px; height: 18px; text-align:center; line-height:18px; border-radius:50%; box-shadow:0 0 1px #333;");
                notify.Text += k;
            }

            DataTable p = new DataTable();
            int getpic = obj.GetUserImage(userid, ref p);
            if(getpic!=-1)
            {
                string imagename = p.Rows[0].Field<string>(0);
                useri.Height = 100;
                useri.ImageUrl = "userpics/" + imagename;
            }

            upload.Click += (s, ev) =>
            {
                if (F.HasFile)
                {
                    string FileName = F.FileName;
                    F.SaveAs(Server.MapPath("userpics/" + FileName));
                    int x = obj.AddProfilePic(FileName, userid);
                    if (x == -1)
                    {
                        sorry.Text += "image upload error \n";
                    }
                    else
                    {
                        useri.Height = 100;
                        useri.ImageUrl = "userpics/" + FileName;
                    }
                }
            };

            remove.Click += (s, ev) =>
            {
                int x = obj.RemoveProfilePic(userid);
                if (x == -1)
                {
                    sorry.Text += "image removal error \n";
                }
                useri.ImageUrl = "";
            };

            b1.Click += (se, ev) =>
            {
                Response.Redirect("home.aspx?val=" + userid);
            };
            b2.Click += (se, ev) =>
            {
                Response.Redirect("profile.aspx?val=" + userid);
            };
            b3.Click += (se, ev) =>
            {
                Response.Redirect("notify.aspx?val=" + userid);
            };
            b4.Click += (se, ev) =>
            {
                int x = obj.LastOnline(userid);
                if (x == -1)
                {
                    Error.Text = "Connection Failed. Try Again!";
                }
                else
                {
                    Response.Redirect("message.aspx?val=" + userid);
                }
            };
            b5.Click += (se, ev) =>
            {
                Response.Redirect("about.aspx");
            };
            b6.Click += (se, ev) =>
            {
                Session.Abandon();
                Session.RemoveAll();
                Response.Write("<script language=javascript> { var Backlen=history.length; history.go(-Backlen); window.location.replace(\"logoutpage.aspx\"); } </script>");                
                Response.Redirect("loginpage.aspx");
            };
            Button2.Click += (se, ev) =>
            {
                Response.Redirect("friends.aspx");
            };
            int j = 0;
            DataTable d1 = new DataTable();
            DataTable d2 = new DataTable();
            bool flag = false;
            int check = obj.GetNotif(userid, ref d1);
            int check2 = obj.CheckLastSeen_Notif(userid,ref flag, ref d2);
            if(check>0 && flag){
                for (int i = 0; i < d1.Rows.Count; i++) {
                    if (d2.Rows[0].Field<DateTime>(1).Subtract(d1.Rows[i].Field<DateTime>(2)).TotalMilliseconds < 0)
                    {
                        j++;
                    }
                }
                if (j > 0)
                {
                    un_notif.Text += j;
                    un_notif.Attributes.Add("style", "content:attr(data-badge); position: absolute; font-size:.9em; background: red; color: white; width: 22px; height: 22px; text-align:center; line-height:18px; border-radius:50%; box-shadow:0 0 1px #333;");
                }    
            }
        }

        protected void searchUserandHash(object sender, EventArgs e)
        {
                Session["searchstring"] = Textbox1.Text;
                Response.Redirect("SearchPage.aspx");
        }
    }
}