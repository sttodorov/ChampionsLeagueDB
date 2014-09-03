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
            string createTableQuery = "CREATE TABLE IF NOT EXISTS YellowCards(id int PRIMARY KEY, Team nvarchar(50), FirstName nvarchar(50), LastName nvarchar(50),YellowCardsCount int)";
            var createTable = new SQLiteCommand(createTableQuery, this.SQLiteDb);
            this.SQLiteDb.Open();
            createTable.ExecuteNonQuery();
            this.SQLiteDb.Close();
            this.InsertRecords();
        }

        private void InsertRecords()
        {
            this.AddRecord("Liverpool", "Orlando", "Greer", 3);
            this.AddRecord("Liverpool", "Gabriel", "Rivera", 2);
            this.AddRecord("Liverpool", "Jeremy", "Cohen", 1);
            this.AddRecord("Liverpool", "Jeffery", "Horton", 5);

            this.AddRecord("Real Sociedad", "Everett", "Carr", 1);
            this.AddRecord("Real Sociedad", "Dan", "Dunn", 2);
            this.AddRecord("Real Sociedad", "Jerald", "Wise", 1);
            this.AddRecord("Real Sociedad", "Rogelio", "Flowers", 3);
        }

        public void AddRecord(string teamName, string firstName, string lastName, int yellowCardsCount)
        {
            string insertIntoYellowCardsQuery = @"INSERT INTO YellowCards (Team, FirstName, LastName, YellowCardsCount)
                                                            VALUES (@teamName, @firstName, @lastName, @cardsCount)";

            SQLiteCommand insertCommand = new SQLiteCommand(insertIntoYellowCardsQuery, this.SQLiteDb);
            this.SQLiteDb.Open();
            insertCommand.Parameters.AddWithValue("@teamName",teamName);
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
                    TeamName= (string)reader["Team"],
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
