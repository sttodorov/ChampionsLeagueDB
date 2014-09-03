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

    public class ConsoleClientEntryPoint
    {
        public static void Main()
        {
            const string MONGO_ACKNOWLEDGEMENT = "\t Mongo has Data!";
            const string SQL_ACKNOWLEDGEMENT = "\t SQL Database Created!";
            const string ZIP_ACKNOWLEDGEMENT = "\t Zip file imported!";
            const string PLAYERS_DATA_IMPORTED = "\t Players imported!";
            const string JSON_ACKNOWLEDGEMENT = "\t JSON reports generated!";

            // Initialize DataBases
            var mongoDb = new MongoDbData();
            var db = new ChampionsLeagueData();

            //Test insert data in mongo
            int mongoTeamsCount = mongoDb.Teams.GetAll().Count;
            if (mongoTeamsCount == 0)
            {
                mongoDb.DataInitilizer();
                Console.WriteLine(MONGO_ACKNOWLEDGEMENT);
            }

            //Test Transfer data from Mongo to SQl
            int sqlTeamsCount = db.Teams.All().Count();
            if (sqlTeamsCount == 0)
            {
                TransferDataFromMongo(mongoDb, db);
                Console.WriteLine(SQL_ACKNOWLEDGEMENT);
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
                Console.WriteLine(ZIP_ACKNOWLEDGEMENT);
            }

            //Loads XML into db
            var xmlManager = new XMLDataManager();
            var xmlFilePath = @"..\..\players.xml";

            int playersCount = db.Players.All().Count();
            if (playersCount == 0)
            {
                var players = xmlManager.GetPlayersFromXML(xmlFilePath);
                xmlManager.SavePlayersInSQLDb(players);
                Console.WriteLine(PLAYERS_DATA_IMPORTED);
            }

            // JSON Reports
            string reportsDirectoryPath = @"..\..\JsonReports";
            var json = new JsonReport(db, reportsDirectoryPath);
            json.GenerateAllTeams();
            Console.WriteLine(JSON_ACKNOWLEDGEMENT);

            //Use MySql and SQLite Databases
            var exl = new ExcelGenerator(reportsDirectoryPath);

            //Transfer data from JSON to MySql Database
            string jsonToMySQLSuccessMsg = "\t Json reports loaded in Mysql";
            
            if (exl.MySqlDb.GetAllTeams().Count == 0)
            {
                exl.MySqlDb.LoadJsonReportsInMySql();
                Console.WriteLine(jsonToMySQLSuccessMsg);
            }

            //Generate Xlsx file

            //If Excel file exists throw exception 
            exl.GenerateReport();
            string excelSuccessMsg = "\t Excel Salary Report Created!";
            Console.WriteLine(excelSuccessMsg);

            //Generate/Load From XML                
            string path = @"..\..\matchReport.xml";
            string xmlSuccessMsg = "\t XML Matches Report Generated!";
            xmlManager.GenerateMatchesReport(path);
            Console.WriteLine(xmlSuccessMsg);

            //Add matches from XML to Mongo            
            var fromMongo = mongoDb.Matches.GetAll();
            
            if(fromMongo.Count == 0)
            {
                var matchesFromXml = xmlManager.GetMatchesFromXML(path);
                xmlManager.SaveMatchesInMongoDb(matchesFromXml, mongoDb);
            }
            foreach (var match in fromMongo)
            {
                Console.WriteLine("{0} -> {1} vs {2} Attendance: {3}", match.Date, match.GuestTeam, match.HostTeam, match.Attendance);
            }

            //PDFReports
            var pdfReporter = new PdfReporter();
            var matches = db.Matches.All().OrderBy(d => d.Date).GroupBy(d => d.Date).ToList();
            pdfReporter.CreateTableReport(matches); 
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