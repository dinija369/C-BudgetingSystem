using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System2;
using System.Runtime.Intrinsics.Arm;

namespace Application
{

	public class Money
	{
        TeamProfile profile = new TeamProfile();
        ErrorInput error = new ErrorInput();

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
        Decimal totalSpentDb = 0;
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

        private static void updateAllowance(string dep, string date, string dateTo, float allowancePerTeam)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Allowance] = @allowance, [Date_from] = @date_from, [Date_to] = @date_to WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@allowance", allowancePerTeam);
            command.Parameters.AddWithValue("@date_from", date);
            command.Parameters.AddWithValue("@date_to", dateTo);

            SqlDataReader reader = command.ExecuteReader();

            //updateBalance(dep, allowancePerTeam);

        }

        public void SetAllowance(string dep, string date, string dateTo, float allowancePerTeam)
        {
            try
            {
                string connString = ConnectionString.Connection();
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
            catch 
            {
                Console.WriteLine("Would you like to update the Allowance?\nY/N >>  ");
                string updateAllowance = Console.ReadLine();
                if (updateAllowance == "Y")
                {
                    int i = 0;
                    Money.updateAllowance(dep, date, dateTo, allowancePerTeam);
                    //TotalSpent();
                    RemainingBalance(i);
                }

                else
                {
                    error.errorInput();
                }
            }

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

            string dep = TeamSession.getSession();

            //TO REMOVE
            //ads both dates to a dictionary
            //DateFromTo.Add(date, dateTo);
            //calculates the  allowance for each team based on people in the team and allowance per person
            allowancePerTeam = allowancePerPerson * peopleInTeam;
            //inserts the allowance for each team in a position 0
            allowance.Insert(i, allowancePerTeam);
            //TO REMOVE

            SetAllowance(dep, date, dateTo, allowancePerTeam);
        }

        public Decimal getAllowance()
        {
            Decimal allowance = 0;
            string dep = TeamSession.getSession();

            string connString = ConnectionString.Connection();

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Allowance] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                allowance = reader.GetDecimal(0);

                connection.Close();

                Console.WriteLine("Allowance  " + allowance);
            }

            else { Console.WriteLine("Allowance 0"); }

            return allowance;

        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////Total spent//////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Decimal getTotalSpent()
        {
            string dep = TeamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Total_spent] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    totalSpentDb = reader.GetDecimal(0);
                }
                catch(System.Data.SqlTypes.SqlNullValueException) 
                {

                }
                connection.Close();

                Console.WriteLine("Money spent  " + totalSpentDb);
            }

            else { Console.WriteLine("Money spent 0 "); }

            return totalSpentDb;

        }

        private static void setTotalSpent(string dep, float totalSpent)
        {
            string connString = ConnectionString.Connection();  
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Total_spent] = @totalSpent WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@totalSpent", totalSpent);

            SqlDataReader reader = command.ExecuteReader();

        }

        public void TotalSpent(float expenseMoney, int i)
        {
            string dep = TeamSession.getSession();
            //total spent amount is gotten from adding new expense to total spent
            totalSpent += expenseMoney;
            //total spent is saved to a list
            moneySpent.Insert(i, totalSpent);
            setTotalSpent(dep, totalSpent);
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //remaining balance ////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void setRemainingBalance(string dep, float newRemainingBalance)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Money_left] = @balance WHERE [Department] = @dep";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@dep", dep);
            command.Parameters.AddWithValue("@balance", newRemainingBalance);

            SqlDataReader reader = command.ExecuteReader();

        }

        public Decimal getRemainingBalance()
        {
            Decimal remainingBalance = 0;
            string dep = TeamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Money_left] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    remainingBalance = reader.GetDecimal(0);
                }
                catch (System.Data.SqlTypes.SqlNullValueException)
                {

                }
                connection.Close();

                Console.WriteLine("Remaining balance  " + remainingBalance);
            }

            else { Console.WriteLine("Remaining balance 0"); }

            return remainingBalance;

        }

        //calculates the remaining balance after each expense made.
        public void RemainingBalance(int i)
        {
            float allowance = (float)getAllowance();
            float totalSpent = (float)getTotalSpent();
            string dep = TeamSession.getSession();
            //calculates remaining balance by substracting total spent from allowance
            float newRemainingBalance = allowance - totalSpent;
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
        }

    }
}
