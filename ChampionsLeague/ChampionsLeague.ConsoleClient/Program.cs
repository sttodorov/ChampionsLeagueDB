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

    public class Program
    {

        public static void InsertDataInMongo(MongoDbData mongoDb)
        {
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Liverpool" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "San Sebastián" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Barcelona" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Valencia" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Islington" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Villarreal" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Elche" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "La Coruña" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Witton" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Torino" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Leicester" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Swansea" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Manchester" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Sevilla" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Bilbao" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Milan" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Madrid" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Parma" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Udine" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Plaermo" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "London" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Birmingham" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Vigo" });
            mongoDb.Towns.Insert(new MongoTown() { TownName = "Turin" });

            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Anfield", Town = "Liverpool", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Anoeta Stadium", Town = "San Sebastián", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Balaidos", Town = "San Sebastián", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Ciutat de Valencia", Town = "Valencia", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Emirates Stadium", Town = "London", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Estadi Cornella-El Prat", Town = "Villarreal", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Estadio El Madrigal", Town = "Villarreal", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Estadio Manuel Martinez Valero", Town = "Elche", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Estadio Nuevo Los Carmenes", Town = "Madrid", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Estadio Riazor", Town = "La Coruña", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Etihad Stadium", Town = "Witton", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Juventus Stadium", Town = "Torino", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "King Power Stadium", Town = "Leicester", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Liberty Stadium", Town = "Swansea", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Mestalla Stadium", Town = "Valencia", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Old Trafford", Town = "Manchester", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Ramon Sanchez Pizjuan Stadium", Town = "Sevila", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "San Mames Stadium", Town = "Bilbao", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "San Siro", Town = "Milan", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Santiago Bernabeu", Town = "Madrid", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Stadio Ennio Tardini", Town = "Parma", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Stadio Friuli", Town = "Udine", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Stadio Renzo Barbera", Town = "Palermo", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Stamford Bridge", Town = "London", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "Villa Park", Town = "Birmingham", Capacity = 45000 });
            mongoDb.Stadiums.Insert(new MongoStadium { Name = "White Hart Lane", Town = "London", Capacity = 45000 });

            mongoDb.Teams.Insert(new MongoTeam() { Name = "Liverpool", Town = "Liverpool" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Real Sociedad", Town = "San Sebastián" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Barcelona", Town = "Barcelona" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Espanyol", Town = "Barcelona" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Valencia", Town = "Valencia" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Granada CF", Town = "Valencia" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Levante", Town = "Valencia" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Islington" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Villarreal", Town = "Villarreal" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Elche", Town = "Elche" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Dep.La Coruna", Town = "La Coruña" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Aston Villa", Town = "Witton" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Torino" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Arsenal", Town = "Leicester" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Leicester", Town = "Leicester" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Swansea", Town = "Swansea" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Manchester City", Town = "Manchester" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Manchester United", Town = "Manchester" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Sevilla", Town = "Sevilla" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Ath Bilbao", Town = "Bilbao" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Ac Milan", Town = "Milan" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Inter", Town = "Milan" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Real Madrid", Town = "Madrid" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Parma", Town = "Parma" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Udinese", Town = "Udine" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Palermo", Town = "Plaermo" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Chelsea", Town = "London" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Tottenham", Town = "London" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Celta Vigo", Town = "Vigo" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Birmingham" });
            mongoDb.Teams.Insert(new MongoTeam() { Name = "Uventus", Town = "Turin" });

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

        static void Main()
        {
            // Initialize DataBases

            var mongoDb = new MongoDbData();
            var db = new ChampionsLeagueData();
            
            //Test insert data in mongo
                //InsertDataInMongo(mongoDb);

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
                //string reportsDirectoryPath = @"..\..\JsonReports";
                //var json = new JsonReport(db, reportsDirectoryPath);
                //json.GenerateAllTeams();
                //Console.WriteLine("\t JSON reports generated!");

            //Use MySql and SQLite Databases
                //var exl = new ExcelGenerator();

            //Transfer data from JSON to MySql Database
                //exl.MySqlDb.LoadJsonReportsInMySql();
                //exl.GenerateReport();
        }
    }
}