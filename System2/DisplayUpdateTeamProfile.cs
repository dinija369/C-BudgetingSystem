using System;
using System.Collections;
using System.Data.SqlClient;
using System2;

namespace Application
{

	public class DisplayUpdateTeamProfile
	{
        SwitchCaseNavigation errorObject = new SwitchCaseNavigation();
        TeamSession teamSession = new TeamSession();

        int updateDetails;


		//gets team department and supervisor to print in team infor in main case 6
		public void getTeamProfile()
		{
			string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department], [Supervisor] FROM dbo.Team_profile";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

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

            connection.Close();
        }
        //gets team username to print in team for profile in main case 6
        public void getTeamLogin()
        {
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Username] FROM dbo.Team_login";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ________________________________________ ");
                Console.WriteLine("|Username: {0, -40}\n", reader.GetString(0));
                Console.WriteLine("|________________________________________|");
            }

            connection.Close();
        }
		//offers the user to update details in main - case 6. 
		public void updateProfile()
		{
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
     
            Console.WriteLine("\nUpdate:\n| 1 Supervisor | 2 Password | 3 Username | 4 Exit\n");
            updateDetails = errorObject.errorInput();
			do
			{
				switch (updateDetails)
				{
					//each case represents one update
					case 1:
                        Console.WriteLine("Please enter the new supervisor >> ");
                        string newSupervisor = Console.ReadLine();
                        string querySupervisor = "UPDATE dbo.Team_profile SET [Supervisor] = @supervisor WHERE [Department] = @dep";
                        SqlCommand commandSupervisor = new SqlCommand(querySupervisor, connection);
                        commandSupervisor.Parameters.AddWithValue("@dep", department);
                        commandSupervisor.Parameters.AddWithValue("@supervisor", newSupervisor);
                        SqlDataReader readerSupervisor = commandSupervisor.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Supervisor has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 2:
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        string queryPassword = "UPDATE dbo.Team_login SET [Password] = @password WHERE [Department] = @dep";
                        SqlCommand commandPassword = new SqlCommand(queryPassword, connection);
                        commandPassword.Parameters.AddWithValue("@dep", department);
                        commandPassword.Parameters.AddWithValue("@password", newPassword);
                        SqlDataReader readerPassword = commandPassword.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 3:
                        Console.WriteLine("Please enter the new username >> ");
                        string newUser = Console.ReadLine();
                        string queryUser = "UPDATE dbo.Team_login SET [Username] = @user WHERE [Department] = @dep";
                        SqlCommand commandUser = new SqlCommand(queryUser, connection);
                        commandUser.Parameters.AddWithValue("@dep", department);
                        commandUser.Parameters.AddWithValue("@user", newUser);
                        SqlDataReader readerUser = commandUser.ExecuteReader();
                        connection.Close();
                        Console.WriteLine("-- Username has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
				}
				if (updateDetails > 4)
				{
					Console.WriteLine("\n*** Unrecognised input! ***\n");
                    updateDetails = errorObject.errorInput();
                }
			} while (updateDetails != 4);
        }

		public void getDepartment()
		{
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Department] FROM dbo.Team_profile";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("|" + reader.GetString(0) + "\n");
            }

            connection.Close();
        }



    }
}
