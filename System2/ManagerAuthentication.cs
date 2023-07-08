using Application;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class ManagerAuthentication
    {
        private void CheckLogin(string username, string EnteredPassword)
        {
            ManagerSession session = new();
            string password;
            int updateDetails;
            int LoginExit = 0;

            do
            {
                try
                {

                    string connString = ConnectionString.Connection();
                    SqlConnection connection = new SqlConnection(connString);
                    connection.Open();
                    string query = "SELECT [Password] FROM dbo.Manager_login WHERE [Username] = @username";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    password = reader.GetString(0);

                    connection.Close();

                    if (password == EnteredPassword)
                    {
                        connection.Open();
                        query = "SELECT [Username] FROM dbo.Manager_login WHERE [Username] = @username";
                        command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@username", username);

                        reader = command.ExecuteReader();
                        reader.Read();
                        string usernameForSession = reader.GetString(0);
                        session.setSession(usernameForSession);
                        connection.Close();
                        LoginExit = 2;

                    }
                }

                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Wrong password and/ or username!\n| 1. Try again | 2. Exit |");
                    LoginExit = Convert.ToInt32(Console.ReadLine());
                    if (LoginExit == 1)
                    {
                        Login();
                        LoginExit = 2;
                    }
                    else
                    {
                        LoginExit = 2;
                        Environment.Exit(0);
                    }
                }
            } while (LoginExit != 2);
        }

        public void Login()
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
            //gets the username and user name length
            Console.WriteLine("Username* >> ");
            string username = Console.ReadLine();
            length = username.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter username >> ");
                username = Console.ReadLine();
                length = username.Length;
            }
            //passes the department, username and password parameters to the method that will save them in database
            CheckLogin(username, password);
        }
    }
}
