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
        ErrorInput errorObject = new ErrorInput();
        ManagerSession session = new ManagerSession();

        private string password;
        private string username;
        int updateDetails;

        //profile values for the manager stoerd in lists 
        List<string> Password = new List<string>();
        List<string> Username = new List<string>();
        List<string> Name = new List<string>();
        List<string> Surname = new List<string>();
        List<string> Email = new List<string>();
        List<string> Phone = new List<string>();

        private static void setManagerLogin(string username, string password)
        {
            string connString = ConnectionString.Connection();
            //string query = "SELECT * FROM dbo.users";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //
            string query = "INSERT INTO dbo.Manager_login ([Username], [Password]) VALUES (@username, @password)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            SqlDataReader reader = command.ExecuteReader();
        }

        //prompts the user to enter password and username, scans the entry and stores data in lists
        public void ManagerLogin()
		{
            Console.WriteLine("\n>> Please enter Username and password <<");
            Console.WriteLine("\nPassword* (at least 2 characters) >> ");
            password = Console.ReadLine();
            //puts the length of password string in integer variable
            int length = password.Length;
            //while loop: checks if the password is too short and keeps repeating the code as long as it is too short
            while (length < 2)
            {
                Console.WriteLine("*** Passsword is too short ***\nPlease try again >> ");
                password = Console.ReadLine();
                length = password.Length;
            }
            //the same process repeated with username
            Console.WriteLine("Username* >> ");
            username = Console.ReadLine();
            length = username.Length;
            while (length < 2)
            {
                Console.WriteLine("*** Please enter username >> ");
                username = Console.ReadLine();
                length = username.Length;
            }

            session.setSession(username);

            setManagerLogin(username, password);
        }

        private static void setManagerProfile(string username, string name, string surname, string email, string phone)
        {
            string connString = ConnectionString.Connection();
            //string query = "SELECT * FROM dbo.users";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //
            string query = "INSERT INTO dbo.Manager_profile ([Username], [Name], [Surname], [Email], [Phone]) VALUES (@username, @name, @surname, @email, @phone)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@phone", phone);

            SqlDataReader reader = command.ExecuteReader();
        }

        public void managerProfile()
        {
            Console.WriteLine("\n>> Create an account. <<\n");
            Console.WriteLine("Name* >> ");
            string name = Console.ReadLine();
            //puts the length of name string in integer variable
            int length = name.Length;
            //while loop checks if there is an input
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
            string username = ManagerSession.getSession();

            setManagerProfile(username, name, surname, email, phone);
        }
        //prints manager account details in profile section in main case 5
        public void getManagerProfile()
        {
            string username = ManagerSession.getSession();

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
                Console.WriteLine("|Username: {0, -40}\n", reader.GetString(0));
                Console.WriteLine("|________________________________________|");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Name: {0, -40}\n", reader.GetString(1));
                Console.WriteLine("|________________________________________|");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Surname: {0, -40}\n", reader.GetString(2));
                Console.WriteLine("|________________________________________|");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Email: {0, -40}\n", reader.GetString(3));
                Console.WriteLine("|________________________________________|");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Phone: {0, -40}\n", reader.GetString(4));
                Console.WriteLine("|________________________________________|");
            }
        }

        public void updateProfile()
        {
            string username = ManagerSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            Console.WriteLine("\nChange:\n1 Name | 2 Surname | 3 Email | 4 Phone number | 5 Password | 6 Username | 7 Exit\n>>");
            updateDetails = errorObject.errorInput();
            do
            {
                switch (updateDetails)
                {
                    //each case represents one update. the new detail is collected in a string variable and inserted in the list in null position to replace
                    //the old detail.
                    case 1:
                        //Name
                        Console.WriteLine("Please enter the new name >> ");
                        string newName = Console.ReadLine();
                        string queryName = "UPDATE dbo.Manager_profile SET [Name] = @name WHERE [Username] = @username";
                        SqlCommand commandName = new SqlCommand(queryName, connection);
                        commandName.Parameters.AddWithValue("@username", username);
                        commandName.Parameters.AddWithValue("@name", newName);
                        SqlDataReader readerSupervisor = commandName.ExecuteReader();
                        connection.Close();
                        //Name.Insert(0, newName);
                        Console.WriteLine("-- Name has been updated succsesfully --");
                        //will check for a good input and go to update another detail if the user chooses so
                        updateDetails = errorObject.errorInput();
                        break;
                    case 2:
                        //Surname
                        Console.WriteLine("Please enter the new surname >> ");
                        string newSurname = Console.ReadLine();
                        string querySurname = "UPDATE dbo.Manager_profile SET [Surname] = @surname WHERE [Username] = @username";
                        SqlCommand commandSurname = new SqlCommand(querySurname, connection);
                        commandSurname.Parameters.AddWithValue("@username", username);
                        commandSurname.Parameters.AddWithValue("@surname", newSurname);
                        SqlDataReader readerSurname = commandSurname.ExecuteReader();
                        connection.Close();
                        //Username.Insert(0, newSurname);
                        Console.WriteLine("-- Surname has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 3:
                        //Email
                        Console.WriteLine("Please enter the new email >> ");
                        string newEmail = Console.ReadLine();
                        string queryEmail = "UPDATE dbo.Manager_profile SET [Email] = @email WHERE [Username] = @username";
                        SqlCommand commandEmail = new SqlCommand(queryEmail, connection);
                        commandEmail.Parameters.AddWithValue("@username", username);
                        commandEmail.Parameters.AddWithValue("@email", newEmail);
                        SqlDataReader readerEmail = commandEmail.ExecuteReader();
                        connection.Close();
                        //Username.Insert(0, newEmail);
                        Console.WriteLine("-- Email has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                        break;
                    case 4:
                        //Phone number
                        Console.WriteLine("Please enter the new phone number >> ");
                        string newPhone = Console.ReadLine();
                        string queryPhone = "UPDATE dbo.Manager_profile SET [Phone] = @phone WHERE [Username] = @username";
                        SqlCommand commandPhone = new SqlCommand(queryPhone, connection);
                        commandPhone.Parameters.AddWithValue("@username", username);
                        commandPhone.Parameters.AddWithValue("@phone", newPhone);
                        SqlDataReader readerPhone = commandPhone.ExecuteReader();
                        connection.Close();
                        //Phone.Insert(0, newPhone);
                        Console.WriteLine("-- Phone number has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 5:
                        //Password
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        string queryPassword = "UPDATE dbo.Manager_login SET [Password] = @password WHERE [Username] = @username";
                        SqlCommand commandPassword = new SqlCommand(queryPassword, connection);
                        commandPassword.Parameters.AddWithValue("@username", username);
                        commandPassword.Parameters.AddWithValue("@name", newPassword);
                        SqlDataReader readerPassword = commandPassword.ExecuteReader();
                        connection.Close();
                        //Password.Insert(0, newPassword);
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 6:
                        //User name
                        Console.WriteLine("Please enter the new username >> ");
                        string newUsername = Console.ReadLine();
                        string queryUsername = "UPDATE dbo.Manager_profile SET [Username] = @username WHERE [Username] = @username";
                        SqlCommand commandUsername = new SqlCommand(queryUsername, connection);
                        commandUsername.Parameters.AddWithValue("@username", username);
                        commandUsername.Parameters.AddWithValue("@username", newUsername);
                        SqlDataReader readerUsername = commandUsername.ExecuteReader();
                        connection.Close();
                        //Username.Insert(0, newUsername);
                        Console.WriteLine("-- Username has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                }
                //if update details choice is larger than 7 it will keep asking for input till it is valid
                if (updateDetails > 7)
                {
                    Console.WriteLine("\n*** Unrecognised input! ***\n");
                    updateDetails = errorObject.errorInput();
                }
                //will end the whileloop if 7 is selecetd as that is an exit number
            } while (updateDetails != 7);
        }

    }
}
