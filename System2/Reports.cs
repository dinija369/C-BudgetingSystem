using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System2;

namespace Application
{
	public class Reports
	{
        AllowanceBalance moneyObject = new AllowanceBalance();
        TeamSession teamSession = new TeamSession();


        //displays date, comments and money spent for each eneterd expense in main case 3. these are displayed in report section in team view

        public void getExpenseReport()
        {
            string department = teamSession.getSession();

            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Expense], [Comment], [Date] FROM dbo.Expense_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ____________________ ________________________________________ _______________ ");
                Console.WriteLine("|{0, -20}|{1, -40}|{2, 15}|\n", reader.GetDecimal(0), reader.GetString(1), reader.GetString(2));
                Console.WriteLine("|____________________|________________________________________|_______________|");
            }

            connection.Close();

        }

        public void ReportSummary()
		{
            //total spent and money left is also printed
            Console.WriteLine(" ____________________ ");
            Console.WriteLine("|{0, -7}|\n", moneyObject.getTotalSpent());
            Console.WriteLine("|____________________|\n");
            Console.WriteLine(" ____________________ ");
            Console.WriteLine("|{0, -8}|\n", moneyObject.getRemainingBalance());
            Console.WriteLine("|____________________|\n");
        }

	}
}