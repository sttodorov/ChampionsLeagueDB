using System.Collections.Generic;
using System.Data.SQLite;
namespace ChampionsLeague.ExcelReport
{
    public class SQLiteDbData
    {
        private SQLiteConnection SQLiteDb;

        public SQLiteDbData()
        {
            string SQLiteConnectionString = @"Data Source=..\..\PlayersAdditionalInfo.db; Version=3;";
            this.SQLiteDb = new SQLiteConnection(SQLiteConnectionString);
            this.InitializeTable();
        }

        private void InitializeTable()
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS YellowCards(id int PRIMARY KEY, FirstName nvarchar(50), LastName nvarchar(50),YellowCardsCount int)";
            var createTable = new SQLiteCommand(createTableQuery, this.SQLiteDb);
            this.SQLiteDb.Open();
            createTable.ExecuteNonQuery();
            this.SQLiteDb.Close();
            this.InsertRecords();
        }

        private void InsertRecords()
        {
            this.AddRecord("Ronnie", "Hammond", 3);
            this.AddRecord("Phillip", "Potter", 2);
            this.AddRecord("Perry", "Hunt", 1);
            this.AddRecord("Orlando", "Greer", 5);
            this.AddRecord("Joey", "Burton", 1);
            this.AddRecord("George", "Boyd", 2);
            this.AddRecord("Freddie", "Knight", 1);
            this.AddRecord("Bert", "Nunez", 3);
            this.AddRecord("Luther", "Chavez", 3);
            this.AddRecord("Alvin", "Porter", 2);
            this.AddRecord("Israel", "Garner", 1);
            this.AddRecord("Antonio", "Jefferson", 5);
            this.AddRecord("Clay ", "Parker", 1);
            this.AddRecord("Hector", "Barrett", 2);
            this.AddRecord("Rafael", "Hoffman", 1);
            this.AddRecord("Jermaine", "Frank", 3);
            this.AddRecord("Byron", "Mathis", 3);
            this.AddRecord("Julian", "Mcdaniel", 1);
            this.AddRecord("Sam", "Estrada", 3);
            this.AddRecord("Arnold", "Price", 3);   

        }

        public void AddRecord(string firstName, string lastName, int yellowCardsCount)
        {
            string insertIntoYellowCardsQuery = @"INSERT INTO YellowCards (FirstName, LastName, YellowCardsCount)
                                                            VALUES (@firstName, @lastName, @cardsCount)";

            SQLiteCommand insertCommand = new SQLiteCommand(insertIntoYellowCardsQuery, this.SQLiteDb);
            this.SQLiteDb.Open();
            insertCommand.Parameters.AddWithValue("@firstName",firstName);
            insertCommand.Parameters.AddWithValue("@lastName",lastName);
            insertCommand.Parameters.AddWithValue("@cardsCount",yellowCardsCount);
            insertCommand.ExecuteNonQuery();
            this.SQLiteDb.Close();   
        }

        public IList<YellowCardsRecoord> GetAll()
        {
            var reccords = new List<YellowCardsRecoord>();
            string selectFromYellowCardsQuery = "Select * From YellowCards";
            SQLiteCommand getAllCommand = new SQLiteCommand(selectFromYellowCardsQuery, this.SQLiteDb);
            this.SQLiteDb.Open();

            var reader = getAllCommand.ExecuteReader();

            while(reader.Read())
            {
                reccords.Add(new YellowCardsRecoord
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    CardsCount = (int)reader["YellowCardsCount"]
                });
            }
            this.SQLiteDb.Close();
            return reccords;
        }

        public void Update(string firstName, string lastName,int cardsCount)
        {
            string updateYellowCardsQuery = "UPDATE YellowCards SET YellowCardsCount = @cardsCount WHERE FirstName = @firstname AND LastName = @lastName";
            var updateCommand = new SQLiteCommand(updateYellowCardsQuery, this.SQLiteDb);
            this.SQLiteDb.Open();

            updateCommand.Parameters.AddWithValue("@cardsCount", cardsCount);
            updateCommand.Parameters.AddWithValue("@firstName", firstName);
            updateCommand.Parameters.AddWithValue("@lastName", lastName);

            updateCommand.ExecuteNonQuery();
            this.SQLiteDb.Close();
        }
    }
}
