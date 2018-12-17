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
    public partial class signuppage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Error1.Text = "";
            Error2.Text = "";
            Error3.Text = "";
            Error4.Text = "";
            Error5.Text = "";
            Error6.Text = "";
        }

        protected void EnterUserButton(object sender, EventArgs e)
        {
            enter obj = new enter();
            String uname = Textbox1.Text;
            if(string.IsNullOrEmpty(uname))
            {
                Error1.Text = "Username cannot be null.";
                return;
            }
            String password = Textbox7.Text;
            bool check1 = false;
            bool check2 = false;
            if (string.IsNullOrEmpty(password))
            {
                Error2.Text = "Password cannot be null.";
                return;
            }
            else
            {
                for (int i = 0; i < password.Length; i++)
                {
                    if (password[i] >= '0' && password[i] <= '9')
                        check1 = true;
                    else if (password[i] >= 'A' && password[i] <= 'Z')
                        check2 = true;
                }
                if (check1 == false || check2 == false)
                {
                    Error2.Text = "You have given an invalid Password. Try again.";
                    return;
                }
            }
            String name = Textbox2.Text;
            if (string.IsNullOrEmpty(name))
            {
                Error3.Text = "Name cannot be null.";
                return;
            }
            String email = Textbox3.Text;
            if (string.IsNullOrEmpty(email))
            {
                Error4.Text = "Email cannot be null.";
                return;
            }
            String country = Textbox4.Text;
            if (string.IsNullOrEmpty(country))
            {
                Error5.Text = "Country cannot be null.";
                return;
            }
            String cellno = Textbox5.Text;
            String bdate = Textbox6.Text;
            if (string.IsNullOrEmpty(bdate))
            {
                Error6.Text = "Birth-date cannot be null.";
                return;
            }
            DataTable DT = new DataTable();
            int found;
            found = obj.EnterUser(uname,password,name,email,bdate,country,cellno,ref DT);
            if (found == -1)
                Error1.Text = "Sorry! Username already exists. Try again.";
            else
            {
                Response.Redirect("loginpage.aspx");
            }
        }
    }
}