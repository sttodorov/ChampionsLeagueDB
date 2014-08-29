namespace ChampionsLeague.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using ChampionsLeague.Data;
    using ChampionsLeague.Data.Migrations;
    using ChampionsLeague.Model;

    public class Program
    {
        static void Main()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChampionsLeagueContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChampionsLeagueContext, Configuration>());

            var db = new ChampionsLeagueContext();
            

            var sofiaTown = new Town() { TownName = "Sofia" };
            var levskiStadium = new Stadium() { Name = "Vasil Levski", Town = sofiaTown, Capacity = 45000 };
            var levski = new Team() { TeamName = "Levski", Town = sofiaTown };
            var levskiPlayer = new Player() { FirstName = "Valeri", LastName = "Bojinov", Team = levski };

            db.Towns.Add(sofiaTown);
            db.Stadiums.Add(levskiStadium);
            db.Teams.Add(levski);
            db.Players.Add(levskiPlayer);

            db.SaveChanges();

            var cskaStadium = new Stadium() { Name = "BG army", Town = sofiaTown, Capacity = 30000 };
            var cska = new Team() { TeamName = "CSKA", Town = sofiaTown };
            var cskaPlayer = new Player() { FirstName = "Spas", LastName = "Delev", Team = cska };

            db.Stadiums.Add(cskaStadium);
            db.Teams.Add(cska);
            db.Players.Add(cskaPlayer);

            db.SaveChanges();

            var match = new Match()
            {
                Stadium = levskiStadium,
                Date = DateTime.Now, //new DateTime(2014,07,15),
                HostTeam = levski,
                GuestTeam = cska
            };

            db.Matches.Add(match);

            db.SaveChanges();

            var getPlayer = db.Players.FirstOrDefault();
            Console.WriteLine(getPlayer.FirstName + " " + getPlayer.LastName + " is in " + getPlayer.Team.TeamName);

            var firstMatch = db.Matches.FirstOrDefault();
            Console.WriteLine(firstMatch.HostTeam.TeamName + " vs. " + firstMatch.GuestTeam.TeamName);

            var firstTeam = db.Teams.FirstOrDefault();
           
            Console.WriteLine("Games:");

            foreach (var matchAsHost in levski.MatchesAsHost)
            {
                Console.WriteLine(matchAsHost.Date);
            }

            // test connection
        }
    }
}