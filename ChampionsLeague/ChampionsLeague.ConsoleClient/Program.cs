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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChampionsLeagueContext, Configuration>());

            var db = new ChampionsLeagueContext();

            var town = new Town() { TownName = "Sofia" };
            var stadium = new Stadium() { Name = "Vasil Levski", Town = town, Capacity = 45000 };
            var team = new Team() { TeamName = "Levski", Town = town };
            var player = new Player() { FirstName = "Valeri", LastName = "Bojinov", Team = team };

            db.Towns.Add(town);
            db.Stadiums.Add(stadium);
            db.Teams.Add(team);
            db.Players.Add(player);

            db.SaveChanges();

            var getPlayer = db.Players.FirstOrDefault();
            Console.WriteLine(getPlayer.FirstName + " " + getPlayer.LastName + " is in " + getPlayer.Team.TeamName);

        }
    }
}
