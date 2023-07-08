using System;
using System.Data.SqlClient;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
using System2;

namespace Application
{
	public class ManagerProfile
	{
        SwitchCaseNavigation switchCaseNavigation = new();
        ManagerSession managerSession = new();

        private string password;
        private string username;
        int updateDetails;

        public void getManagerProfile()
        {
            string username = managerSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Username], [Name], [Surname], [Email], [Phone] FROM dbo.Manager_profile WHERE [Username] = @username";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Username:   {0, 4}", reader.GetString(0));
                Console.WriteLine("|----------------------------------------|");
                Console.WriteLine("|Name:       {0, 4}", reader.GetString(1));
                Console.WriteLine("|----------------------------------------| ");
                Console.WriteLine("|Surname:    {0, 4}", reader.GetString(2));
                Console.WriteLine("|----------------------------------------| ");
                Console.WriteLine("|Email:      {0, 4}", reader.GetString(3));
                Console.WriteLine("|----------------------------------------| ");
                Console.WriteLine("|Phone:      {0, 4}", reader.GetString(4));
                Console.WriteLine("|________________________________________|");
            }
            connection.Close();
        }

        private void setManagerLogin(string username, string password)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Manager_login ([Username], [Password]) VALUES (@username, @password)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteReader();
            connection.Close();
        }

        //prompts the user to enter password and username
        public void ManagerLogin()
		{
            Console.WriteLine("\n>> Please enter Username and password <<");
            Console.WriteLine("\nPassword* (at least 2 characters) >> ");
            password = Console.ReadLine();
            int length = password.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Passsword is too short ***\nPlease try again >> ");
                password = Console.ReadLine();
                length = password.Length;
            }
            Console.WriteLine("Username* >> ");
            username = Console.ReadLine();
            length = username.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter username >> ");
                username = Console.ReadLine();
                length = username.Length;
            }
            managerSession.setSession(username);
            setManagerLogin(username, password);
        }

        private void setManagerProfile(string username, string name, string surname, string email, string phone)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Manager_profile ([Username], [Name], [Surname], [Email], [Phone]) VALUES (@username, @name, @surname, @email, @phone)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@phone", phone);

            try
            {
                command.ExecuteReader();
            }
            catch (SqlException)
            {
                Console.WriteLine("Username is already taken\nPlease choose a different one\n>>  ");
                managerProfile();
            }
            connection.Close();
        }

        public void managerProfile()
        {
            Console.WriteLine("\n>> Create an account. <<\n");
            Console.WriteLine("Name* >> ");
            string name = Console.ReadLine();
            int length = name.Length;
            while (length < 1)
            {
                Console.WriteLine("\nPlease enter your name >> ");
                name = Console.ReadLine();
                length = name.Length;
            }
            //collects surname
            Console.WriteLine("Surname >> ");
            string surname = Console.ReadLine();
            //collects email address
            Console.WriteLine("Email >> ");
            string email = Console.ReadLine();
            length = email.Length;
            while (length < 1)
            {
                Console.WriteLine("*** Please enter username >> ");
                email = Console.ReadLine();
                length = email.Length;
            }
            //collects phone number
            Console.WriteLine("Phone number >> ");
            string phone = Console.ReadLine();
            string username = managerSession.getSession();
            setManagerProfile(username, name, surname, email, phone);
        }
        //prints manager account details in profile section in main case 5

        public void updateProfileLogin()
        {
            string username = managerSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            Console.WriteLine("\nChange:\n1 Name | 2 Surname | 3 Email | 4 Phone number | 5 Password | 6 Username | 7 Exit\n");
            updateDetails = switchCaseNavigation.errorInput();
            do
            {
                switch (updateDetails)
                {
                    //each case represents one update
                    case 1:
                        //Name
                        Console.WriteLine("Please enter the new name >> ");
                        string newName = Console.ReadLine();
                        connection.Open();
                        string queryName = "UPDATE dbo.Manager_profile SET [Name] = @name WHERE [Username] = @username";
                        SqlCommand commandName = new SqlCommand(queryName, connection);
                        commandName.Parameters.AddWithValue("@username", username);
                        commandName.Parameters.AddWithValue("@name", newName);
                        commandName.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Name has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                    case 2:
                        //Surname
                        Console.WriteLine("Please enter the new surname >> ");
                        string newSurname = Console.ReadLine();
                        connection.Open();
                        string querySurname = "UPDATE dbo.Manager_profile SET [Surname] = @surname WHERE [Username] = @username";
                        SqlCommand commandSurname = new SqlCommand(querySurname, connection);
                        commandSurname.Parameters.AddWithValue("@username", username);
                        commandSurname.Parameters.AddWithValue("@surname", newSurname);
                        SqlDataReader readerSurname = commandSurname.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Surname has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                    case 3:
                        //Email
                        Console.WriteLine("Please enter the new email >> ");
                        string newEmail = Console.ReadLine();
                        connection.Open();
                        string queryEmail = "UPDATE dbo.Manager_profile SET [Email] = @email WHERE [Username] = @username";
                        SqlCommand commandEmail = new SqlCommand(queryEmail, connection);
                        commandEmail.Parameters.AddWithValue("@username", username);
                        commandEmail.Parameters.AddWithValue("@email", newEmail);
                        SqlDataReader readerEmail = commandEmail.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Email has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                    case 4:
                        //Phone number
                        Console.WriteLine("Please enter the new phone number >> ");
                        string newPhone = Console.ReadLine();
                        connection.Open();
                        string queryPhone = "UPDATE dbo.Manager_profile SET [Phone] = @phone WHERE [Username] = @username";
                        SqlCommand commandPhone = new SqlCommand(queryPhone, connection);
                        commandPhone.Parameters.AddWithValue("@username", username);
                        commandPhone.Parameters.AddWithValue("@phone", newPhone);
                        SqlDataReader readerPhone = commandPhone.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Phone number has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                    case 5:
                        //Password
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        connection.Open();
                        string queryPassword = "UPDATE dbo.Manager_login SET [Password] = @password WHERE [Username] = @username";
                        SqlCommand commandPassword = new SqlCommand(queryPassword, connection);
                        commandPassword.Parameters.AddWithValue("@username", username);
                        commandPassword.Parameters.AddWithValue("@password", newPassword);
                        SqlDataReader readerPassword = commandPassword.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                    case 6:
                        //User name
                        Console.WriteLine("Please enter the new username >> ");
                        string newUsername = Console.ReadLine();
                        connection.Open();
                        string queryUsername = "UPDATE dbo.Manager_profile SET [Username] = @username WHERE [Username] = @username";
                        SqlCommand commandUsername = new SqlCommand(queryUsername, connection);
                        commandUsername.Parameters.AddWithValue("@username", username);
                        commandUsername.Parameters.AddWithValue("@username", newUsername);
                        SqlDataReader readerUsername = commandUsername.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Username has been updated succsesfully --");
                        updateDetails = 7;
                        break;
                }
                if (updateDetails > 7)
                {
                    Console.WriteLine("\n*** Unrecognised input! ***\n");
                    updateDetails = 7;
                }
            } while (updateDetails != 7);
        }
    }
}
