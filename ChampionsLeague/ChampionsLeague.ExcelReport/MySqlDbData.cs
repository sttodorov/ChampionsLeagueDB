namespace ChampionsLeague.ExcelReport
{
    using System.IO;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    using MySql.Data.MySqlClient;
    
    using ChampionsLeague.Model;
   
    public class MySqlDbData
    {
        private MySqlConnection MySqlDb;
        private JavaScriptSerializer jsnoSerializer;
        private string pathToFile;

        public MySqlDbData(string path)
        {
            //this.MySqlDb = new MySqlConnection(@"Server=localhost;Port=3306;Database=championsleague;IntegratedSecurity=yes;Uid=auth_windows;");
            string connectionString = @"Server=localhost;Port=3306;Uid=root;Pwd=9410094420aA;";
            this.MySqlDb = new MySqlConnection(connectionString);
            this.jsnoSerializer = new JavaScriptSerializer();
            this.pathToFile = path;
            this.InitializeMySqlDb();
        }

        private void InitializeMySqlDb()
        {
            string createDbQuery = @"CREATE DATABASE  IF NOT EXISTS `championsleague` /*!40100 DEFAULT CHARACTER SET utf8 */;
                                USE `championsleague`;

                                CREATE TABLE IF NOT EXISTS `teams` (
                                  `Id` int(11) NOT NULL AUTO_INCREMENT,
                                  `TeamName` varchar(45) NOT NULL,
                                  `TownName` varchar(45) NOT NULL,
                                  PRIMARY KEY (`Id`)
                                ) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8;
                                
                                  CREATE TABLE IF NOT EXISTS `players` (
                                  `Id` int(11) NOT NULL AUTO_INCREMENT,
                                  `FirstName` varchar(45) NOT NULL,
                                  `LastName` varchar(45) NOT NULL,
                                  `TeamId` int(11) DEFAULT NULL,
                                  `Salary` decimal(10,0) DEFAULT NULL,
                                  PRIMARY KEY (`Id`),
                                  KEY `TeamId_idx` (`TeamId`),
                                  CONSTRAINT `TeamId` FOREIGN KEY (`TeamId`) REFERENCES `teams` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
                                ) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8;";

            var createCommand = new  MySqlCommand(createDbQuery,this.MySqlDb);
            this.MySqlDb.Open();
            createCommand.ExecuteNonQuery();
            this.MySqlDb.Close();
        }

        public void LoadJsonReportsInMySql()
        {
            var teamReports = Directory.GetFiles(this.pathToFile);
            
            foreach (var report in teamReports)
            {
                var reader = new StreamReader(report);
                var team = this.jsnoSerializer.Deserialize <DeserializedObject> (reader.ReadToEnd());

                AddTeam(team.Team,team.Town);

                foreach (var player in team.Players)
                {
                    AddPlayer(player.FirstName, player.LastName, team.Id, player.Salary);
                }
            }
        }

        public void AddTeam(string teamName, string townName)
        {
            string insertIntoTeamsQuery = @"INSERT INTO teams(TeamName, TownName) VALUES (@team, @town)";
            var addTeamCommand = new MySqlCommand(insertIntoTeamsQuery, this.MySqlDb);

            this.MySqlDb.Open();
            addTeamCommand.Parameters.AddWithValue("@team", teamName);
            addTeamCommand.Parameters.AddWithValue("@town", townName);
            addTeamCommand.ExecuteNonQuery();
            this.MySqlDb.Close();
        }

        public void AddPlayer(string firstName, string lastName, int teamId, decimal salary)
        {
            string insertIntoPlayersQuery = @"INSERT INTO players(FirstName, LastName, TeamId, Salary) VALUES (@fname, @lname, @teamId,@salary)";
            var addPlayerCommand = new MySqlCommand(insertIntoPlayersQuery,this.MySqlDb);
            
            this.MySqlDb.Open();
            addPlayerCommand.Parameters.AddWithValue("@fname", firstName);
            addPlayerCommand.Parameters.AddWithValue("@lname", lastName);
            addPlayerCommand.Parameters.AddWithValue("@teamId", teamId);
            addPlayerCommand.Parameters.AddWithValue("@salary", salary);
            addPlayerCommand.ExecuteNonQuery();
            this.MySqlDb.Close();
           
        }

        public IList<Player> GetAllPlayers()
        {
            var allPLayers = new List<Player>();
            string selectPlayersQuery = "Select * FROM players";
            var allPlayersCommand = new MySqlCommand(selectPlayersQuery, this.MySqlDb);
            this.MySqlDb.Open();

            var reader = allPlayersCommand.ExecuteReader();

            while(reader.Read())
            {
                allPLayers.Add(new Player()
                {
                    Salary = (decimal)reader["Salary"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    TeamId = (int)reader["TeamId"]
                });
            }
            this.MySqlDb.Close();
            return allPLayers;
        }

        public IList<Team> GetAllTeams()
        {
            var allTeams = new List<Team>();
            string selectTeamsQuery = "Select * FROM teams";
            var allPlayersCommand = new MySqlCommand(selectTeamsQuery, this.MySqlDb);

            this.MySqlDb.Open();

            var reader = allPlayersCommand.ExecuteReader();

            while(reader.Read())
            {
                allTeams.Add(new Team()
                {
                    TeamId = (int)reader["Id"],
                    TeamName = (string)reader["TeamName"],
                    Town = new Town(){TownName = (string)reader["TownName"]}
                });

            }
            this.MySqlDb.Close();
            return allTeams;
        }

        //Create methods for  update and delete

    }
}
