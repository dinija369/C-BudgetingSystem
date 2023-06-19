using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System2;

namespace Application
{
	public class Reports
	{
		//lists to keep date, comment and money spent for each eneterd expense
		private List<string> date = new List<string>();
        private List<string> comments = new List<string>();
        private List<float> moneySpent = new List<float>();

        Money moneyObject = new Money();


        //gets date, comments and money spent for each eneterd expense in main case 3. Ads date, comments and money spent to array lists.
		//takes three arguments from the main class. These will be used in Report and toBeApproved methods.

        public void getExpenseReport()
        {
            string dep = Session.getSession();

            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "SELECT [Expense], [Comment], [Date] FROM dbo.Expense_table WHERE [Department] = @department";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", dep);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(" ____________________ ________________________________________ _______________ ");
                Console.WriteLine("|{0, -20}|{1, -40}|{2, 15}|\n", reader.GetDecimal(0), reader.GetString(1), reader.GetString(2));
                Console.WriteLine("|____________________|________________________________________|_______________|");
            }

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

		public void toBeApproved()
		{
			for (int i = 0; i < date.Count(); i++)
			{
				//the date, comments, and money spent is gotten for each entered expense
				Console.WriteLine(" ____________________ ________________________________________ _______________ ");
				Console.WriteLine("|{0, -20}|{1, -40}|{2, 15}|\n", date[i], comments[i], moneySpent[i]);
				Console.WriteLine("|____________________|________________________________________|_______________|");
			}
		}
	}
}