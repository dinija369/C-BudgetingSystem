using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.Design;
using Application;
using System.Data.SqlClient;

namespace System2
{
    internal class Expense
    {
        Money moneyObject = new Money();
        Reports reportsObject = new Reports();
        float expenseMoney;
        float expense = 0f;
        float moneyLeft = 0f;
        int i = 0;

        private static void setExpense(string department, float expenseMoney, string expenseComment, string date)
        {
            string connString = "Server = DESKTOP-LQ2RF0O\\SQLEXPRESS; Database = BudgetManager; Trusted_Connection = True;";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Expense_table ([Department], [Expense], [Comment], [Date]) VALUES (@department, @expense, @comment, @date)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@expense", expenseMoney);
            command.Parameters.AddWithValue("@comment", expenseComment);
            command.Parameters.AddWithValue("@date", date);

            SqlDataReader reader = command.ExecuteReader();

            /*while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2));
            }*/
        }
        public void getExpense()
        {
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
            moneyLeft = moneyObject.RemainingBalance(i);
            //current date saved to a date variable
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            Console.WriteLine("Department* >> ");
            //collects department name from user
            string department = Console.ReadLine();
            //date, comment and money spent is passed to a reports class to be used for reports
            reportsObject.setItemisedSpend(date, expenseComment, expenseMoney);
            setExpense(department, expenseMoney, expenseComment, date);
        }

        public void printExpense()
        {
            Console.WriteLine("\nMoney spent " + expense + "\nMoney left " + moneyLeft);
        }
    }
}
