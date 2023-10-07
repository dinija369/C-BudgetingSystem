using Application;
using System.Data.SqlClient;

namespace System2
{
    internal class Expense
    {
        AllowanceBalance allowanceBalance = new();
        TeamSession teamSession = new();
        float expenseMoney;

        private void setExpense(string department, float expenseMoney, string expenseComment, string date)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "INSERT INTO dbo.Expense_table ([Department], [Expense], [Comment], [Date]) VALUES (@department, @expense, @comment, @date)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@expense", expenseMoney);
            command.Parameters.AddWithValue("@comment", expenseComment);
            command.Parameters.AddWithValue("@date", date);

            command.ExecuteReader();
            connection.Close();
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
            //total spent is calculated
            allowanceBalance.TotalSpent(expenseMoney);
            //remaining balance is calculated after each expense
            allowanceBalance.RemainingBalance();
            //current date
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string department = teamSession.getSession();
            setExpense(department, expenseMoney, expenseComment, date);
        }
    }
}
