using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System2
{
    internal class ConnectionString
    {
        public static string Connection()
        {
            string connString = "Server = ; Database = BudgetManager; Trusted_Connection = True;";
            return connString;
        }
    }
}
