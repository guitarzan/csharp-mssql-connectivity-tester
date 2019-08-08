using System;
using System.Data.SqlClient;

namespace sql_connectivity_tester
{
    class Program
    {
        public enum ExitCode : int
        {
            Success = 0,
            UnknownError = 1
        }

        static int Main(string[] args)
        {
            try
            {
                string connectionString;
                if (args != null && args.Length > 0)
                {
                    connectionString = args[0];
                }
                else
                {
                    connectionString = AppConfig.ConnectionString;
                }

                Console.WriteLine("Connecting to: {0}", connectionString);
                using (var connection = new SqlConnection(connectionString))
                {
                    var query = "select 1";
                    Console.WriteLine("Executing: {0}", query);

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        Console.WriteLine("SQL Connection successful.");

                        command.ExecuteScalar();
                    }

                    Console.WriteLine("SQL Query execution successful.");
                    Console.WriteLine();

                    return (int)ExitCode.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure: {0}", ex.Message);
                Console.WriteLine();
                return (int)ExitCode.UnknownError;
            }
        }
    }
}
