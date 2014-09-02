﻿namespace ChampionsLeague.JsonReports
{
    using System.Linq;
    using System.IO;
    using System.Web.Script.Serialization;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;

    public class JsonReport
    {
        private JavaScriptSerializer jsonSerializer;
        private ChampionsLeagueData dbContext;
        private string folderPath;

        public JsonReport(ChampionsLeagueData context, string path)
        {
            this.jsonSerializer = new JavaScriptSerializer();
            this.dbContext = context;
            this.folderPath = path;
        }

        public void GenerateAllTeams()
        {
            var teams = this.dbContext.Teams.All().Select(t => new
            {
                ID = t.TeamId,
                Team = t.TeamName,
                Town = t.Town.TownName,
                Players = t.Players.Select(p => new { p.FirstName, p.LastName, p.Salary })
            });

            Directory.CreateDirectory(this.folderPath);

            foreach (var team in teams)
            {
                string result = this.jsonSerializer.Serialize(team);
                string fileName = team.ID.ToString();

                SaveToFile(fileName, result);
            }
        }

        private void SaveToFile(string name, string content)
        {
            if(name.Length<2)
            {
                name = '0' + name;
            }
            string path = Path.Combine(this.folderPath, name + ".json");

            File.WriteAllText(path, content);
        }
    }
}
