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
            this.MySqlDb = new MySqlConnection(@"Server=localhost;Port=3306;Database=championsleague;Uid=root;Pwd=9410094420aA;");
            this.jsnoSerializer = new JavaScriptSerializer();
            this.pathToFile = path;
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
            var addTeamCommand = new MySqlCommand(@"INSERT INTO teams(TeamName, TownName) VALUES (@team, @town)", this.MySqlDb);
            this.MySqlDb.Open();
            addTeamCommand.Parameters.AddWithValue("@team", teamName);
            addTeamCommand.Parameters.AddWithValue("@town", townName);
            addTeamCommand.ExecuteNonQuery();
            this.MySqlDb.Close();
        }

        public void AddPlayer(string firstName, string lastName, int teamId, decimal salary)
        {
            var addPlayerCommand = new MySqlCommand(@"INSERT INTO players(FirstName, LastName, TeamId, Salary) VALUES (@fname, @lname, @teamId,@salary)",this.MySqlDb);
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
            var allPlayersCommand = new MySqlCommand("Select * FROM players", this.MySqlDb);
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
            var allPlayersCommand = new MySqlCommand("Select * FROM teams", this.MySqlDb);

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
