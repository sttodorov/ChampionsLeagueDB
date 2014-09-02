
namespace ChampionsLeague.XMLData
{
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System.IO;

    using ChampionsLeague.Model;
    using ChampionsLeague.Data;

    public class XMLPlayersGenerator
    {
        private static Random random = new Random();
        private const int MinSalary = 200;
        private const int MaxSalary = 50000;
        private List<string> playersAsString;

        public XMLPlayersGenerator()
        {
            this.playersAsString = new List<string>();
        }

        public void LoadPlayersFromFile(string filePath)
        {
            using(var reader = new StreamReader(filePath))
            {
                var currentLine = reader.ReadLine();
                while (!string.IsNullOrEmpty(currentLine))
                {
                    playersAsString.Add(currentLine);
                    currentLine = reader.ReadLine();
                }
            }
        }

        public void SavePlayersAsXML(string filePath)
        {
            int lastTeamId = 0;

            using(var db = new ChampionsLeagueContext())
            {
                lastTeamId = db.Teams.Count();
            }
            
            var xmlPlayers = new List<XElement>();

            foreach (var player in this.playersAsString)
            {
                var splittedData = player.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                xmlPlayers.Add(
                    new XElement("Player",
                        new XElement("FirstName", splittedData[0]),
                        new XElement("LastName", splittedData[1]),
                        new XElement("Salary", random.Next(MinSalary, MaxSalary)),
                        new XElement("TeamId",random.Next(1, lastTeamId))                        
                        ));
            }

            var xmlSerializedPlayers = new XElement("Players", xmlPlayers);
            xmlSerializedPlayers.Save(filePath);
        }
    }
}
