using System;
using System.Collections.Generic;
using System.Linq;
using ChampionsLeague.XMLDataParser;
using ChampionsLeague.Model;
using ChampionsLeague.Data;


namespace ChampionsLeague.XMLParserTest
{
    class Program
    {
        static void Main(string[] args)
        {            

            string path = @"..\..\matchReport.xml";

            var parser = new XMLParser();                        

            using(var db = new ChampionsLeagueContext())
            {
                var matches = db.Matches.ToList();                
                parser.SaveMatchReport(path, matches);
            }
           

            var m = parser.LoadMatchReport(path);
            
            foreach (var item in m)
            {
                Console.WriteLine(item.HostTeamId);
            }
        }
    }
}
