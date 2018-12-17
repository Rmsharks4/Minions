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
    public partial class loginpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchUserButton(object sender, EventArgs e)
        {
            enter obj = new enter();
            String Name = Textbox1.Text;
            String Password = Textbox2.Text;
            int found;
            found = obj.SearchUser(Name, Password);
            if (found == -1)
            {
                Error.Text = "Username or Password is incorrect. Try again.";
            }
            else
            {
                Session["userid"] = found;
                Session["username"] = '@' + Name;
                Session["Uname"] = Name;
                Response.Redirect("home.aspx?val=" + found);
            }
        }
    }
}