using System;

namespace Application
{

	public class SwitchCaseNavigation
	{
		//returns an integer that is used in the main to go to a corresponding case in a switch
		int option;

		public int errorInput()
		{
			while (true)
			{
				try
				{
					Console.WriteLine("\n>> ");
					option = Convert.ToInt32(Console.ReadLine());
					break;
				}
				catch
				{
					Console.WriteLine ("*** Input not recognised. ***");
				}
			}
			//returns the selected menu option to the main and goes to the corresponding switch case.
			return option;
		}
	}
}
