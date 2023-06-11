using Microsoft.VisualBasic;
using System;
using System.ComponentModel.Design;

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
            SqlConnTeamProfile sqlConn = new SqlConnTeamProfile();

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
                SqlConnTeamProfile.TeamProfile();

                SqlConnTeamProfile.TeamLogin();
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
                            Console.WriteLine("Allowance " + allowance + "\nMoney spent " + expense + "\nMoney left " + moneyLeft);
                            //checks the case choice to go to another menu item for errors
                            menuOption = errorObject.errorInput();
                            break;
                        case 2:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> ADD ALLOWANCE <<|\n");
                            //allows a user to set allowance for the team
                            moneyObject.setAllowance(i = 0);
                            //returns allowance from the setAloowance method above that is stored in allowance variable and printed in home screen.
                            allowance = moneyObject.getAllowance(i = 0);
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> ADD EXPENSE <<|\n");
                            while(true)
                            {
                                try
                                {
                                    //expense money collected
                                    Console.WriteLine("Money spent >> ");
                                    expenseMoney = float.Parse(Console.ReadLine());
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    System.Console.WriteLine(ex.Message);
                                }
                            }
                            //comments for expense collected
                            Console.WriteLine("Comments >> ");
                            string expenseComment = Console.ReadLine();
                            //total spent is calculated, added to a list and returned after each expense
                            expense = moneyObject.Expense(expenseMoney, i);
                            //remaining balance is calculated after each expense added to a list and returned
                            moneyLeft = moneyObject.getRemainingBalance(i);
                            //current date saved to a date variable
                            string date = DateTime.Now.ToString("dd/MM/yyyy");
                            //date, comment and money spent is passed to a reports class to be used for reports
                            reportsObject.setItemisedSpend(date, expenseComment, expenseMoney);
                            menuOption = errorObject.errorInput();
                            break;
                        case 4:
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> REPORTS <<|\n");
                            //gets the report and passes expense and remaining balance arguments to print in the report
                            reportsObject.Report(expense, moneyObject.getRemainingBalance(i));
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            //Notifications section. Nothing there yet but its coming
                            Menu.TeamMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            Console.WriteLine(">> You have no messages yet! <<");
                            menuOption = errorObject.errorInput();
                            break;
                        case 6:
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            //prints the department and supervisor
                            teams.getTeamProfile(i = 0);
                            //prints the username
                            teams.getTeamLogin(i = 0);
                            //can update all profile details one by one
                            teams.updateProfile(i = 0);
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

            }

            else
            {
                //asks if the user wants to register or login and keeps repeating if the input is not 1 or 2
                while (true)
                {
                    try
                    {
                        Menu.RegisterOrLogin();
                        registerLogin = Console.Read();
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
                    managerObject.setManagerLogin();
                    //prompts the user for name, surname, email and phone and saves them in string list for manager profile
                    managerObject.setManagerProfile();
                }

                else
                {
                    //prompts the user for password and username and saves them in string array list for manager profile
                    managerObject.setManagerLogin();
                }

                menuOption = 1;
                int a = 0;

                do
                {
                    switch (menuOption)
                    {
                        case 1:
                            Menu.ManagerMenu();
                            //sets moneyLeft and expense to 0 for each team
                            moneyLeft = 0f;
                            expense = 0f;
                            //fisrt visit to the manager mode sets a to 1. Adding a team sets a to 2. accessing the team sets a to 3. this executes the if statement
                            a++;
                            Console.WriteLine("\n|>> HOME <<|\n");
                            while (true)
                            {
                                try
                                {
                                    //'Teams' class method. prints every created team by department
                                    teams.getDepartment();
                                    menuOption = errorObject.errorInput();
                                    //if statement goes to team view if input is more than 6
                                    if (menuOption > 6)
                                    {
                                        i = menuOption - 8;
                                        if (a > 2)
                                        {
                                            //sets total spent value to 0
                                            moneyObject.zeroTotalSpent();
                                        }
                                        do
                                        {
                                            switch (menuOption)
                                            {
                                                case 1:
                                                    //prints home menu for team mode
                                                    Menu.TeamMenu();
                                                    Console.WriteLine("\n|>> HOME <<|\n");
                                                    //prints the current allowance, expense and money left. allowance is taken from case 2. expense and money left is taken from case 3
                                                    Console.WriteLine("Allowance " + allowance + "\nMoney spent " + expense + "\nMoney left " + moneyLeft);
                                                    //checks the case choice to go to another menu item for errors
                                                    menuOption = errorObject.errorInput();
                                                    break;
                                                case 2:
                                                    Menu.TeamMenu();
                                                    Console.WriteLine("\n|>> ADD ALLOWANCE <<|\n");
                                                    //allows a useer to set allowance for the team
                                                    moneyObject.setAllowance(i = 0);
                                                    //returns allowance from the setAloowance method above that is stored in allowance variable and printed in home screen.
                                                    allowance = moneyObject.getAllowance(i = 0);
                                                    menuOption = errorObject.errorInput();
                                                    break;
                                                case 3:
                                                    Menu.TeamMenu();
                                                    Console.WriteLine("\n|>> ADD EXPENSE <<|\n");
                                                    while (true)
                                                    {
                                                        try
                                                        {
                                                            //expense money collected
                                                            Console.WriteLine("Money spent >> ");
                                                            expenseMoney = float.Parse(Console.ReadLine());
                                                            break;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            System.Console.WriteLine(ex.Message);
                                                        }
                                                    }
                                                    //comments for expense collected
                                                    Console.WriteLine("Comments >> ");
                                                    string expenseComment = Console.ReadLine();
                                                    //total spent is calculated, added to a list and returned after each expense
                                                    expense = moneyObject.Expense(expenseMoney, i);
                                                    //remaining balance is calculated after each expense added to a list and returned
                                                    moneyLeft = moneyObject.getRemainingBalance(i);
                                                    //current date saved to a date variable
                                                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                                                    //date, comment and money spent is passed to a reports class to be used for reports
                                                    reportsObject.setItemisedSpend(date, expenseComment, expenseMoney);
                                                    menuOption = errorObject.errorInput();
                                                    break;
                                                case 4:
                                                    Menu.TeamMenu();
                                                    Console.WriteLine("\n|>> REPORTS <<|\n");
                                                    //gets the report and passes expense and remaining balance arguments to print in the report
                                                    reportsObject.Report(expense, moneyObject.getRemainingBalance(i));
                                                    menuOption = errorObject.errorInput();
                                                    break;
                                                case 5:
                                                    //Notifications section. Nothing there yet but its coming
                                                    Menu.TeamMenu();
                                                    Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                                                    Console.WriteLine(">> You have no messages yet! <<");
                                                    menuOption = errorObject.errorInput();
                                                    break;
                                                case 6:
                                                    Console.WriteLine("\n|>> PROFILE <<|\n");
                                                    //prints the department and supervisor
                                                    teams.getTeamProfile(i = 0);
                                                    //prints the username
                                                    teams.getTeamLogin(i = 0);
                                                    //can update all profile details one by one
                                                    teams.updateProfile(i = 0);
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

                                        menuOption = 1;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine();
                                }
                                break;
                            }
                            break;
                        case 2:
                            //allows the manager to add teams
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> ADD TEAMS <<|\n");
                            SqlConnTeamProfile.TeamProfile();
                            SqlConnTeamProfile.TeamLogin();
                            menuOption = errorObject.errorInput();
                            break;
                        case 3:
                            //To be approved section shows expenses made by teams
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> TO BE APPROVED <<|\n");
                            reportsObject.toBeApproved();
                            menuOption = errorObject.errorInput();
                            break;
                        case 4:
                            //Notifications section. Nothing there yet but its coming
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> NOTIFICATIONS <<|\n");
                            Console.WriteLine(">> You have no messages yet! <<");
                            menuOption = errorObject.errorInput();
                            break;
                        case 5:
                            //manager profile allows the to view profile details and make changes
                            Menu.ManagerMenu();
                            Console.WriteLine("\n|>> PROFILE <<|\n");
                            managerObject.getManagerProfile();
                            managerObject.getManagerLogin();
                            managerObject.updateProfile();
                            Menu.ManagerMenu();
                            menuOption = errorObject.errorInput();
                            break;
                    }

                //while statement in do while loop. terminates the loop if 'menuOption' is 6
                } while (menuOption != 6);
            }


        }
    }
}