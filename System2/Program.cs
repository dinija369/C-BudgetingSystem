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
            TeamProfile teamProfile = new();
            SwitchCaseNavigation switchCaseNavigation = new();
            AllowanceBalance allowanceBalance = new();
            Reports reports = new();
            ManagerProfile managerProfile = new();
            SaveTeamProfile saveTeamProfile = new();
            Expense expense = new();
            TeamSession teamSession = new();
            TeamNotifications teamNotifications = new();
            ManagerNotifications managerNotifications = new();
            TeamAuthentication teamAuthentication = new();
            ManagerAuthentication managerAuthentication = new();
            ManagerSession managerSession = new();
            

            //used for the user to choose manager or team mode. if array takes 1 as an input - team mode accessed, 2 - manager mode
            int managerTeamView;
            //used to get to menu option 1 which is a home page in a switch satemenet
            int menuOption;
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

            // TEAM SECTION

            if (managerTeamView == 1)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine(">> Team Profile <<");
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
                    Console.WriteLine(">> Team Register <<");
                    //'Teams' class method. prompts the user for profile details
                    saveTeamProfile.RegisterProfile();
                    saveTeamProfile.RegisterLogin();
                }

                else
                {
                    Console.WriteLine(">> Team Login <<");
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
                            Console.WriteLine("Allowance:        | {0, 4}", allowanceBalance.PrintAllowance());
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Money spent:      | {0, 4}", allowanceBalance.PrintTotalSpent());
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Rmaining balance: | {0, 4}", allowanceBalance.getRemainingBalance());
                            menuOption = switchCaseNavigation.errorInput();
                            break;
                        case 2:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> ADD ALLOWANCE <<|\n");
                            Console.WriteLine("\n| + Add allowance\n| Q (Exit)\n\n>>  ");
                            string input = Console.ReadLine();
                            if (input == "+")
                            {
                                //allows a user to set allowance for the team
                                allowanceBalance.Allowance();
                                goto case 2;
                            }
                            else if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 3:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> ADD EXPENSE <<|\n");
                            Console.WriteLine("\n| + Add Expense\n| Q (Exit)\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "+")
                            {
                                //collects expense information and passes to anothere method in the same class to be saved in database
                                expense.getExpense();
                                goto case 3;
                            }
                            else if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 4:
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> REPORTS <<|\n");
                            //gets the report and passes expense and remaining balance arguments to print in the report
                            reports.getExpenseReport();
                            reports.ReportSummary();
                            Console.WriteLine("\n\n| Q (Exit)\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 5:
                            //notifications
                            DisplayMenu.TeamMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            teamNotifications.getNotifications();
                            Console.WriteLine("\n| + New message \n| Q (Exit)\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "+")
                            {
                                teamNotifications.Notifications();
                                goto case 5;
                            }
                            else if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 6:
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            //prints the department and supervisor
                            teamProfile.getTeamProfile();
                            //prints the username
                            teamProfile.getTeamLogin();
                            //can update all profile details one by one
                            teamProfile.updateProfile();
                            Console.WriteLine("\n| Q Exit\n| 4 View profile\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "q" || input == "Q")
                            {
                                goto case 1;
                            }
                            else if (input == "4")
                            {
                                goto case 6;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                    }
                    if (menuOption > 7)
                    {
                        Console.WriteLine("\n*** Unrecognised input! ***\n");
                        menuOption = 1;
                    }
                } while (menuOption != 7);

               teamSession.endSession();
            }

            // MANAGER SECTION

            else if (managerTeamView == 2)
            {
                //asks if the user wants to register or login and keeps repeating if the input is not 1 or 2
                while (true)
                {
                    try
                    {
                        Console.WriteLine(">> Manager Profile <<");
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
                    Console.WriteLine(">> Manager Register <<");
                    //prompts the user for password and username
                    managerProfile.ManagerLogin();
                    //prompts the user for name, surname, email and phone
                    managerProfile.managerProfile();
                }

                else
                {
                    Console.WriteLine(">> Manager Login <<");
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
                            menuOption = switchCaseNavigation.errorInput();
                            break;
                        case 2:
                            //allows the manager to add teams
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> ADD TEAMS <<|\n");
                            Console.WriteLine("\n| + Add Another team\n| 5 View teams \n| Q (Exit)\n\n>>  ");
                            string input = Console.ReadLine();
                            if (input == "+")
                            {
                                saveTeamProfile.RegisterProfile();
                                saveTeamProfile.RegisterLogin();
                                Console.WriteLine("Team added succesfully!");
                                goto case 2;
                            }
                            else if (input == "5")
                            {
                                goto case 5;
                            }
                            else if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 3:
                            //Notifications section
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            managerNotifications.getNotifications();
                            Console.WriteLine("\n| + New message \n| Q (Exit)\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "+")
                            {
                                managerNotifications.Notifications();
                                goto case 3;
                            }
                            else if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 4:
                            //manager profile allows the to view profile details and make changes
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            managerProfile.getManagerProfile();
                            managerProfile.updateProfileLogin();
                            Console.WriteLine("\n| Q Exit\n| 4 View profile\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "q" || input == "Q")
                            {
                                goto case 1;
                            }
                            else if (input == "4")
                            {
                                goto case 4;
                            }
                            else
                            {
                                menuOption = switchCaseNavigation.errorInput();
                                break;
                            }
                        case 5:
                            DisplayMenu.ManagerMenu();
                            Console.WriteLine("\n|>> TEAMS <<|\n");
                            teamProfile.getDepartment();
                            Console.WriteLine("\n| + Add Another team\n| Department \n| Q (Exit)\n\n>>  ");
                            input = Console.ReadLine();
                            if (input == "Q" || input == "q")
                            {
                                goto case 1;
                            }
                            else if (input == "+")
                            {
                                goto case 2;
                            }
                            else
                            {
                                teamSession.setSession(input);
                                teamProfile.getTeamProfile();
                                teamProfile.getTeamLogin();
                                teamProfile.updateProfile();
                                teamSession.endSession();
                                goto case 5;
                            }
                    }
                    if (menuOption > 6)
                    {
                        Console.WriteLine("\n*** Unrecognised input! ***\n");
                        menuOption = 1;
                    }
                } while (menuOption != 6);

                managerSession.endSession();
            }
        }
    }
}