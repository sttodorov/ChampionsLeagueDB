namespace ChampionsLeague.XMLData
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;
    using System.Data.Entity.Validation;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;
    using System.Data.Entity.Validation;
    using System.Collections.Generic;
    using ChampionsLeague.MongoDb.Model;
    using ChampionsLeague.MongoDb.Data;

    public class XMLDataManager
    {
        private static Random random = new Random();

        XMLParser parser;

        public XMLDataManager()
        {
            this.parser = new XMLParser();
        }

        public ICollection<MongoMatch> GetMatchesFromXML(string filename)
        {
            return this.parser.LoadMatchReport(filename);
        }

        public void SaveMatchesInMongoDb(ICollection<MongoMatch> matches, MongoDbData mongoDb)
        {
            foreach (var match in matches)
            {
                mongoDb.Matches.Insert(new MongoMatch()
                {
                    Date = match.Date,
                    GuestTeam = match.GuestTeam,
                    HostTeam = match.HostTeam,
                    Stadium = match.Stadium,
                    Attendance = match.Attendance
                });
            }

            //using (var db = new ChampionsLeagueContext())
            //{
            //    foreach (var match in matches)
            //    {
            //        db.Matches.Add(match);
            //    }
            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbEntityValidationException e)
            //    {
            //        Console.WriteLine(e.Message);

            //        foreach (var item in e.EntityValidationErrors)
            //        {
            //            foreach (var err in item.ValidationErrors)
            //            {
            //                Console.WriteLine(err.ErrorMessage);
            //            }
            //        }
            //    }
            //}
        }

        public void GenerateMatchesReport(string filename)
        {
            using (var db = new ChampionsLeagueContext())
            {
                var matches = db.Matches;
                this.parser.SaveMatchReport(filename, matches.ToList());
            }
        }

        public ICollection<Player> GetPlayersFromXML(string filename)
        {
            return this.parser.LoadPlayers(filename);
        }

        public void SavePlayersInSQLDb(ICollection<Player> players)
        {
            using (var db = new ChampionsLeagueContext())
            {
                foreach (var player in players)
                {
                    db.Players.Add(player);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);

                    foreach (var item in e.EntityValidationErrors)
                    {
                        foreach (var err in item.ValidationErrors)
                        {
                            Console.WriteLine(err.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}
