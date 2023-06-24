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
        public static void getNotifications()
        {
            string dep = TeamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Message], [Sender] FROM dbo.Team_notifications WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("Sender: {0, -40}\n", reader.GetString(1));
                Console.WriteLine("________________________");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Message: {0, -40}\n", reader.GetString(0));
                Console.WriteLine("|________________________________________|");
            }
        }

        public void setNotification(string username, string message, string dep)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Manager_notifications ([Username], [Message], [Sender]) VALUES (@username, @message, @sender)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@sender", dep);

            SqlDataReader reader = command.ExecuteReader();
        }

        public void Notifications()
        {
            string dep = TeamSession.getSession();
            //todo - check if the username exists
            Console.WriteLine("Username >> ");
            //collects department name from user
            string username = Console.ReadLine();
            Console.WriteLine("Message >> ");
            string message = Console.ReadLine();
            setNotification(username, message, dep);
        }
    }
}
