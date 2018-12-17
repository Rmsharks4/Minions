using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using test.Entry;

namespace test
{
    public partial class adminpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGrid();
        }
        public void loadGrid()
        {
            //this function will load the data into grid
            enter obj = new enter();
            modifyUser.DataSource = obj.GetAllUsers();
            modifyUser.DataBind();
            modifyTweet.DataSource = obj.GetAllTweets();
            modifyTweet.DataBind();
        }
        protected void User_RowDelete_Click(Object sender, GridViewDeleteEventArgs e)
        {
            // Retrieve the row that raised the event from the Rows
            // collection of the GridView control.
            GridViewRow row = modifyUser.Rows[e.RowIndex];
            //get roll number from that row
            String userid = row.Cells[1].Text;

            //Call the DAL function to delete the student with this roll number 
            enter obj = new enter();
            if (obj.DeleteUser(userid) == 1)
            {
                Message.Text = "UserID =" + userid + " was Deleted";
                loadGrid(); //reload the grid to show the modifications in table
            }
            else
                Message.Text = "there was some error";
        }
        protected void User_RowEdit_Click(Object sender, GridViewEditEventArgs e)
        {

            //get the index of the row you ewant to delete and load the grid
            modifyUser.EditIndex = e.NewEditIndex;
            loadGrid();


        }
        protected void User_RowEditCancel_Click(object sender, GridViewCancelEditEventArgs e)
        {

            // Retrieve the row that raised the event from the Rows
            // collection of the GridView control.
            GridViewRow row = modifyUser.Rows[e.RowIndex];

            // The update operation was canceled. Display the 
            // primary key of the row. In this example, the primary
            // key is displayed in the second column of the GridView
            // control. To access the text of the column, use the Cells
            // collection of the row.
            String message = "Update for UserID = " + row.Cells[1].Text + " Canceled.";
            //reload the grid and restore the data
            modifyUser.EditIndex = -1;
            loadGrid();
            Message.Text = message;
        }
        protected void User_RowUpdate_Click(Object sender, GridViewUpdateEventArgs e)
        {

            // Retrieve the row that raised the event from the Rows

            GridViewRow row = modifyUser.Rows[e.RowIndex];


            //retrieve roll number from that row (key-non editable)
            int Userid = Convert.ToInt32(row.Cells[1].Text.ToString());
            //get the new values from that row
            string NewUsername = e.NewValues["username"].ToString();
            string NewPassword = e.NewValues["password"].ToString();
            string NewName = e.NewValues["name"].ToString();
            string NewEmail = e.NewValues["email"].ToString();
            string NewBdate = e.NewValues["bdate"].ToString();
            string NewCountry = e.NewValues["country"].ToString();
            string NewCellno = e.NewValues["cellno"].ToString();

            Message.Text = Userid + NewUsername + NewPassword + NewEmail + NewBdate + NewCountry + NewCellno;

            //=====updating the newly entered values in database====
            enter obj = new enter();
            int result = obj.UpdateUsers(Userid, NewUsername, NewPassword, NewName, NewEmail, NewBdate, NewCountry, NewCellno);
            //reload the page======================================================
            modifyUser.EditIndex = -1;
            loadGrid();
            Message.Text = "UserID = " + Userid + "'s info updated";

        }
        /////////////////////////////
        protected void Tweet_RowDelete_Click(Object sender, GridViewDeleteEventArgs e)
        {
            // Retrieve the row that raised the event from the Rows
            // collection of the GridView control.
            GridViewRow row = modifyTweet.Rows[e.RowIndex];
            //get roll number from that row
            String tweetid = row.Cells[1].Text;

            //Call the DAL function to delete the student with this roll number 
            enter obj = new enter();
            if (obj.DeleteTweet(tweetid) == 1)
            {
                Message.Text = "TweetID =" + tweetid + " was Deleted";
                loadGrid(); //reload the grid to show the modifications in table
            }
            else
                Message.Text = "there was some error";
        }
        protected void Tweet_RowEdit_Click(Object sender, GridViewEditEventArgs e)
        {

            //get the index of the row you ewant to delete and load the grid
            modifyTweet.EditIndex = e.NewEditIndex;
            loadGrid();


        }
        protected void Tweet_RowEditCancel_Click(object sender, GridViewCancelEditEventArgs e)
        {

            // Retrieve the row that raised the event from the Rows
            // collection of the GridView control.
            GridViewRow row = modifyTweet.Rows[e.RowIndex];

            // The update operation was canceled. Display the 
            // primary key of the row. In this example, the primary
            // key is displayed in the second column of the GridView
            // control. To access the text of the column, use the Cells
            // collection of the row.
            String message = "Update for TweetID = " + row.Cells[1].Text + " Canceled.";
            //reload the grid and restore the data
            modifyTweet.EditIndex = -1;
            loadGrid();
            Message.Text = message;
        }
        protected void Tweet_RowUpdate_Click(Object sender, GridViewUpdateEventArgs e)
        {

            // Retrieve the row that raised the event from the Rows

            GridViewRow row = modifyTweet.Rows[e.RowIndex];


            //retrieve roll number from that row (key-non editable)
            int Tweetid = Convert.ToInt32(row.Cells[1].Text.ToString());
            //get the new values from that row
            string NewTweetstr = e.NewValues["tweetstr"].ToString();
            string NewTime = e.NewValues["time"].ToString();
            
            Message.Text = Tweetid + NewTweetstr + NewTime;

            //=====updating the newly entered values in database====
            enter obj = new enter(); 
            int result = obj.UpdateTweet(Tweetid,NewTweetstr,NewTime);
            //reload the page======================================================
            modifyUser.EditIndex = -1;
            loadGrid();
            Message.Text = "TweetID = " + Tweetid + "'s info updated";

        }
        
    }
}