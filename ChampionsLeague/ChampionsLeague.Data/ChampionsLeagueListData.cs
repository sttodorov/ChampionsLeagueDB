namespace ChampionsLeague.Data
{
    using ChampionsLeague.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChampionsLeagueListData
    {
        public List<Town> Towns { get; set; }
        public List<Stadium> Stadiums { get; set; }
        public List<Team> Teams { get; set; }
        public List<Player> Players { get; set; }
        public List<Match> Matches { get; set; }

        public ChampionsLeagueListData()
        {
            this.Towns = new List<Town>();
            this.Stadiums = new List<Stadium>();
            this.Teams = new List<Team>();
            this.Players = new List<Player>();
            this.Matches = new List<Match>();
        }
    }
}
