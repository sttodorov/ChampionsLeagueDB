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
        private const string StadiumTeamXMLProp = "Stadium";        

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
                                //currentMatch.HostTeamId = GetTeamId(hostTeamName);
                                currentMatch.HostTeam = GetTeamId(hostTeamName);
                                break;
                            case GuestTeamXMLProp:
                                string guestTeamName = reader.ReadElementString();
                                //currentMatch.GuestTeamId = GetTeamId(guestTeamName);
                                currentMatch.GuestTeam = GetTeamId(guestTeamName);
                                break;
                            case StadiumTeamXMLProp:
                                string stadiumName = reader.ReadElementString();
                                currentMatch.StadiumId = GetStadiumId(stadiumName);
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

        private Team GetTeamId(string name)
        {
            int teamId = 0;
            var team = new Team();

            using (var db = new ChampionsLeagueContext())
            {
                //var team = db.Teams.FirstOrDefault(x => x.TeamName == name);
                team = db.Teams.FirstOrDefault(x => x.TeamName == name);

                if (team == null)
                {
                    throw new ArgumentNullException("Cannot find team in database");
                }
                teamId = team.TeamId;
            }
            return team;
        }

        private int GetStadiumId(string name)
        {
            int stadiumId = 0;

            using (var db = new ChampionsLeagueContext())
            {
                var stadium = db.Stadiums.FirstOrDefault(x => x.Name == name);

                if (stadium == null)
                {
                    throw new ArgumentNullException("Cannot find team in database");
                }
                stadiumId = stadium.StadiumId;
            }
            return stadiumId;
        }
    }
}
