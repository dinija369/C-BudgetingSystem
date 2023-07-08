using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class TeamSession
    {
        public void setSession(string department)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_session ([Department]) VALUES (@department)";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);

            command.ExecuteReader();
            connection.Close();
        }

        public void endSession()
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "DELETE FROM dbo.Team_session";
            SqlCommand command = new(query, connection);

            command.ExecuteReader();
            connection.Close();
        }

        public string getSession()
        {
            string department;
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "SELECT [Department] FROM dbo.Team_session";
            SqlCommand command = new(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();      
            department = reader.GetString(0);
            connection.Close();

            return department;
        }
    }
}
