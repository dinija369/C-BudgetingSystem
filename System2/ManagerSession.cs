using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class ManagerSession
    {
        public void setSession(string username)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Manager_session ([Username]) VALUES (@username)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);

            command.ExecuteReader();
            connection.Close();
        }

        public void endSession()
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "DELETE FROM dbo.Manager_session";
            SqlCommand command = new SqlCommand(query, connection);

            command.ExecuteReader();

            connection.Close();
        }

        public string getSession()
        {
            string username;
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Username] FROM dbo.Manager_session";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            username = reader.GetString(0);

            connection.Close();

            return username;
        }
    }
}
