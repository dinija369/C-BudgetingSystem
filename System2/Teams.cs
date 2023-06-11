using System;
using System.Collections;

namespace Application
{

	public class Teams
	{
        ErrorInput errorObject = new ErrorInput();
        //aray lists holding password, username, department and supervisor variables for teams that are entered at the beginning and will be used TODO!!!!!!
        private List<string> Password = new List<string>();
		private List<string> Username = new List<string>();
        private List<string> Department = new List<string>();
        private List<string> Supervisor = new List<string>();

        int updateDetails;
        private string department;
        private string supervisor;
        private string password;
        private string username;


		//gets team department and supervisor in null position to print in team infor in main case 6
		public void getTeamProfile(int i)
		{
			Console.WriteLine("Department: " + Department[i]);
			Console.WriteLine("Supervisor: " + Supervisor[i]);
		}
        //gets team username in null position to print in team for profile in main case 6
        public void getTeamLogin(int i)
        {
            Console.WriteLine("Username: " + Username[i]);
        }
		//offers the user to update details in main case 6. 
		public void updateProfile(int i)
		{
			Console.WriteLine("\nChange:\n1 Department | 2 Supervisor | 3 Password | 4 Username | 5 Exit\n");
            updateDetails = errorObject.errorInput();
			do
			{
				switch (updateDetails)
				{
					//each case represents one update. the new detail is collected in a string variable and inserted in the list in null position to replace
					//the old detail.
					case 1:
						Console.WriteLine("Please enter the new department >> ");
						string newDepartment = Console.ReadLine();
						Department.Insert(i, newDepartment);
						Console.WriteLine("-- Dpartment has been updated succsesfully --");
						//will check for a good input and go to update another detail if the user chooses so
                        updateDetails = errorObject.errorInput();
                        break;
					case 2:
                        Console.WriteLine("Please enter the new supervisor >> ");
                        string newSupervisor = Console.ReadLine();
                        Department.Insert(i, newSupervisor);
                        Console.WriteLine("-- Supervisor has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 3:
                        Console.WriteLine("Please enter the new password >> ");
                        string newPassword = Console.ReadLine();
                        Department.Insert(i, newPassword);
                        Console.WriteLine("-- Password has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
					case 4:
                        Console.WriteLine("Please enter the new username >> ");
                        string newUsername = Console.ReadLine();
                        Department.Insert(i, newUsername);
                        Console.WriteLine("-- Username has been updated succsesfully --");
                        updateDetails = errorObject.errorInput();
                        break;
				}
				//if update details choice is larger than 5 it will keep asking for input till it is valid
				if (updateDetails > 5)
				{
					Console.WriteLine("\n*** Unrecognised input! ***\n");
                    updateDetails = errorObject.errorInput();
                }
			//will end the whileloop if 5 is selecetd as that is an exit number
			} while (updateDetails != 5);
        }
		//loops through each department and assigns value starting from 7 to each department. This will be printed in manager mode home screen every time a new team is added
		//the number will be used to go to a specific teams profile from manager mode.
		public void getDepartment()
		{
			//loops through department list
			for (int i = 0; i < Department.Count; i++)
			{
				int a = i + 8;
				//prints number + department in manager mode home screen
				Console.WriteLine(a + " " + Department[i] + "\n");
			}
		}






    }
}
