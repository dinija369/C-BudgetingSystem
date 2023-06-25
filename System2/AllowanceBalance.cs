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

	public class AllowanceBalance
	{
        SwitchCaseNavigation error = new SwitchCaseNavigation();
        TeamSession teamSession = new TeamSession();

        //collects allowance per person
        private float allowancePerPerson;
        //collects the amount of people in the team
        private float peopleInTeam;
        //used to calculate the allowance per team by multiplying allowance per person by people in a team
        private float allowancePerTeam;
        //date to and from variables
        string dateTo;
        Decimal totalSpentDb = 0;
        //used for total spent in Expense method
        private float totalSpent = 0f;

        //updates allowance in the database
        private static void updateAllowance(string department, string date, string dateTo, float allowancePerTeam)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Allowance] = @allowance, [Date_from] = @date_from, [Date_to] = @date_to WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@allowance", allowancePerTeam);
            command.Parameters.AddWithValue("@date_from", date);
            command.Parameters.AddWithValue("@date_to", dateTo);

            command.ExecuteReader();

            connection.Close();

        }

        private void SetAllowance(string department, string date, string dateTo, float allowancePerTeam)
        {
            try
            {
                string connString = ConnectionString.Connection();
                SqlConnection connection = new SqlConnection(connString);
                connection.Open();
                string query = "INSERT INTO dbo.Allowance_table ([Department], [Allowance], [Date_from], [Date_to]) VALUES (@department, @allowance, @date_from, @date_to)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@department", department);
                command.Parameters.AddWithValue("@allowance", allowancePerTeam);
                command.Parameters.AddWithValue("@date_from", date);
                command.Parameters.AddWithValue("@date_to", dateTo);

                command.ExecuteReader();

                connection.Close();
            }
            catch 
            {
                Console.WriteLine("Would you like to update the Allowance?\nY/N >>  ");
                string updateAllowance = Console.ReadLine();
                if (updateAllowance == "Y")
                {
                    Application.AllowanceBalance.updateAllowance(department, date, dateTo, allowancePerTeam);
                    RemainingBalance();
                }

                else
                {
                    error.errorInput();
                }
            }

        }

        public void Allowance()
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

            string department = teamSession.getSession();

            //calculates the  allowance for each team based on people in the team and allowance per person
            allowancePerTeam = allowancePerPerson * peopleInTeam;

            SetAllowance(department, date, dateTo, allowancePerTeam);
        }

        public Decimal getAllowance()
        {
            Decimal allowance = 0;
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Allowance] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

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

        // Total Spent //

        public Decimal getTotalSpent()
        {
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Total_spent] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

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

        private static void setTotalSpent(string department, float totalSpent)
        {
            string connString = ConnectionString.Connection();  
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Total_spent] = @totalSpent WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@totalSpent", totalSpent);

            command.ExecuteReader();

            connection.Close();

        }

        public void TotalSpent(float expenseMoney)
        {
            string department = teamSession.getSession();
            //total spent amount is gotten from adding new expense to total spent
            totalSpent += expenseMoney;
            setTotalSpent(department, totalSpent);
        }


        // Remaining Balance //

        private static void setRemainingBalance(string department, float newRemainingBalance)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Money_left] = @balance WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@balance", newRemainingBalance);

            command.ExecuteReader();

            connection.Close();

        }

        public Decimal getRemainingBalance()
        {
            Decimal remainingBalance = 0;
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Money_left] FROM dbo.Allowance_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

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
        public void RemainingBalance()
        {
            float allowance = (float)getAllowance();
            float totalSpent = (float)getTotalSpent();
            string department = teamSession.getSession();
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
            setRemainingBalance(department, newRemainingBalance);
        }

    }
}
