using System;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml.Linq;

namespace Application
{
	public class ManagerProfile
	{
        ErrorInput errorObject = new ErrorInput();

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
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
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
            //saves password and username in lists to be used for profile access
            Password.Add(password);
            Username.Add(username);
            //saves placeholders in a list while there are no values
            Name.Add("None");
            Surname.Add("None");
            Email.Add("None");
            Phone.Add("None");

            setManagerLogin(username, password);
        }

        private static void setManagerProfile(string username, string name, string surname, string email, string phone)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
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
            Console.WriteLine("Username* >> ");
            username = Console.ReadLine();

            //replaces the placeholders in the lists with  user input values from profile section in main
            Name.Insert(0, name);
            Surname.Insert(0, surname);
            Email.Insert(0, email);
            Phone.Insert(0, phone);

            setManagerProfile(username, name, surname, email, phone);
        }
        //prints manager account details in profile section in main case 5
        public void getManagerProfile()
        {
            Console.WriteLine("Name: " + Name[0]);
            Console.WriteLine("Surname: " + Surname[0]);
            Console.WriteLine("Email: " + Email[0]);
            Console.WriteLine("Phone: " + Phone[0]);
        }

        //gets team username in null position to print in manager profile for profile in main case 5
        public void getManagerLogin()
        {
            Console.WriteLine("Username: " + Username[0]);
        }

        public void updateProfile()
        {
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
                        Name.Insert(0, newName);
                        Console.WriteLine("-- Name has been updated succsesfully --");
                        //will check for a good input and go to update another detail if the user chooses so
                        updateDetails = errorObject.errorInput();
                        break;
                    case 2:
                        //Surname
                        Console.WriteLine("Please enter the new surname >> ");
                        string newSurname = Console.ReadLine();
                        Username.Insert(0, newSurname);
                        Console.WriteLine("-- Surname has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 3:
                        //Email
                        Console.WriteLine("Please enter the new email >> ");
                        string newEmail = Console.ReadLine();
                        Username.Insert(0, newEmail);
                        Console.WriteLine("-- Email has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                        break;
                    case 4:
                        //Phone number
                        Console.WriteLine("Please enter the new phone number >> ");
                        string newPhone = Console.ReadLine();
                        Phone.Insert(0, newPhone);
                        Console.WriteLine("-- Phone number has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 5:
                        //Password
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        Password.Insert(0, newPassword);
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
                    case 6:
                        //User name
                        Console.WriteLine("Please enter the new username >> ");
                        string newUsername = Console.ReadLine();
                        Username.Insert(0, newUsername);
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
