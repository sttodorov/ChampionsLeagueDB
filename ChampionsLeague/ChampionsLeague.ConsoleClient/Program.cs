namespace ChampionsLeague.ConsoleClient
{
    using System;
    using System.Linq;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;

    public class Program
    {
        static void Main()
        {

            var db = new ChampionsLeagueData();

            var sofiaTown = new Town() { TownName = "Sofia" };
            var levskiStadium = new Stadium() { Name = "Vasil Levski", Town = sofiaTown, Capacity = 45000 };
            var levski = new Team() { TeamName = "Levski", Town = sofiaTown };
            var levskiPlayer = new Player() { FirstName = "Valeri", LastName = "Bojinov", Team = levski };

            db.Towns.Add(sofiaTown);
            db.Stadiums.Add(levskiStadium);
            db.Teams.Add(levski);
            db.Players.Add(levskiPlayer);


            var plovdiv = new Town() { TownName = "Plovdiv" };
            var cskaStadium = new Stadium() { Name = "BG army", Town = plovdiv, Capacity = 30000 };
            var cska = new Team() { TeamName = "CSKA", Town = plovdiv };
            var cskaPlayer = new Player() { FirstName = "Spas", LastName = "Delev", Team = cska };

            db.Towns.Add(plovdiv);
            db.Stadiums.Add(cskaStadium);
            db.Teams.Add(cska);
            db.Players.Add(cskaPlayer);

            var match = new Match()
            {
                Stadium = levskiStadium,
                Date = DateTime.Now,
                HostTeam = levski,
                GuestTeam = cska
            };

            db.Matches.Add(match);

            db.SaveChanges();
            
            var allPlayers = db.Players.All().Select(p => new { 
                FirstName = p.FirstName,
                LastName = p.LastName,
                TeamName = p.Team.TeamName
            });
            foreach (var player  in allPlayers)
            {            
                Console.WriteLine(player.FirstName + " " + player.LastName + " is in " + player.TeamName);                    
            }

            var firstMatch = db.Matches.All().FirstOrDefault();
            Console.WriteLine(firstMatch.HostTeam.TeamName + " vs. " + firstMatch.GuestTeam.TeamName);

            var levskiMatches = db.Teams.All().Where(t => t.TeamName == "Levski").Select(t => new { 
                asHost = t.MatchesAsHost,
                asGuest = t.MatchesAsGuest
            });

            Console.WriteLine("Games:");

            foreach (var lavskiMatch in levskiMatches)
            {
                foreach (var hostMatch in lavskiMatch.asHost)
                {
                    Console.WriteLine(hostMatch.Date);
                }
            }
        }
    }
}