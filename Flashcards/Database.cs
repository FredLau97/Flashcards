using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class Database
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Flashcards"].ConnectionString);

        public static SqlCommand cmd = new SqlCommand("DELETE FROM Stacks", connection);
        public static DataSet dataSet;
        public static SqlDataAdapter dataAdapter;
        public static string sql;

        public static void OpenConnection()
        {
            try 
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine($"The connection is {connection.State.ToString()}");
                    cmd.ExecuteNonQuery();
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine($"Opening connection failed: {e.Message}");
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine($"The connection is {connection.State.ToString()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Closing connection error: {e.Message}");
            }
        }
    }
}
