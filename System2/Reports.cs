using System;
using System.Collections.Generic;

namespace Application
{
	public class Reports
	{
		//lists to keep date, comment and money spent for each eneterd expense
		private List<string> date = new List<string>();
        private List<string> comments = new List<string>();
        private List<float> moneySpent = new List<float>();


        //gets date, comments and money spent for each eneterd expense in main case 3. Ads date, comments and money spent to array lists.
		//takes three arguments from the main class. These will be used in Report and toBeApproved methods.
        public void setItemisedSpend(string expenseDate, string expenseComment, float expenseMoney)
		{
			date.Add(expenseDate);
			comments.Add(expenseComment);
			moneySpent.Add(expenseMoney);
		}

		public void Report(float totalSpent, float moneyLeft)
		{
			//loop is looping as long as there are money spent items saved in the list.
			for (int i = 0; i < date.Count();  i++)
			{
				//the date, comments, and money spent is gotten for each entered expense
				Console.WriteLine(" ____________________ ________________________________________ _______________ ");
				Console.WriteLine("|{0, -20}|{1, -40}|{2, 15}|\n", date[i], comments[i], moneySpent[i]);
				Console.WriteLine("|____________________|________________________________________|_______________|");
            }
            //total spent and money left is also printed
            Console.WriteLine(" ____________________ ");
            Console.WriteLine("|Total spent: {0, -7}|\n", totalSpent);
            Console.WriteLine("|____________________|\n");
            Console.WriteLine(" ____________________ ");
            Console.WriteLine("|Money left: {0, -8}|\n", moneyLeft);
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