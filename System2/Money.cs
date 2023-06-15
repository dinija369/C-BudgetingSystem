using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;

namespace Application
{

	public class Money
	{
        //collects allowance per person
        private float allowancePerPerson;
        //collects the amount of people in the team
        private float peopleInTeam;
        //used to calculate the allowance per team by multiplying allowance per person by people in a team
        private float allowancePerTeam;
        //date to and from variables
        string dateTo;
        string dateFrom;
        float a = 0f;
        //used for total spent in Expense method
        private float totalSpent = 0f;
        //string hashmap to store dates
        private Dictionary<string, string> DateFromTo = new Dictionary<string, string>();
        //list to store allowance that is added in each teams index place
        private List<float> allowance = new List<float>();
        //saves total money spent from Expense method each time new expense is added
        private List<float> moneySpent = new List<float>();
        //saves remaining balance by subtracting expense from balance each time
        private List<float> remainingBalance = new List<float>();

        //gets information about allowance for each team. per person, people in a team, date from to. calculates the allowance for the team and saves allowance in a 
        //list and dates in a dictionary.
        //todo - figure out how to get the session and work from that. Like each department session.

        private static void SetAllowance(string dep, string date, string dateTo, float allowancePerTeam)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Allowance_table ([Department], [Allowance], [Date_from], [Date_to]) VALUES (@dep, @allowance, @date_from, @date_to)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@allowance", allowancePerTeam);
            command.Parameters.AddWithValue("@date_from", date);
            command.Parameters.AddWithValue("@date_to", dateTo);

            SqlDataReader reader = command.ExecuteReader();

            /*while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2));
            }*/
        }
        public void Allowance(int i)
		{
            //collects allowance per person
            while (true)
			{
				try
				{
					Console.WriteLine("Allowance per person (BP) >> ");
					allowancePerPerson = float.Parse(Console.ReadLine());
					break;
				} 
				catch
				{
					Console.WriteLine("*** Input not recognised ***");
				}
			}
            //collects people in a team
            while (true)
            {
                try
                {
                    Console.WriteLine("People in the team >> ");
                    peopleInTeam = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("*** Input not recognised ***");
                }
            }
            //gets current date
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            //colects date to and checks if correct date format used
            while (true)
            {
                try
                {
                    Console.WriteLine("Date To (dd/MM/yyyy) >> ");
                    dateTo = Console.ReadLine();
                    //sets the format for a date and keeps it in a variable
                    string format = "dd/MM/yyyy";
                    //checks the correct format was eneterd
                    DateTime result = DateTime.ParseExact(dateTo, format, CultureInfo.InvariantCulture);
                    //returns a string with the date
                    dateTo = result.ToString();
                    break;
                }
                catch
                {
                    Console.WriteLine("*** Input not recognised ***");
                }
            }

            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();

            //TO REMOVE
            //ads both dates to a dictionary
            DateFromTo.Add(date, dateTo);
            //calculates the  allowance for each team based on people in the team and allowance per person
            allowancePerTeam = allowancePerPerson * peopleInTeam;
            //inserts the allowance for each team in a position 0
            allowance.Insert(i, allowancePerTeam);
            //TO REMOVE

            SetAllowance(dep, date, dateTo, allowancePerTeam);
        }

        public float getAllowance(int i)
        {
            return allowance[i];
        }

        private static void setExpense(string dep, float totalSpent)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Total_spent] = @totalSpent WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@totalSpent", totalSpent);

            SqlDataReader reader = command.ExecuteReader();

        }

        public float Expense(float expenseMoney, int i)
        {
            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();
            //total spent amount is gotten from adding new expense to total spent
            totalSpent += expenseMoney;
            //total spent is saved to a list
            moneySpent.Insert(i, totalSpent);
            setExpense(dep, totalSpent);
            //total spent is returned and collecetd by expense variable in main and printed on the home screen
            return moneySpent[i];
        }

        private static void setRemainingBalance(string dep, float newRemainingBalance)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Money_left] = @balance WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@balance", newRemainingBalance);

            SqlDataReader reader = command.ExecuteReader();

        }

        //calculates the remaining balance after each expense made.
        public float RemainingBalance(int i)
        {
            Console.WriteLine("Department* >> ");
            //collects department name from user
            string dep = Console.ReadLine();
            //calculates remaining balance by substracting total spent from allowance
            float newRemainingBalance = allowancePerTeam - totalSpent;
            //if allowance per team has been set
            if (allowancePerTeam > 0)
            {
                //and remining balance is less or equel to 0 the budget has been exeeded
                if (newRemainingBalance <= 0)
                {
                    Console.WriteLine("*** You have exceeded your budget! ***\n");
                }
            }
            //ads new remaining balance to a list after each calculation
            remainingBalance.Insert(i, newRemainingBalance);
            setRemainingBalance(dep, newRemainingBalance);
            //returns remaining balance and to keep it is money left variable on main and print it in a home page
            return remainingBalance[i];
        }

        public void zeroTotalSpent()
        {
            //sets total spent value to 0 for each team in manager view
            totalSpent = 0f;
        }

        public void addAllowance()
        {
            //for loop ads 10 float entries to allowance array list
            for (int i = 0; i < 10; i++)
            {
                allowance.Add(a);
            }
        }
    }
}
