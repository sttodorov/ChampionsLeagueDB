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
    using ChampionsLeague.ExcelReport;
    using ChampionsLeague.XMLData;
    using ChampionsLeague.PdfReporter;

    public class Program
    {
        public static void Main()
        {
            // Initialize DataBases
            var mongoDb = new MongoDbData();
            var db = new ChampionsLeagueData();

            //Test insert data in mongo
            int mongoTeamsCount = mongoDb.Teams.GetAll().Count;
            if (mongoTeamsCount == 0)
            {
                mongoDb.DataInitilizer();
                Console.WriteLine("\t Mongo has Data!");
            }

            //Test Transfer data from Mongo to SQl
            int sqlTeamsCount = db.Teams.All().Count();
            if (sqlTeamsCount == 0)
            {
                TransferDataFromMongo(mongoDb, db);
                Console.WriteLine("\t SQL Database Created!");
            }

            // import zip
            int matchesCount = db.Matches.All().Count();
            if (matchesCount == 0)
            {
                string tempDirectoryPath = @"..\..\Temp";
                string importDirectoryPath = @"..\..\";
                string zipFileName = "Matches-Report.zip";
                var zipReader = new ZipReader(db, importDirectoryPath, tempDirectoryPath);
                zipReader.ReadFile(zipFileName, "B3:F50");
                Console.WriteLine("\t Zip file imported!");
            }

            //Loads XML into db
            var xmlManager = new XMLDataManager();

            int playersCount = db.Players.All().Count();
            if (playersCount == 0)
            {
                var players = xmlManager.GetPlayersFromXML(@"..\..\players.xml");
                xmlManager.SavePlayersInSQLDb(players);
                Console.WriteLine("\t Players imported!");
            }
            
            // JSON Reports
            string reportsDirectoryPath = @"..\..\JsonReports";
            //var json = new JsonReport(db, reportsDirectoryPath);
            //json.GenerateAllTeams();
            //Console.WriteLine("\t JSON reports generated!");

            //Use MySql and SQLite Databases
            var exl = new ExcelGenerator(reportsDirectoryPath);

            //Transfer data from JSON to MySql Database
            //exl.MySqlDb.LoadJsonReportsInMySql();
            //Console.WriteLine("Json reports loaded in Mysql");

            //Generate Xlsx file
            //exl.GenerateReport();
            //Console.WriteLine("\t Excel Salary Report Created!");

            //Generate/Load From XML                
            string path = @"..\..\matchReport.xml";
            var matchesFromXml = xmlManager.GetMatchesFromXML(path);

            //xmlManager.GenerateMatchesReport(path);
            //Console.WriteLine("\t XML Matches Report Generated!");

            //xmlManager.SaveMatchesInSQLDb(matchesFromXml);

            //Add matches from XML to Mongo

            //TODO: Get teams and stadiums names
            //foreach (var match in matchesFromXml)
            //{
            //   mongoDb.Matches.Insert(new MongoMatch()
            //  {
            //        Date = match.Date.Day + "." + match.Date.Month + "." + match.Date.Year,
            //        GuestTeam = match.GuestTeamId.ToString(),
            //        HostTeam = match.HostTeamId.ToString(),
            //        Stadium = match.StadiumId.ToString()
            //        //Town = match.Stadium.TownId.ToString()
            //    });
            //}
            //var fromMongo = mongoDb.Matches.GetAll();
            //foreach (var match in fromMongo)
            //{
            //    Console.WriteLine(match.Date + " -> " + match.GuestTeam + " vs " + match.HostTeam);
            //}

            //PDFReports
            //var pdfReporter = new PdfReporter();
            //var matches = db.Matches.All().OrderBy(d => d.Date).GroupBy(d => d.Date).ToList();
            //pdfReporter.CreateTableReport(matches); 
        }

        public static void TransferDataFromMongo(MongoDbData mongoDb, ChampionsLeagueData db)
        {
            var allStadiums = mongoDb.Stadiums.GetAll();
            var allTeams = mongoDb.Teams.GetAll();

            foreach (var town in mongoDb.Towns.GetAll())
            {
                var sqlTown = new Town()
                {
                    TownName = town.TownName
                };

                db.Towns.Add(sqlTown);

                var stadiumsInCurrTown = allStadiums.Where(t => t.Town == sqlTown.TownName);

                foreach (var stad in stadiumsInCurrTown)
                {
                    var sqlStadium = new Stadium()
                    {
                        Name = stad.Name,
                        Town = sqlTown,
                        Capacity = stad.Capacity
                    };
                    db.Stadiums.Add(sqlStadium);
                }
                var allTeamsInCurrTown = allTeams.Where(t => t.Town == sqlTown.TownName);

                foreach (var team in allTeamsInCurrTown)
                {
                    var sqlTeam = new Team()
                    {
                        TeamName = team.Name,
                        Town = sqlTown
                    };
                    db.Teams.Add(sqlTeam);
                }
            }

            db.SaveChanges();
        }
    }
}