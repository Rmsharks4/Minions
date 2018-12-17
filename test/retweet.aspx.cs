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
    public partial class retweet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enter obj = new enter();
            int userid = (int)Session["userid"];
            int tweetid = (int)Session["retweetid"];
            ret.Attributes.Add("value",Convert.ToString(Session["retweetstr"]));
            btnSave.Text = "Post ReTweet";
            btnSave.Click += (se, ev) =>
            {
                int found = obj.InsertReTweet(tweetid,userid,ret.Text);
                if(found==-1)
                {
                    Label t = new Label();
                    t.ID = t.UniqueID;
                    t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                    t.BorderStyle = BorderStyle.Solid;
                    t.Text = "An error occured. Please Go Back to Previous Page and Try Again";
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(t);
                    ph.Controls.Add(new LiteralControl("<br /><br /><br />"));
                }
                else
                {
                    Label t = new Label();
                    t.ID = t.UniqueID;
                    t.Attributes.Add("style", "font-family: Times New Roman; font-size: 25px; background-color: #ffffff;");
                    t.BorderStyle = BorderStyle.Solid;
                    t.Text = "Your Retweet was posted successfully!";
                    ph.Controls.Add(new LiteralControl("<br />"));
                    ph.Controls.Add(t);
                    ph.Controls.Add(new LiteralControl("<br /><br /><br />"));
                    Response.Redirect("home.aspx?val=" + userid);
                }
            };
            
        }
    }
}