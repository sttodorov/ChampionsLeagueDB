using System;
using System.Collections.Generic;
using System.Linq;
using ChampionsLeague.XMLData;
using ChampionsLeague.Model;
using ChampionsLeague.Data;


namespace ChampionsLeague.XMLParserTest
{
    class Program
    {
        static void Main(string[] args)
        {            

            string path = @"..\..\matchReport.xml";

            var reportManager = new XMLReportManager();

            //reportManager.SaveMatchReportsFromDb(path);
            reportManager.LoadMatchReportsInDb(path);
        }
    }
}
