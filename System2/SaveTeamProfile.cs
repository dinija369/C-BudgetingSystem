using System;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System2;

namespace Application
{
    public class SaveTeamProfile
    {
        TeamSession teamSession = new TeamSession();
       

        private void setTeamProfile(string department, string supervisor)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_profile ([Department], [Supervisor]) VALUES (@department, @supervisor)";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@supervisor", supervisor);

            try
            {
                command.ExecuteReader();
            } catch (SqlException) 
            { 
                Console.WriteLine("Department is already taken\nPlease choose a different one\n>>  ");
                RegisterProfile();
            }
            connection.Close();
        }

        public void RegisterProfile()
        {
            Console.WriteLine("\n>> Create a team <<\n");
            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();
            int length = dep.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter department >> ");
                dep = Console.ReadLine();
                length = dep.Length;
            }
            //collects the suprevisor name for the department
            Console.WriteLine("Supervisor >> ");
            string sup = Console.ReadLine();
            teamSession.setSession(dep);
            //passes the department and supervisor parameters to the method that will save them in database
            setTeamProfile(dep, sup);
        }

        private void SetTeamLogin(string department, string username, string password)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Team_login ([Department], [Username], [Password]) VALUES (@department, @username, @password)";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteReader();
            connection.Close();

        }

        public void RegisterLogin()
        {
            //gets password from the user
            Console.WriteLine("\n>> Please enter Username and password <<");
            Console.WriteLine("\nPassword* (at least 2 characters) >> ");
            string password = Console.ReadLine();
            int length = password.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Passsword is too short ***\nPlease try again >> ");
                password = Console.ReadLine();
                length = password.Length;
            }
            //gets the username
            Console.WriteLine("Username* >> ");
            string username = Console.ReadLine();
            length = username.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter username >> ");
                username = Console.ReadLine();
                length = username.Length;
            }
            string department = teamSession.getSession();
            //passes the department, username and password parameters to the method that will save them in database
            SetTeamLogin(department, username, password);
        }
    }
}
