using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class ManagerNotifications
    {
        ManagerSession managerSession = new();
        public void getNotifications()
        {
            string username = managerSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Message], [Sender] FROM dbo.Manager_notifications WHERE [Username] = @username";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Sender: {0, -40}", reader.GetString(1));
                Console.WriteLine("|----------------------------------------| ");
                Console.WriteLine("|Message:");
                Console.WriteLine("|----------");
                Console.WriteLine("  {0, -40}", reader.GetString(0));
            }
            connection.Close();
        }

        private void setNotification(string department, string message, string username)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_notifications ([Department], [Message], [Sender]) VALUES (@department, @message, @sender)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@sender", username);

            command.ExecuteReader();
            connection.Close();
        }

        public void Notifications()
        {
            string username = managerSession.getSession();
            //todo - check if the username exists
            Console.WriteLine("Department >> ");
            //collects department name from user
            string department = Console.ReadLine();
            Console.WriteLine("Message >> ");
            string message = Console.ReadLine();
            setNotification(department, message, username);
        }
    }
}
