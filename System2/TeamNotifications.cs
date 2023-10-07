using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class TeamNotifications
    {
        TeamSession teamSession = new();
        public void getNotifications()
        {
            string department = teamSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "SELECT [Message], [Sender] FROM dbo.Team_notifications WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);

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

        private void setNotification(string username, string message, string department)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Manager_notifications ([Username], [Message], [Sender]) VALUES (@username, @message, @sender)";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@sender", department);

            command.ExecuteReader();
            connection.Close();
        }

        public void Notifications()
        {
            string department = teamSession.getSession();
            Console.WriteLine("Username >> ");
            //collects department name from user
            string username = Console.ReadLine();
            Console.WriteLine("Message >> ");
            string message = Console.ReadLine();
            setNotification(username, message, department);
        }
    }
}
