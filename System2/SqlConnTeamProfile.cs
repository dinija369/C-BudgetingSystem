using System;
using System.Data.SqlClient;

namespace Application
{
    public class SqlConnTeamProfile

        //todo - merge login and profile and make the profile options possible to be null
    {
        private static void setTeamProfile(string dep, string sup)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            //string query = "SELECT * FROM dbo.users";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //
            string query = "INSERT INTO dbo.Team_profile ([Department], [Supervisor]) VALUES (@dep, @sup)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@sup", sup);

            SqlDataReader reader = command.ExecuteReader();

            /*while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1));
            }*/
        }

        public static void TeamProfile()
        {

            Console.WriteLine("\n>> Create a team <<\n");
            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();
            //gets the length of the department and keeps repeating the wile loop if the length is less than two
            int length = dep.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter department >> ");
                dep = Console.ReadLine();
                //if length is two or more exits the while loop
                length = dep.Length;
            }
            //collects the suprevisor name for the department
            Console.WriteLine("Supervisor >> ");
            string sup = Console.ReadLine();

            //passes the department and supervisor parameters to the method that will save them in database
            setTeamProfile(dep, sup);
        }

        private static void SetTeamLogin(string dep, string use, string pass)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_login ([Department], [Username], [Password]) VALUES (@dep, @use, @pass)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@use", use);
            command.Parameters.AddWithValue("@pass", pass);

            SqlDataReader reader = command.ExecuteReader();

            /*while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2));
            }*/
        }

        public static void TeamLogin()
        {
            //gets passord from the user
            Console.WriteLine("\n>> Please enter Username and password <<");
            Console.WriteLine("\nPassword* (at least 2 characters) >> ");
            //collects the password
            string pass = Console.ReadLine();
            //gets the length of the password
            int length = pass.Length;
            //while loop checks if the password length is more than 2 and keeps looping if it is less
            while (length < 2)
            {
                //checks the password length again for the while loop
                Console.WriteLine("*** Passsword is too short ***\nPlease try again >> ");
                pass = Console.ReadLine();
                length = pass.Length;
            }
            //gets the username and user name length
            Console.WriteLine("Username* >> ");
            string use = Console.ReadLine();
            length = use.Length;
            //while loop checks if the username length is more than 2 and keeps looping if it is less
            while (length < 2)
            {
                //checks the username length again for the while loop
                Console.WriteLine("*** Please enter username >> ");
                use = Console.ReadLine();
                length = use.Length;
            }

            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();

            //passes the department, username and password parameters to the method that will save them in database
            SetTeamLogin(dep, use, pass);
        }
        
    }


}
