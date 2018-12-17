using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace test.Entry
{
    public class enter
    {
        private static readonly string connection =
       System.Configuration.ConfigurationManager.ConnectionStrings["test.Properties.Settings.connection"].ConnectionString;

        public int EnterUser(string uname, string password, string name, string email, string bdate, string country, string cellno, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("INSERT INTO [User] VALUES (@username,@password,@name,@email,@bdate,@country,@cellno) SELECT SCOPE_IDENTITY()", con); //name of your SQL procedure
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 30); //input of SQL stored procedure
                cmd.Parameters["@username"].Value = uname;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 16); //input of SQL stored procedure
                cmd.Parameters["@password"].Value = password;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 30); //input of SQL stored procedure
                cmd.Parameters["@name"].Value = name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50); //input of SQL stored procedure
                cmd.Parameters["@email"].Value = email;
                cmd.Parameters.Add("@bdate", SqlDbType.Date, 10); //input of SQL stored procedure
                cmd.Parameters["@bdate"].Value = bdate;
                cmd.Parameters.Add("@country", SqlDbType.VarChar, 20); //input of SQL stored procedure
                cmd.Parameters["@country"].Value = country;
                cmd.Parameters.Add("@cellno", SqlDbType.Char, 11); //input of SQL stored procedure
                cmd.Parameters["@cellno"].Value = cellno;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int GetMsgs(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("GetAllmsgs", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int checkLastOnline(int userid, ref bool check)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand c;
            int x = -1;
            try
            {
                c = new SqlCommand("SELECT * FROM LastOnline WHERE userid=@userid", con);
                c.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@userid"].Value = userid;
                var r = c.ExecuteReader();
                if (r.HasRows)
                {
                    check = true;
                    x = 1;
                }
                else
                {
                    check = false;
                    x = 1;
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int LastOnline(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            bool check = false;
            int x = -1;
            try
            {
                x = checkLastOnline(userid,ref check);
                if (check == true)
                {
                    cmd = new SqlCommand("UPDATE LastOnline SET [time]=@time WHERE userid=@userid", con); //name of your SQL procedure
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO LastOnline VALUES (@userid,@time)", con); //name of your SQL procedure
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int GetLatest(int user1, int user2, ref DataTable d)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("select top(1) msg,[time] from [Message] where user1id=@user1id and user2id=@user2id order by [time] desc", con); //name of your SQL procedure
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = user1;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    d.Load(reader);
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int GetLastSeen(int user1, int user2, ref DateTime t)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("select [time] from LastSeen where user1id=@user1id and user2id=@user2id", con); //name of your SQL procedure
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = user1;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2;
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    DataTable d = new DataTable();
                    d.Load(reader);
                    t = d.Rows[0].Field<DateTime>(0);
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int InsertMessage(int user1id, int user2id, string msg, DateTime time)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("insert into [Message] Values(@user1id,@user2id,@msg,@time)", con); //name of your SQL procedure
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = user1id;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2id;
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 150);
                cmd.Parameters["@msg"].Value = msg;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = time;
                var reader = cmd.ExecuteReader();
                x = UpdateLastSeen(user1id, user2id);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int UpdateLastSeen(int userid, int user2)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            bool check = false;
            int x = -1;
            try
            {
                x = CheckLastSeen(userid, user2, ref check);
                if (check == true)
                {
                    cmd = new SqlCommand("UPDATE LastSeen SET [time]=@time WHERE user1id=@user1id AND user2id=@user2id", con); //name of your SQL procedure
                    cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user1id"].Value = userid;
                    cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user2id"].Value = user2;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO LastSeen VALUES (@user1id,@user2id,@time)", con); //name of your SQL procedure
                    cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user1id"].Value = userid;
                    cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user2id"].Value = user2;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int InsertMessage(int userid, int user2, string text)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("insert into [Message] Values(@user1id,@user2id,@msg,@time)", con); //name of your SQL procedure
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = userid;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2;
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 150);
                cmd.Parameters["@msg"].Value = text;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = DateTime.Now;
                var reader = cmd.ExecuteReader();
                x = UpdateLastSeen(userid, user2);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        internal int EditUser(int userid, string name, string username, string email, string bdate, string country, string cellno, string password)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand c;
            int x = -1;
            try
            {
                c = new SqlCommand("Update [User] set name=@name, username=@username, email = @email, bdate = @bdate, country = @country, cellno = @cellno, password = @password where userid = @userid", con);
                c.Parameters.Add("@name", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@name"].Value = name;
                c.Parameters.Add("@username", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@username"].Value = username;
                c.Parameters.Add("@email", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@email"].Value = email;
                c.Parameters.Add("@bdate", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@bdate"].Value = bdate;
                c.Parameters.Add("@country", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@country"].Value = country;
                c.Parameters.Add("@cellno", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@cellno"].Value = cellno;
                c.Parameters.Add("@password", SqlDbType.VarChar); //input of SQL stored procedure
                c.Parameters["@password"].Value = password;
                c.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@userid"].Value = userid;
                var r = c.ExecuteReader();
                if (r.HasRows)
                {
                    x = 1;
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;

        }

        public int CheckLastSeen(int userid, int user2, ref bool check)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand c;
            int x = -1;
            try
            {
                c = new SqlCommand("SELECT * FROM LastSeen WHERE user1id=@user1id AND user2id=@user2id", con);
                c.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@user1id"].Value = userid;
                c.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@user2id"].Value = user2;
                var r = c.ExecuteReader();
                if (r.HasRows)
                {
                    check = true;
                    x = 1;
                }
                else
                {
                    check = false;
                    x = 1;
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int InsertReTweet(int tweetid, int userid, string text)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("insertReTweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                cmd.Parameters.Add("@stweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@stweetid"].Value = tweetid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar, 150);
                cmd.Parameters["@tweetstr"].Value = text;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = DateTime.Now;
                var reader = cmd.ExecuteReader();
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int InsertTweet(int userid, string text)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("insert into Tweet Values(@userid,@tweetstr,@time)", con); //name of your SQL procedure
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar, 150);
                cmd.Parameters["@tweetstr"].Value = text;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = DateTime.Now;
                var reader = cmd.ExecuteReader();
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }


        public string getName(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            string name=null;
            try
            {
                cmd = new SqlCommand("SELECT name FROM [User] WHERE userid = @userid", con); //name of your SQL procedure
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                DataTable result = new DataTable();
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        name = result.Rows[0].Field<string>(0);
                con.Close();
            }
            catch (SqlException)
            {
                name = null; //if any erron return 0
            }
            finally
            {
                con.Close();
            }
            return name;
        }

        public int SearchUser(string Name, string Password)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("SELECT userid FROM [User] WHERE username = @name COLLATE Latin1_General_CS_AS AND password = @password COLLATE Latin1_General_CS_AS", con); //name of your SQL procedure
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 30); //input of SQL stored procedure
                cmd.Parameters["@name"].Value = Name;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 16); //input of SQL stored procedure
                cmd.Parameters["@password"].Value = Password;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if(result != null)
                    if(result.Rows.Count>0)
                    x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1; //if any erron return 0
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public string GetUser(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            string end = null;
            try
            {
                cmd = new SqlCommand("SELECT username FROM [User] WHERE userid = @userid", con); //name of your SQL procedure
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null && result.Rows.Count>0)
                    end = result.Rows[0].Field<string>(0);
                con.Close();
            }
            catch (SqlException)
            {
                end = null;
            }
            finally
            {
                con.Close();
            }
            return end;
        }

        public int GetTweets(int userid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("Profile", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int GetUserID(string username)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {

                cmd = new SqlCommand("SELECT userid FROM [User] WHERE username = @username", con); //name of your SQL procedure
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 30); //input of SQL stored procedure
                cmd.Parameters["@username"].Value = username;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int readTweet(int tweetid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("SELECT tweetstr,[time] FROM Tweet WHERE tweetid = @tweetid", con); //name of your SQL procedure
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = tweetid;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int editTweet(int tweetid, string text)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = 1;
            try
            {
                cmd = new SqlCommand("UPDATE Tweet set tweetstr=@tweetstr where tweetid=@tweetid", con); //name of your SQL procedure
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar, 150); //input of SQL stored procedure
                cmd.Parameters["@tweetstr"].Value = text;
                var reader = cmd.ExecuteReader();
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int checkifcollowing(int user1id, int user2id,ref bool check)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            DataTable result = new DataTable();
            
            try
            {
                cmd = new SqlCommand("SELECT * FROM Follows WHERE user1id=@user1id and user2id=@user2id ", con); //name of your SQL procedure
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = user1id;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2id;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        check = true;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int numOfLikes(int userid, int tweetid, ref bool checklike)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlConnection d = new SqlConnection(connection);
            con.Open();
            d.Open();
            SqlCommand cmd;
            SqlCommand c;
            int x = -1;
            try
            {
                c = new SqlCommand("SELECT * FROM [ like ] WHERE userid=@userid AND tweetid=@tweetid", d);
                c.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@userid"].Value = userid;
                c.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                c.Parameters["@tweetid"].Value = tweetid;
                c.Parameters.Add("@time", SqlDbType.DateTime, 32);
                c.Parameters["@time"].Value = DateTime.Now;
                var r = c.ExecuteReader();
                if (r.HasRows)
                {
                    checklike = true;
                }
                else
                {
                    checklike = false;
                }
                d.Close();
                cmd = new SqlCommand("NoOflikes", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int numOfcomments(int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("NoOfComments", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int numOfretweets(int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("NoOfRetweets", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int newsfeed(int userid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("NewsFeed", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int EnterLike(int userid, int tweetid, ref bool ret)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                x = numOfLikes(userid, tweetid, ref ret);
                if(ret == true)
                {
                    cmd = new SqlCommand("DELETE FROM [ like ] WHERE userid=@userid AND tweetid=@tweetid", con); //name of your SQL procedure
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@tweetid"].Value = tweetid;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                    x--;
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO [ like ] VALUES (@userid,@tweetid,@time)", con); //name of your SQL procedure
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@tweetid"].Value = tweetid;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                    x++;
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int enterfollow(int user1id, int user2id, ref bool ret)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
               x= checkifcollowing(user1id, user2id,  ref ret);
                if (ret == true)
                {
                    cmd = new SqlCommand("DELETE FROM Follows WHERE user1id=@user1id AND user2id=@user2id", con); //name of your SQL procedure
                    cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user1id"].Value = user1id;
                    cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user2id"].Value = user2id;
                   
                    var reader = cmd.ExecuteReader();
                    
                }
                else
                {
                    cmd = new SqlCommand("Insert into Follows values( @user1id,@user2id,@time)", con); //name of your SQL procedure
                    cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user1id"].Value = user1id;
                    cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                    cmd.Parameters["@user2id"].Value = user2id;
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                    cmd.Parameters["@time"].Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                  
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int GetComments(int tweetid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("SELECT commentid,tweetstr,userid,[time] FROM Tweet JOIN Comment ON Tweet.tweetid = Comment.commentid WHERE Comment.stweetid = @tweetid order by [time]", con); //name of your SQL procedure
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int MsgPreview(int userid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("MsgPreview", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int Convo(int user1id, int user2id, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("ViewMessages", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user1id"].Value = user1id;
                cmd.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@user2id"].Value = user2id;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }

        public int InsertComment(int userid, int tweetid, string tweetstr)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("insertComment", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                cmd.Parameters.Add("@stweetid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@stweetid"].Value = tweetid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar, 150);
                cmd.Parameters["@tweetstr"].Value = tweetstr;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = DateTime.Now;
                var reader = cmd.ExecuteReader();
                DataTable d = new DataTable();
                d.Load(reader);
                if (d != null)
                    if (d.Rows.Count > 0)
                        x = d.Rows[0].Field<int>(0); 
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int search(string s, ref DataTable result, ref DataTable DT)//copy this function
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                if (s[0] == '#')
                {

                    cmd = new SqlCommand("SELECT tweetid,hashstr FROM hashtag WHERE hashstr like @hashstr+'%' ", con); //name of your SQL procedure
                    cmd.Parameters.Add("@hashstr", SqlDbType.VarChar, 30); //input of SQL stored procedure
                    cmd.Parameters["@hashstr"].Value = s;
                    var reader = cmd.ExecuteReader();
                    result.Load(reader);
                    if (result != null)
                        if (result.Rows.Count > 0)
                            x = 1;
                    cmd = new SqlCommand("SELECT hashstr,SUM(occurences) as counts FROM hashtag WHERE hashstr like @hashstr+'%' group by hashstr order by counts desc", con); //name of your SQL procedure
                    cmd.Parameters.Add("@hashstr", SqlDbType.VarChar, 30); //input of SQL stored procedure
                    cmd.Parameters["@hashstr"].Value = s;
                    var reader2 = cmd.ExecuteReader();
                    DT.Load(reader2);
                    if (DT != null)
                        if (DT.Rows.Count > 0)
                            x = 1;
                    con.Close();
                }
                else
                {

                    cmd = new SqlCommand("SELECT username FROM [User] WHERE username like '%'+@username+'%' ", con); //name of your SQL procedure
                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 30); //input of SQL stored procedure
                    cmd.Parameters["@username"].Value = s;
                    var reader = cmd.ExecuteReader();
                    result.Load(reader);
                    if (result != null)
                        if (result.Rows.Count > 0)
                            x = 1;
                    con.Close();

                }
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;

        }
        public DataSet GetAllUsers()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("select * from [User]", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.Text; //set type of sqL Command
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public int DeleteUser(string userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            //int result = 0;
            try
            {
                cmd = new SqlCommand("DeleteUser_procedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return -1;
            }
            finally
            {
                con.Close();
            }

            return 1;
        }
        public int UpdateUsers(int userid, string username, string password, string name, string email, string bdate, string country, string cellno)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int result = 0;
            try
            {
                cmd = new SqlCommand("EditUser_procedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@bdate", SqlDbType.Date).Value = bdate;
                cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = country;
                cmd.Parameters.Add("@cellno", SqlDbType.Char).Value = cellno;

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return result;
        }
        public DataSet GetAllTweets()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("select * from Tweet", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.Text; //set type of sqL Command
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }

        public int DeleteTweet(string tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            //int result = 0;
            try
            {
                cmd = new SqlCommand("DeleteTweet_procedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int).Value = tweetid;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return -1;
            }
            finally
            {
                con.Close();
            }

            return 1;
        }
        public int UpdateTweet(int tweetid, string tweetstr, string time)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int result = 0;
            try
            {
                cmd = new SqlCommand("EditTweet_procedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int).Value = tweetid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar).Value = tweetstr;
                cmd.Parameters.Add("@time", SqlDbType.DateTime).Value = time;
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        public string GetATweet(int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            string x = null;
            try
            {
                cmd = new SqlCommand("select tweetstr from Tweet where tweetid=@tweetid", con); //name of your SQL procedure
                cmd.Parameters.Add("@tweetid", SqlDbType.Int);
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<string>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int GetUserIDfromTweet(int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("select userid from Tweet where tweetid=@tweetid", con); //name of your SQL procedure
                cmd.Parameters.Add("@tweetid", SqlDbType.Int);
                cmd.Parameters["@tweetid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                if (result != null)
                    if (result.Rows.Count > 0)
                        x = result.Rows[0].Field<int>(0);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int GetFollowingID(int userid, ref DataTable DT)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("[Following]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int);
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                DT.Load(reader);
                if (DT != null)
                    if (DT.Rows.Count > 0)
                        x = 1;
                con.Close();
            }
            catch (SqlException)
            {
                x = -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int GetFollowerID(int userid, ref DataTable DT)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("Followers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int);
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                DT.Load(reader);
                if (DT != null)
                    if (DT.Rows.Count > 0)
                        x = 1;
                con.Close();
            }
            catch (SqlException)
            {
                x = -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int GetNotif(int userid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("[ notification]", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                if (result != null)
                    x = result.Rows.Count;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
		        public int CheckLastSeen_Notif(int userid, ref bool check, ref DataTable d)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand c;
            int x = -1;
            try
            {
                c = new SqlCommand("find_lastseen_notif", con);
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.Add("@userid", SqlDbType.Int, 32).Value = userid; //input of SQL stored procedure
                var reader = c.ExecuteReader();
                d.Load(reader);
                var r = c.ExecuteReader();
                if (r.HasRows)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
                x = 1;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
        public int UpdateLastSeen_Notif(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            DataTable temp = new DataTable();
            bool check = false;
            int x = -1;
            try
            {
                x = CheckLastSeen_Notif(userid, ref check, ref temp);
                if (check == true)
                {
                    cmd = new SqlCommand("update_lastseen_notif", con); //name of your SQL procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32).Value = userid;//input of SQL stored procedure
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32).Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                else
                {
                    cmd = new SqlCommand("insert_lastseen_notif", con); //name of your SQL procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32).Value = userid; //input of SQL stored procedure
                    cmd.Parameters.Add("@time", SqlDbType.DateTime, 32).Value = DateTime.Now;
                    var reader = cmd.ExecuteReader();
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
		
		public int InsertTweet(int userid, string text, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("insert into Tweet Values(@userid,@tweetstr,@time) SELECT SCOPE_IDENTITY()", con); //name of your SQL procedure
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32); //input of SQL stored procedure
                cmd.Parameters["@userid"].Value = userid;
                cmd.Parameters.Add("@tweetstr", SqlDbType.VarChar, 150);
                cmd.Parameters["@tweetstr"].Value = text;
                cmd.Parameters.Add("@time", SqlDbType.DateTime, 32);
                cmd.Parameters["@time"].Value = DateTime.Now;
                var reader = cmd.ExecuteReader();
                result.Load(reader);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

		public int Add_Image(string FileName, int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlConnection d = new SqlConnection(connection);
            con.Open();
            d.Open();
            SqlCommand cmd;
            SqlCommand c;
            try
            {
                c = new SqlCommand("select * from Images where tweetid = @tweetid", d);
                c.Parameters.Add("@tweetid", SqlDbType.Int, 32);
                c.Parameters["@tweetid"].Value = tweetid;
                var reader = c.ExecuteReader();
                if (reader.HasRows)
                {
                    d.Close();
                    cmd = new SqlCommand("update Images set imagepath=@imagepath where tweetid=@tweetid", con);
                    cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32);
                    cmd.Parameters["@tweetid"].Value = tweetid;
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarChar, 200);
                    cmd.Parameters["@imagepath"].Value = FileName;
                    cmd.ExecuteReader();
                }
                else
                {
                    d.Close();
                    cmd = new SqlCommand("insert into Images Values(@tweetid,@imagepath)", con);
                    cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32);
                    cmd.Parameters["@tweetid"].Value = tweetid;
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarChar, 200);
                    cmd.Parameters["@imagepath"].Value = FileName;
                    cmd.ExecuteReader();
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        } 

        public int AddProfilePic(string Filename, int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlConnection d = new SqlConnection(connection);
            con.Open();
            d.Open();
            SqlCommand cmd;
            SqlCommand c;
            try
            {
                c = new SqlCommand("select * from ProfilePic where userid = @userid", d);
                c.Parameters.Add("@userid", SqlDbType.Int, 32);
                c.Parameters["@userid"].Value = userid;
                var reader = c.ExecuteReader();
                if (reader.HasRows)
                {
                    d.Close();
                    cmd = new SqlCommand("update ProfilePic set imagepath=@imagepath where userid=@userid", con);
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32);
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarChar, 200);
                    cmd.Parameters["@imagepath"].Value = Filename;
                    cmd.ExecuteReader();
                }
                else
                {
                    d.Close();
                    cmd = new SqlCommand("insert into ProfilePic Values(@userid,@imagepath)", con);
                    cmd.Parameters.Add("@userid", SqlDbType.Int, 32);
                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarChar, 200);
                    cmd.Parameters["@imagepath"].Value = Filename;
                    cmd.ExecuteReader();
                }
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int RemoveProfilePic(int userid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("delete from ProfilePic where userid = @userid", con);
                //  cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32);
                cmd.Parameters["@userid"].Value = userid;
                cmd.ExecuteReader();
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int Remove_Image(int tweetid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("delete from Images where tweetid = @tweetid", con);
                //  cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tweetid", SqlDbType.Int, 32);
                cmd.Parameters["@tweetid"].Value = tweetid;
                cmd.ExecuteReader();
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 1;
        }

        public int Get_Image(int tweetid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            bool flag = false;
            try
            {
                cmd = new SqlCommand("select imagepath from Images where tweetid=@tid", con);
                //  cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tid", SqlDbType.Int, 32);
                cmd.Parameters["@tid"].Value = tweetid;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    flag = true;
                result.Load(reader);
                con.Close(); 
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }

            if (flag)
                return 1;
            else
                return -1;
        }

        public int GetUserImage(int userid, ref DataTable result)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            bool flag = false;
            try
            {
                cmd = new SqlCommand("select imagepath from ProfilePic where userid=@userid", con);
                //  cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int, 32);
                cmd.Parameters["@userid"].Value = userid;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    flag = true;
                result.Load(reader);
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }

            if (flag)
                return 1;
            else
                return -1;
        }

        public int GetInformation(int id, ref DataTable DT)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd;
            int x = -1;
            try
            {
                cmd = new SqlCommand("select name,username,email,bdate,country,cellno,password from [User] where userid=@id", con); //name of your SQL procedure
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                var reader = cmd.ExecuteReader();
                DT.Load(reader);
                if (DT != null)
                    if (DT.Rows.Count > -1)
                        x = 1;
                con.Close();
            }
            catch (SqlException)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return x;
        }
    }
}