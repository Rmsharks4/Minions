using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using test.Entry;

namespace test.Entry
{
    public class convo
    {
        [WebMethod]
        public static void GetMsg(int user1id, int user2id, DateTime time, ref Message m)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test.Properties.Settings.connection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  user1id,user2id,msg,[time] FROM [Message] where user1id = @user1id and user2id = @user2id and [time] > @time", connection))
                {
                    command.Parameters.Add("@user1id", SqlDbType.Int, 32); //input of SQL stored procedure
                    command.Parameters["@user1id"].Value = user1id;
                    command.Parameters.Add("@user2id", SqlDbType.Int, 32); //input of SQL stored procedure
                    command.Parameters["@user2id"].Value = user2id;
                    command.Parameters.Add("@time", SqlDbType.DateTime, 32); //input of SQL stored procedure
                    command.Parameters["@time"].Value = time;
                    
                    command.Notification = null;
                    SqlDependency.Start(ConfigurationManager.ConnectionStrings["test.Properties.Settings.connection"].ConnectionString);
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable d = new DataTable();
                            d.Load(reader);
                            m.user1id = d.Rows[0].Field<int>(0);
                            m.user2id = d.Rows[0].Field<int>(1);
                            m.msg = d.Rows[0].Field<string>(2);
                            m.time = d.Rows[0].Field<DateTime>(3);
                        }
                        else
                        {
                            m.user1id = -1;
                        }
                    }
                }
            }
        }
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            MyHub.Show();
        }
    }
}