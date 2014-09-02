namespace ChampionsLeague.XMLPlayersGeneration
{
    using System;
    using System.Linq;
    using ChampionsLeague.XMLData;
    

    class Program
    {
        static void Main()
        {
            //Generate XML with random players
            var playersGenerator = new XMLPlayersGenerator();
            playersGenerator.LoadPlayersFromFile(@"..\..\playersAsString.txt");
            playersGenerator.SavePlayersAsXML(@"..\..\players.xml");

            //Loads XML into db
            var xmlManager = new XMLDataManager();
            var players = xmlManager.GetPlayersFromXML(@"..\..\players.xml");
            xmlManager.SavePlayersInSQLDb(players);
        }
    }
}
