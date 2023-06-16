using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class Session
    {
        public void setSession(string dep)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Session ([Department]) VALUES (@department)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            connection.Close();
        }

        public static void endSession()
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "DELETE FROM dbo.Session";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            connection.Close();
        }

        public static string getSession()
        {
            string dep;

            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department] FROM dbo.Session";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            
            dep = reader.GetString(0);
            

            connection.Close();

            return dep;
        }
    }
}
