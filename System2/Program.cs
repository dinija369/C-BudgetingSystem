using Microsoft.VisualBasic;
using System;
using System.ComponentModel.Design;
using System2;

namespace Application
{
    public class System2
    {
        static void Main(string[] args)
        {
            //class object constructors
            DisplayUpdateTeamProfile teams = new DisplayUpdateTeamProfile();
            SwitchCaseNavigation errorObject = new SwitchCaseNavigation();
            AllowanceBalance moneyObject = new AllowanceBalance();
            Reports reportsObject = new Reports();
            ManagerProfile managerObject = new ManagerProfile();
            SaveTeamProfile teamProfile = new SaveTeamProfile();
            Expense expenseObj = new Expense();
            TeamSession session = new TeamSession();
            TeamNotifications notification = new TeamNotifications();
            ManagerNotifications managerNotifications = new ManagerNotifications();
            TeamAuthentication teamAuthentication = new TeamAuthentication();
            ManagerAuthentication managerAuthentication = new ManagerAuthentication();
            ManagerSession managerSession = new ManagerSession();
            

            //used for the user to choose manager or team mode. if array takes 1 as an input - team mode accessed, 2 - manager mode
            int managerTeamView;
            //used to get to menu option 1 which is a home page in a switch satemenet
            int menuOption;
            float expenseMoney;
            int registerLogin;

            while (true)
            {
                try
                {
                    //'Menu' class method. prints a prompt for team or manager view
                    DisplayMenu.TeamManagerChoice();
                    //scanner object takes manager or team view input
                    managerTeamView = Convert.ToInt32(Console.ReadLine());
                    break;
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            if (managerTeamView == 1)
            {
                while (true)
                {
                    try
                    {
                        DisplayMenu.RegisterOrLogin();
                        registerLogin = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("*** Input not recognised ***\nType 1 to Register or 2 to Log in");

                    }
                }
                //1 = register
                if (registerLogin == 1)
                {
                    //'Teams' class method. prompts the user for profile details
                    teamProfile.RegisterProfile();

                    teamProfile.RegisterLogin();
                }

                else
                {
                    //prompts the user for password and username and check if they are available in the database
                    teamAuthentication.Login();
                }
                
                menuOption = 1;

                do
                {
                    switch (menuOption)
                    {
                        case 1:
                            //prints home menu for team mode
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> HOME <<|\n");
                            //prints the current allowance, expense and money left. allowance is taken from case 2. expense and money left is taken from case 3
                            moneyObject.getAllowance();
                            moneyObject.getTotalSpent();
                            moneyObject.getRemainingBalance();
                            menuOption = errorObject.errorInput();
                            break;
                        case 2:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> ADD ALLOWANCE <<|\n");
                            //allows a user to set allowance for the team
                            moneyObject.Allowance();
                            //returns allowance from the setAloowance method above that is stored in allowance variable and printed in home screen.
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> ADD EXPENSE <<|\n");
                            expenseObj.getExpense();
                            menuOption = errorObject.errorInput();
                            break;
                        case 4:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> REPORTS <<|\n");
                            //gets the report and passes expense and remaining balance arguments to print in the report
                            reportsObject.getExpenseReport();
                            reportsObject.ReportSummary();
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            //notifications
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            notification.getNotifications();
                            Console.WriteLine("+ new message >> ");
                            string newMessage = Console.ReadLine();
                            if (newMessage == "+")
                            {
                                notification.Notifications();
                            }
                            menuOption = errorObject.errorInput();
                            break;
                        case 6:
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            //prints the department and supervisor
                            teams.getTeamProfile();
                            //prints the username
                            teams.getTeamLogin();
                            //can update all profile details one by one
                            teams.updateProfile();
                            DisplayMenu.TeamMenu();
                            menuOption = errorObject.errorInput();
                            break;
                    }
                    if (menuOption > 7)
                    {
                        Console.WriteLine("\n*** Unrecognised input! ***\n");
                        menuOption = 1;
                    }
                } while (menuOption != 7);

               session.endSession();

            }

            else
            {
                //asks if the user wants to register or login and keeps repeating if the input is not 1 or 2
                while (true)
                {
                    try
                    {
                        DisplayMenu.RegisterOrLogin();
                        registerLogin = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("*** Input not recognised ***\nType 1 to Register or 2 to Log in");

                    }
                }
                //1 = register
                if (registerLogin == 1)
                {
                    //prompts the user for password and username
                    managerObject.ManagerLogin();
                    //prompts the user for name, surname, email and phone
                    managerObject.managerProfile();
                }

                else
                {
                    //prompts the user for password and username and checks if it is in the database
                    managerAuthentication.Login();
                }

                menuOption = 1;

                do
                {
                    switch (menuOption)
                    {
                        case 1:
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> HOME <<|\n");
                            menuOption = errorObject.errorInput();
                            break;
                        case 2:
                            //allows the manager to add teams
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> ADD TEAMS <<|\n");
                            teamProfile.RegisterProfile();
                            teamProfile.RegisterLogin();
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            //Notifications section
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            managerNotifications.getNotifications();
                            Console.WriteLine("+ new message >> ");
                            string newMessage = Console.ReadLine();
                            if (newMessage == "+")
                            {
                                managerNotifications.Notifications();
                            }
                            menuOption = errorObject.errorInput();
                            break;
                        case 4:
                            //manager profile allows the to view profile details and make changes
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            managerObject.getManagerProfile();
                            managerObject.updateProfile();
                            DisplayMenu.ManagerMenu();
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> TEAMS <<|\n");
                            teams.getDepartment();
                            Console.WriteLine("Department: >>");
                            string department = Console.ReadLine();
                            session.setSession(department);
                            teams.getTeamProfile();
                            teams.getTeamLogin();
                            teams.updateProfile();
                            session.endSession();
                            menuOption = errorObject.errorInput();
                            break;
                    }
                } while (menuOption != 6);

                managerSession.endSession();
            }


        }
    }
}