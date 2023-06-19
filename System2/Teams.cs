using System;
using System.Collections;
using System.Data.SqlClient;
using System2;

namespace Application
{

	public class Teams
	{
        ErrorInput errorObject = new ErrorInput();
        //aray lists holding password, username, department and supervisor variables for teams that are entered at the beginning and will be used TODO!!!!!!
        private List<string> Password = new List<string>();
		private List<string> Username = new List<string>();

        int updateDetails;
        private string department;
        private string supervisor;
        private string password;
        private string username;


		//gets team department and supervisor in null position to print in team infor in main case 6
		public void getTeamProfile()
		{
			string dep = Session.getSession();

            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department], [Supervisor] FROM dbo.Team_profile";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Department: {0, -40}\n", reader.GetString(0));
                Console.WriteLine("|________________________________________|");
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Supervisor: {0, -40}\n", reader.GetString(1));
                Console.WriteLine("|________________________________________|");
            }
		}
        //gets team username in null position to print in team for profile in main case 6
        public void getTeamLogin()
        {
            string dep = Session.getSession();

            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Username] FROM dbo.Team_login";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Username: {0, -40}\n", reader.GetString(0));
                Console.WriteLine("|________________________________________|");
            }
        }
		//offers the user to update details in main - case 6. 
		public void updateProfile(int i)
		{
            string dep = Session.getSession();

            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
     
            Console.WriteLine("\nUpdate:\n| 1 Supervisor | 2 Password | 3 Username | 4 Exit\n");
            updateDetails = errorObject.errorInput();
			do
			{
				switch (updateDetails)
				{
					//each case represents one update. the new detail is collected in a string variable and inserted in the list in null position to replace
					//the old detail.
					case 1:
                        Console.WriteLine("Please enter the new supervisor >> ");
                        string newSupervisor = Console.ReadLine();
                        string querySupervisor = "UPDATE dbo.Team_profile SET [Supervisor] = @supervisor WHERE [Department] = @dep";
                        SqlCommand commandSupervisor = new SqlCommand(querySupervisor, connection);
                        commandSupervisor.Parameters.AddWithValue("@dep", dep);
                        commandSupervisor.Parameters.AddWithValue("@supervisor", newSupervisor);
                        SqlDataReader readerSupervisor = commandSupervisor.ExecuteReader();
                        connection.Close();
                        //Department.Insert(i, newSupervisor);
                        Console.WriteLine("-- Supervisor has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 2:
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        string queryPassword = "UPDATE dbo.Team_login SET [Password] = @password WHERE [Department] = @dep";
                        SqlCommand commandPassword = new SqlCommand(queryPassword, connection);
                        commandPassword.Parameters.AddWithValue("@dep", dep);
                        commandPassword.Parameters.AddWithValue("@password", newPassword);
                        SqlDataReader readerPassword = commandPassword.ExecuteReader();
                        connection.Close();
                        //Department.Insert(i, newPassword);
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 3:
                        Console.WriteLine("Please enter the new username >> ");
                        string newUser = Console.ReadLine();
                        string queryUser = "UPDATE dbo.Team_login SET [Username] = @user WHERE [Department] = @dep";
                        SqlCommand commandUser = new SqlCommand(queryUser, connection);
                        commandUser.Parameters.AddWithValue("@dep", dep);
                        commandUser.Parameters.AddWithValue("@user", newUser);
                        SqlDataReader readerUser = commandUser.ExecuteReader();
                        connection.Close();
                        //Department.Insert(i, newUsername);
                        Console.WriteLine("-- Username has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
				}
				//if update details choice is larger than 5 it will keep asking for input till it is valid
				if (updateDetails > 4)
				{
					Console.WriteLine("\n*** Unrecognised input! ***\n");
                    updateDetails = errorObject.errorInput();
                }
			//will end the whileloop if 5 is selecetd as that is an exit number
			} while (updateDetails != 4);
        }

		/// //////////////In manager mode//////////////////////////////////////////////////////////////////////////////////////////////////
		//loops through each department and assigns value starting from 7 to each department. This will be printed in manager mode home screen every time a new team is added
		//the number will be used to go to a specific teams profile from manager mode.
		public void getDepartment()
		{
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department] FROM dbo.Team_profile";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("|\n", reader.GetString(0));
            }
        }



    }
}
