namespace Application
{
	class DisplayMenu
	{
        //prints a prompt to choose manager or team view
        public static void TeamManagerChoice()
		{
			Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| 1. Team view | 2. Manager view |");
            Console.WriteLine("+--------------------------------+");
			Console.WriteLine("\n>>");
        }

        //prints a prompt to register or log in
        public static void RegisterOrLogin()
		{
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| 1. Register | 2. Log in |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("\n>>");
        }

        //prints manager menu when logged in manager profile
        public static void ManagerMenu()
        {
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("| 1. Home | 2. Add teams | 3. Notifications | 4. Profile | 5. Teams | 6. Exit |");
            Console.WriteLine("+-----------------------------------------------------------------------------+");
        }

        //prints teams menu when logged in teams profile
        public static void TeamMenu()
        {
            Console.WriteLine("+----------------------------------------------------------------------------------------------------+");
            Console.WriteLine("| 1. Home | 2. Add allowance | 3. Add expense | 4. Reports | 5. Notifications | 6. Profile | 7. Exit |");
            Console.WriteLine("+----------------------------------------------------------------------------------------------------+");
        }
    }
}
