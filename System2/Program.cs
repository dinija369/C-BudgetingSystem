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
            //class object constructors. object constructor is a function that can be called to create an object form a class
            Teams teams = new Teams();
            ErrorInput errorObject = new ErrorInput();
            Money moneyObject = new Money();
            Reports reportsObject = new Reports();
            ManagerProfile managerObject = new ManagerProfile();
            TeamProfile teamProfile = new TeamProfile();
            Expense expenseObj = new Expense();
            TeamSession session = new TeamSession();
            TeamNotifications notification = new TeamNotifications();
            ManagerNotifications managerNotifications = new ManagerNotifications();
            

            //integer array declarations
            //used for the user to choose manager or team mode. if array takes 1 as an input - team mode accessed, 2 - manager mode
            int managerTeamView;
            //used to got ot menu option 1 which is a home page in a switch satemenet
            int menuOption;
            //used to print allowance, expense and money left information on the home page for team view. gotten from TODO!!!!!!!!!!
            float allowance = 0f;
            float expense = 0f;
            float moneyLeft = 0f;
            float expenseMoney;
            int i = 0;
            int registerLogin;

            //while loop keeps looping if the input is not integer and printing the error message from the catch block
            while (true)
            {
                try
                {
                    //'Menu' class method. prints a prompt for team or manager view
                    Menu.TeamManagerChoice();
                    //scanner object takes manager or team view input
                    managerTeamView = Convert.ToInt32(Console.ReadLine());
                    //breaks out of the loop if there is no error
                    break;
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            //if statement checks the user input and runs the corresponding code
            if (managerTeamView == 1)
            {
                //'Teams' class method. prompts the user for profile details and saves them in arraylist
                teamProfile.Profile();

                teamProfile.Login();
                //will jump to the first condition in below switch statement
                menuOption = 1;

                do
                {
                    switch (menuOption)
                    {
                        case 1:
                            //prints home menu for team mode
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> HOME <<|\n");
                            //prints the current allowance, expense and money left. allowance is taken from case 2. expense and money left is taken from case 3
                            //todo - add option to update allowance as currently it will not allow it because of primary key valiation
                            moneyObject.getAllowance();
                            moneyObject.getTotalSpent();
                            moneyObject.getRemainingBalance();
                            //checks the case choice to go to another menu item for errors
                            menuOption = errorObject.errorInput();
                            break;
                        case 2:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> ADD ALLOWANCE <<|\n");
                            //allows a user to set allowance for the team
                            moneyObject.Allowance(i = 0);
                            //returns allowance from the setAloowance method above that is stored in allowance variable and printed in home screen.
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> ADD EXPENSE <<|\n");
                            expenseObj.getExpense();
                            menuOption = errorObject.errorInput();
                            break;
                        case 4:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> REPORTS <<|\n");
                            //gets the report and passes expense and remaining balance arguments to print in the report
                            reportsObject.getExpenseReport();
                            reportsObject.ReportSummary();
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            //todo - notifications
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            TeamNotifications.getNotifications();
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
                            Menu.TeamMenu();
                            menuOption = errorObject.errorInput();
                            break;
                    }
                    //if statement prints error message and goes to home page if input more than 7
                    if (menuOption > 7)
                    {
                        Console.WriteLine("\n*** Unrecognised input! ***\n");
                        menuOption = 1;
                    }
                //terminates do while loop if input is 7
                } while (menuOption != 7);

               TeamSession.endSession();

            }

            else
            {
                //asks if the user wants to register or login and keeps repeating if the input is not 1 or 2
                while (true)
                {
                    try
                    {
                        Menu.RegisterOrLogin();
                        registerLogin = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("*** Input not recognised ***\nType 1 to Register or 2 to Log in");

                    }
                }
                //if statement runs a block of code if input is 1. 1 = register
                if (registerLogin == 1)
                {
                    //prompts the user for password and username and saves them in string array list for manager profile
                    managerObject.ManagerLogin();
                    //prompts the user for name, surname, email and phone and saves them in string list for manager profile
                    managerObject.managerProfile();
                }

                else
                {
                    //prompts the user for password and username and saves them in string array list for manager profile
                    managerObject.ManagerLogin();
                }

                menuOption = 1;

                do
                {
                    switch (menuOption)
                    {
                        case 1:
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> HOME <<|\n");
                            menuOption = errorObject.errorInput();
                            break;
                        case 2:
                            //allows the manager to add teams
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> ADD TEAMS <<|\n");
                            teamProfile.Profile();
                            teamProfile.Login();
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            //Notifications section. Nothing there yet but its coming
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            ManagerNotifications.getNotifications();
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
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            managerObject.getManagerProfile();
                            //managerObject.getManagerLogin();
                            managerObject.updateProfile();
                            Menu.ManagerMenu();
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> TEAMS <<|\n");
                            teams.getDepartment();
                            Console.WriteLine("Department: >>");
                            string department = Console.ReadLine();
                            session.setSession(department);
                            teams.getTeamProfile();
                            teams.getTeamLogin();
                            teams.updateProfile();
                            TeamSession.endSession();
                            //if statement goes to team view if input is more than 6
                            menuOption = errorObject.errorInput();
                            break;
                    }

                //while statement in do while loop. terminates the loop if 'menuOption' is 7
                } while (menuOption != 6);

                TeamSession.endSession();
            }


        }
    }
}