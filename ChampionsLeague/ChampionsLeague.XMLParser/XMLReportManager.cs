namespace ChampionsLeague.XMLData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ChampionsLeague.Data;
    using ChampionsLeague.Model;
    

    public class XMLReportManager
    {
        public void LoadMatchReportsInDb(string filename)
        {
            var parser = new XMLParser();

            var matches = parser.LoadMatchReport(filename);

            var db = new ChampionsLeagueData();
            //using(var db = new ChampionsLeagueData())
            //{
                foreach (var match in matches)
                {
                    db.Matches.Add(match);
                }
            //}
            throw new NotImplementedException();
        }

        public void SaveMatchReportsFromDb(string filename, IQueryable<Match> mathces)
        {
            throw new NotImplementedException();
        }
    }
}
