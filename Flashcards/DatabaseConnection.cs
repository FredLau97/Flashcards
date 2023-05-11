using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Flashcards
{
    internal class DatabaseConnection
    {
        private SqlConnection connection;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private string sql;

        public DatabaseConnection()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Flashcards"].ConnectionString);
            cmd = new SqlCommand();
            adapter = new SqlDataAdapter();
            sql = "";
        }

        private bool OpenConnection()
        {
            try 
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    //Console.WriteLine($"The connection is {connection.State.ToString()}");
                    return true;
                }
            } 
            catch (Exception e)
            {
                //Console.WriteLine($"Opening connection failed: {e.Message}");
            }

            return false;
        }

        private bool CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    //Console.WriteLine($"The connection is {connection.State.ToString()}");
                    return true;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine($"Closing connection error: {e.Message}");
            }

            return false;
        }

        public List<StackDTO>? GetStacks()
        {
            if (!OpenConnection()) return null;

            sql = "SELECT * FROM Stacks";
            cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            var stacks = new List<StackDTO>();

            while (reader.Read()) 
            {
                stacks.Add(new StackDTO((int)reader["StackID"], (string)reader["StackName"]));
            }

            CloseConnection();
            return stacks;
        }

        internal void CreateStack(string stackName)
        {
            if (!OpenConnection()) return;

            sql = $"INSERT INTO Stacks (StackName) VALUES ('{stackName}')";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        internal bool CreateFlashcard(FlashcardDTO card)
        {
            if (!OpenConnection()) return false;

            sql = $"INSERT INTO Cards (CardFront, CardBack, StackID) VALUES ('{card.CardFront}', '{card.CardBack}', '{card.StackId}')";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
            return true;
        }

        internal StackDTO GetStack(string stackName)
        {
            if (!OpenConnection()) return null;

            sql = $"SELECT * FROM Stacks WHERE StackName = '{stackName}'";
            cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"STACK! StackID: {reader["StackID"]}, StackName: {reader["StackName"]}");
                var stackID = (int)reader["StackID"];
                CloseConnection();
                return new StackDTO(stackID, stackName);
            }

            return null;
        }

        internal List<FlashcardDTO> GetCardsInStack(StackDTO stack)
        {
            if (!OpenConnection()) return null;

            sql = $"SELECT * FROM Cards WHERE StackID = '{stack.StackId}'";
            cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            var cards = new List<FlashcardDTO>();

            while (reader.Read())
            {
                cards.Add(new FlashcardDTO((int)reader["CardID"], (string)reader["CardFront"], (string)reader["CardBack"], (int)reader["StackID"]));
            }

            CloseConnection();
            return cards;
        }

        internal void EditFlashcard(FlashcardDTO card)
        {
            if (!OpenConnection()) return;

            sql = $"UPDATE Cards SET CardFront='{card.CardFront}', CardBack='{card.CardBack}' WHERE CardID='{card.CardId}'";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        internal void DeleteFlashcard(int cardID)
        {
            if (!OpenConnection()) return;

            sql = $"DELETE FROM Cards WHERE CardID='{cardID}'";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        internal void DeleteStack(int stackId)
        {
            if (!OpenConnection()) return;

            DeleteCardsInStack(stackId);
            sql = $"DELETE FROM Stacks WHERE StackID='{stackId}'";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        internal void DeleteCardsInStack(int stackId)
        {
            sql = $"DELETE FROM Cards WHERE StackID='{stackId}'";
            cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        internal List<FlashcardDTO> GetAllCards()
        {
            if (!OpenConnection()) return null;

            sql = "SELECT * FROM Cards";
            cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            var cards = new List<FlashcardDTO>();

            while (reader.Read())
            {
                cards.Add(new FlashcardDTO((int)reader["CardID"], (string)reader["CardFront"], (string)reader["CardBack"], (int)reader["StackID"]));
            }

            CloseConnection();
            return cards;
        }
    }
}
