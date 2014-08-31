namespace ChampionsLeague.ConsoleClient
{
    using System;
    using System.Linq;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;
    using ChampionsLeague.Import;
    using ChampionsLeague.JsonReports;

    using ChampionsLeague.MongoDb.Model;
    using ChampionsLeague.MongoDb.Data;

    public class Program
    {

        public static void InsetDataInSqlServer(ChampionsLeagueData db)
        {
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
        }

        static void Main()
        {
            
            var mongoDb = new MongoDbData();

            //Test insert data in mongo

            mongoDb.Towns.Insert(new MongoTown() { TownName = "Sofia" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Razgrad" });

            var stadSofia = new MongoStadium
            {
                Name = "Vasil LEvski",
                Town = "Sofia",
                Capacity = 45000
            };
            var stadRazgrad = new MongoStadium
            {
                Name = "Ludogorets Arena",
                Town = "Razgrad",
                Capacity = 5000
            };

            mongoDb.Stadiums.Insert(stadSofia);
            mongoDb.Stadiums.Insert(stadRazgrad);

            foreach (var town in mongoDb.Towns.GetAll())
            {
                Console.WriteLine(town.TownName);
            }
            foreach (var stad in mongoDb.Stadiums.GetAll())
            {
                Console.WriteLine(stad.Name + " - " + stad.Town);
            }

            //---------------------------------------
            
            //Test Transfer data from Mongo to SQl
            var db = new ChampionsLeagueData();

            // Work Slow
            foreach (var town in mongoDb.Towns.GetAll())
            {
                Console.WriteLine(town.TownName);
                var sqlTown = new Town()
                {
                    TownName = town.TownName
                };

                db.Towns.Add(sqlTown);

                var stadiumsInCurrTown = mongoDb.Stadiums.GetAll().Where(t => t.Town == sqlTown.TownName);
                
                foreach (var stad in stadiumsInCurrTown)
                {
                    Console.WriteLine("     " + stad.Name);
                    var sqlStadium = new Stadium()
                    {
                        Name = stad.Name,
                        Town = sqlTown,
                        Capacity = stad.Capacity
                    };
                    db.Stadiums.Add(sqlStadium);
                }
            }

            db.SaveChanges();

            //------------------------------------
            
            //Test insert data in SQL Server

            //var db = new ChampionsLeagueData();
            //Use to insert Data into SQL Server
            //InsetDataInSqlServer(db);

            //------------------------------------

            //Test load data From SQL
            //TO DELETE

            //var allPlayers = db.Players.All().Select(p => new
            //{
            //    FirstName = p.FirstName,
            //    LastName = p.LastName,
            //    TeamName = p.Team.TeamName
            //});
            //foreach (var player in allPlayers)
            //{
            //    Console.WriteLine(player.FirstName + " " + player.LastName + " is in " + player.TeamName);
            //}

            //    var firstMatch = db.Matches.All().FirstOrDefault();
            //    Console.WriteLine(firstMatch.HostTeam.TeamName + " vs. " + firstMatch.GuestTeam.TeamName);

            //    var levskiMatches = db.Teams.All().Where(t => t.TeamName == "Levski").Select(t => new { 
            //        asHost = t.MatchesAsHost,
            //        asGuest = t.MatchesAsGuest
            //    });

            //    Console.WriteLine("Games:");

            //    foreach (var lavskiMatch in levskiMatches)
            //    {
            //        foreach (var hostMatch in lavskiMatch.asHost)
            //        {
            //            Console.WriteLine(hostMatch.Date);
            //        }
            //    }

            // import zip
            string tempDirectoryPath = @"..\..\Temp";
            string importDirectoryPath = @"..\..\";
            string zipFileName = "Sample-Sales-Reports.zip";

            var zipReader = new ZipReader(db, importDirectoryPath, tempDirectoryPath);
            var matches = zipReader.ReadFile(zipFileName);
            Console.WriteLine("\t Zip file imported! {0} matches extracted", matches.Count);

            // JSON Reports
            string reportsDirectoryPath = @"..\..\JsonReports";
            var json = new JsonReport(db, reportsDirectoryPath);
            json.GenerateAllTeams();
            Console.WriteLine("\t JSON reports generated!");

        }
    }
}