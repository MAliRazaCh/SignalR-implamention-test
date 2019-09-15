using signalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace signalR.Models
{
    public class notificationRepositery
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["newConnection"].ConnectionString;
        DatabaseEntities db = new DatabaseEntities();
        public List<notificationTable> GetAllMessages()
        {
            var data = new List<notificationTable>();

            var query = db.notificationTables as DbQuery<notificationTable>;
            string cmdText = query.ToString();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Notification = null;

            var dependency = new SqlDependency(cmd);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                notificationTable obj = new notificationTable();
                obj.Id = Convert.ToInt32(reader[0]);
                obj.name = Convert.ToString(reader[1]);
                obj.data = Convert.ToString(reader[2]);
                data.Add(obj);
            }
            return data;
        }
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                notificationHub.SendMessages();
            }
        }
    }
}