namespace ChampionsLeague.XMLData
{
    using System;    
    using System.Linq;
    
    using ChampionsLeague.Data;    
    using ChampionsLeague.Model;
    using System.Data.Entity.Validation;

    public class XMLReportManager
    {
        XMLParser parser;

        public XMLReportManager()
        {
            this.parser = new XMLParser();
        }

        public void LoadMatchReportsInDb(string filename)
        {            
            var matches = this.parser.LoadMatchReport(filename);

            using(var db = new ChampionsLeagueContext())
            {
                foreach (var match in matches)
                {
                    db.Matches.Add(match);
                }
                try
                {
                    db.SaveChanges();
                }
                catch(DbEntityValidationException e)
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

        public void SaveMatchReportsFromDb(string filename)
        { 
            using(var db = new ChampionsLeagueContext())
            {
                var matches = db.Matches;
                this.parser.SaveMatchReport(filename, matches.ToList());                
            }
        }
    }
}
