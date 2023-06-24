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
        public void setSession(string dep)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_session ([Department]) VALUES (@department)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            connection.Close();
        }

        public static void endSession()
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "DELETE FROM dbo.Team_session";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            connection.Close();
        }

        public static string getSession()
        {
            string dep;

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department] FROM dbo.Team_session";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            
            dep = reader.GetString(0);
            

            connection.Close();

            return dep;
        }
    }
}
