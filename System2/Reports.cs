using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System2;

namespace Application
{
	public class Reports
	{
        AllowanceBalance allowanceBalance = new();
        TeamSession teamSession = new();


        //displays date, comments and money spent for each eneterd expense in main case 3. these are displayed in report section in team view

        public void getExpenseReport()
        {
            string department = teamSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "SELECT [Expense], [Comment], [Date] FROM dbo.Expense_table WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(" _________________________________________________________________________________");
                Console.WriteLine("|{0, -20}|{1, -40}|{2, 15}", reader.GetDecimal(0), reader.GetString(1), reader.GetString(2));
                Console.WriteLine("----------------------------------------------------------------------------------" + "|");
            }
            connection.Close();
        }

        public void ReportSummary()
		{
            //total spent and money left is also printed
            Console.WriteLine("|Total Spent: {0, 14}", allowanceBalance.PrintTotalSpent());
            Console.WriteLine("----------------------------------------------------------------------------------" + "|");
            Console.WriteLine("|Remaining balance: {0, 8}", allowanceBalance.getRemainingBalance());
            Console.WriteLine(" _________________________________________________________________________________" + "|");
        }
	}
}