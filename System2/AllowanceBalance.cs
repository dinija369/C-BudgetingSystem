using System.Globalization;
using System.Data.SqlClient;
using System2;

namespace Application
{

	public class AllowanceBalance
	{
        readonly SwitchCaseNavigation switchCaseNavigation = new();
        readonly TeamSession teamSession = new();

        private float allowancePerPerson;
        private float peopleInTeam;
        private float allowancePerTeam;
        string dateTo;
        private Decimal totalSpentDb = 0;
        private Decimal allowance = 0;
        //used for total spent in Expense method
        private float totalSpent = 0f;

        private static void updateAllowance(string department, string date, string dateTo, float allowancePerTeam)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Allowance] = @allowance, [Date_from] = @date_from, [Date_to] = @date_to WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@allowance", allowancePerTeam);
            command.Parameters.AddWithValue("@date_from", date);
            command.Parameters.AddWithValue("@date_to", dateTo);

            command.ExecuteReader();
            connection.Close();
        }
        //inserts allownace details in Allowance_table. When allowance details are inserted first time the try block will execute. When allowance inserted any repaeting times the catch block will execute
        //because department is a primary key and the program will try to insert the same department as it is for the same team which will raise a duplicate primary key error. This will redirect to the 
        //update allowance method.
        private void SetAllowance(string department, string date, string dateTo, float allowancePerTeam)
        {
            try
            {
                string connString = ConnectionString.Connection();
                SqlConnection connection = new(connString);
                connection.Open();
                string query = "INSERT INTO dbo.Allowance_table ([Department], [Allowance], [Date_from], [Date_to], [Money_left]) VALUES (@department, @allowance, @date_from, @date_to, @money_left)";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@department", department);
                command.Parameters.AddWithValue("@allowance", allowancePerTeam);
                command.Parameters.AddWithValue("@date_from", date);
                command.Parameters.AddWithValue("@date_to", dateTo);
                command.Parameters.AddWithValue("@money_left", allowancePerTeam);

                command.ExecuteReader();
                connection.Close();
            }
            catch
            {
                Console.WriteLine("Would you like to update the Allowance?\nY/N >>  ");
                string updateAllowance = Console.ReadLine();
                if (updateAllowance == "Y")
                {
                    AllowanceBalance.updateAllowance(department, date, dateTo, allowancePerTeam);
                    RemainingBalance();
                }
                else
                {
                    switchCaseNavigation.errorInput();
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
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            //colects date to and checks if correct date format used
            while (true)
            {
                try
                {
                    Console.WriteLine("Date To (dd/MM/yyyy) >> ");
                    dateTo = Console.ReadLine();
                    string format = "dd/MM/yyyy";
                    //checks the correct format was eneterd // TODO - learn more about CultureInfo class and add date check
                    DateTime result = DateTime.ParseExact(dateTo, format, CultureInfo.InvariantCulture);
                    dateTo = result.ToString();
                    break;
                }
                catch
                {
                    Console.WriteLine("*** Input not recognised ***");
                }
            }
            string department = teamSession.getSession();
            allowancePerTeam = allowancePerPerson * peopleInTeam;
            SetAllowance(department, date, dateTo, allowancePerTeam);
        }

        public Decimal PrintAllowance()
        {
            string department = teamSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "SELECT [Allowance] FROM dbo.Allowance_table WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                allowance = reader.GetDecimal(0);
                connection.Close();
            }
            return allowance;
        }

        // Total Spent //

        public Decimal PrintTotalSpent()
        {
            string department = teamSession.getSession();
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "SELECT [Total_spent] FROM dbo.Allowance_table WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

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
            }
            return totalSpentDb;
        }

        private static void setTotalSpent(string department, float totalSpent)
        {
            string connString = ConnectionString.Connection();  
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Total_spent] = @totalSpent WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@totalSpent", totalSpent);

            command.ExecuteReader();
            connection.Close();
        }

        public void TotalSpent(float expenseMoney)
        {
                string department = teamSession.getSession();
                string connString = ConnectionString.Connection();
                SqlConnection connection = new(connString);
                connection.Open();
                string query = "SELECT [Total_spent] FROM dbo.Allowance_table WHERE [Department] = @department";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@department", department);

                SqlDataReader reader = command.ExecuteReader();

            try {
                reader.Read();
                totalSpent = (float)reader.GetDecimal(0);
                connection.Close();
            }
            catch { totalSpent = 0; }

            totalSpent += expenseMoney;
            setTotalSpent(department, totalSpent);
        }


        // Remaining Balance //

        private static void setRemainingBalance(string department, float newRemainingBalance)
        {
            string connString = ConnectionString.Connection();
            SqlConnection connection = new(connString);
            connection.Open();
            string query = "UPDATE dbo.Allowance_table SET [Money_left] = @balance WHERE [Department] = @department";
            SqlCommand command = new(query, connection);

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
            SqlConnection connection = new(connString);
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
            }
            return remainingBalance;
        }

        //calculates the remaining balance after each expense made.
        public void RemainingBalance()
        {
            float allowance = (float)PrintAllowance();
            float totalSpent = (float)PrintTotalSpent();
            string department = teamSession.getSession();
            float newRemainingBalance = allowance - totalSpent;
            if (allowancePerTeam > 0)
            {
                if (newRemainingBalance <= 0)
                {
                    Console.WriteLine("*** You have exceeded your budget! ***\n");
                }
            }
            setRemainingBalance(department, newRemainingBalance);
        }
    }
}
