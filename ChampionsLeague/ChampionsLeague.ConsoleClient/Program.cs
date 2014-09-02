﻿namespace ChampionsLeague.ConsoleClient
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

        static void Main()
        {
            // Initialize DataBases

            var mongoDb = new MongoDbData();
            var db = new ChampionsLeagueData();

            //Test insert data in mongo
            //mongoDb.DataInitilizer();

            //Test Transfer data from Mongo to SQl
            //TransferDataFromMongo(mongoDb, db);

            // import zip
            //string tempDirectoryPath = @"..\..\Temp";
            //string importDirectoryPath = @"..\..\";
            //string zipFileName = "Sample-Sales-Reports.zip";

            //var zipReader = new ZipReader(db, importDirectoryPath, tempDirectoryPath);
            //var matches = zipReader.ReadFile(zipFileName);
            //Console.WriteLine("\t Zip file imported! {0} matches extracted", matches.Count);

            // JSON Reports
            string reportsDirectoryPath = @"..\..\JsonReports";
            var json = new JsonReport(db, reportsDirectoryPath);
            json.GenerateAllTeams();
            Console.WriteLine("\t JSON reports generated!");

            //Use MySql and SQLite Databases
            //var exl = new ExcelGenerator(reportsDirectoryPath);

            //Create table in SQLite
            //Run only first tume
            //exl.SQLiteDb.InitializeTable();

            //Transfer data from JSON to MySql Database
            //exl.MySqlDb.LoadJsonReportsInMySql();
            //exl.GenerateReport();
            /*
            //Generate/Load From XML                
            string path = @"..\..\matchReport.xml";

            var xmlManager = new XMLDataManager();
            var matchesFromXml = xmlManager.GetMatchesFromXML(path);

            xmlManager.GenerateMatchesReport(path);
            //xmlManager.SaveMatchesInSQLDb(matchesFromXml);

            //Loads XML into db
            var players = xmlManager.GetPlayersFromXML(@"..\..\players.xml");
            xmlManager.SavePlayersInSQLDb(players);

            //Add matches from XML to Mongo
            //TODO: Get teams and stadiums names
            foreach (var match in matchesFromXml)
            {
                mongoDb.Matches.Insert(new MongoMatch()
                {

                    Date = match.Date.ToString(),
                    GuestTeam = match.GuestTeamId.ToString(),
                    HostTeam = match.HostTeamId.ToString(),
                    Stadium = match.StadiumId.ToString()
                    //Town = match.Stadium.TownId.ToString()
                });
            }
            var fromMongo = mongoDb.Matches.GetAll();
            //foreach (var match in fromMongo)
            //{
            //    Console.WriteLine(match.Date);
            //    Console.WriteLine(match.GuestTeam + " vs " + match.HostTeam);
            //}

            //PDFReports
            //var pdfReporter = new PdfReporter();
            //var matches = db.Matches.All().OrderBy(d => d.Date).GroupBy(d => d.Date).ToList();
            //pdfReporter.CreateTableReport(matches);
        }
    }
}