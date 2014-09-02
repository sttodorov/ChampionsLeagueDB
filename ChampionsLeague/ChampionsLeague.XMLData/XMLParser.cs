namespace ChampionsLeague.XMLData
{
    using System;
    using System.Linq;    
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.Generic;

    using ChampionsLeague.Model;
    using ChampionsLeague.Data; 

    public class XMLParser
    {
        private const string MatchXMLProp = "Match";
        private const string DateTimeXMLProp = "Date";        
        private const string HostTeamXMLProp = "HostTeam";
        private const string GuestTeamXMLProp = "GuestTeam";
        private const string StadiumXMLProp = "Stadium";
        private const string TownXMLProp = "Town";        

        public XMLParser()
        {
        }

        public ICollection<Match> LoadMatchReport(string filePath)
        {
            var matches = new HashSet<Match>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                var currentMatch = new Match();

                while (reader.Read())
                {  
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case MatchXMLProp:
                                if (currentMatch.HostTeamId != 0)
                                {
                                    matches.Add(currentMatch);
                                    currentMatch = new Match();
                                }                                                              
                                break;
                            case DateTimeXMLProp:
                                currentMatch.Date = DateTime.Parse(reader.ReadElementString());
                                break;
                            case HostTeamXMLProp:
                                string hostTeamName = reader.ReadElementString();
                                var hostFromDb = GetTeam(hostTeamName);

                                //currentMatch.HostTeam = hostFromDb;
                                currentMatch.HostTeamId = hostFromDb.TeamId;
                                break;
                            case GuestTeamXMLProp:
                                string guestTeamName = reader.ReadElementString();
                                var guestFromDb = GetTeam(guestTeamName);

                                //currentMatch.GuestTeam = guestFromDb;
                                currentMatch.GuestTeamId = guestFromDb.TeamId;
                                break;
                            case StadiumXMLProp:
                                string stadiumName = reader.ReadElementString();
                                var stadiumFromDb = GetStadium(stadiumName);
                                //currentMatch.Stadium = stadiumFromDb;
                                currentMatch.StadiumId = stadiumFromDb.StadiumId;
                                break;                                
                            case TownXMLProp:
                                string townName = reader.ReadElementString();
                                var townFromDb = GetTown(townName);
                                //currentMatch.Stadium.Town = townFromDb;
                                break;
                            default:                                
                                break;
                        }
                    }                    
                }

                matches.Add(currentMatch);
            }

            return matches;
        }

        public void SaveMatchReport(string filePath, ICollection<Match> matches)
        {
            var xmlMatches = new List<XElement>();

            foreach (var match in matches)
            {
                xmlMatches.Add(
                    new XElement("Match",
                    new XElement("Date", match.Date),
                    new XElement("HostTeam", match.HostTeam.TeamName),
                    new XElement("GuestTeam", match.GuestTeam.TeamName),
                    new XElement("Stadium", match.Stadium.Name),
                    new XElement("Town", match.Stadium.Town.TownName)
                    ));
            }

            var xmlSerializedMatches = new XElement("Matches", xmlMatches);
            xmlSerializedMatches.Save(filePath);
        }        

        private Stadium GetStadium(string name){
            using (var db = new ChampionsLeagueContext())
            {
                var stadium = db.Stadiums.FirstOrDefault(x => x.Name == name);
                if (stadium == null)
                {
                    Console.WriteLine("Create NEW stadium"); 
                    stadium = new Stadium() { Name = name };
                }
                return stadium;
            }
        }
        private Team  GetTeam(string name)
        {
            using (var db = new ChampionsLeagueContext())
            {
                var team = db.Teams.FirstOrDefault(x => x.TeamName == name);
                if (team == null)
                {
                    Console.WriteLine("Create NEW Team");
                    team = new Team() { TeamName = name };
                }
                return team;
            }
        }
        private Town GetTown(string name)
        {
            using (var db = new ChampionsLeagueContext())
            {
                var town = db.Towns.FirstOrDefault(x => x.TownName == name);
                if (town == null)
                {
                    Console.WriteLine("Create NEW Town");
                    town = new Town() { TownName = name};
                }
                return town;
            }
        }
    }
}