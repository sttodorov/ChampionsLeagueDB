namespace ChampionsLeague.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using ChampionsLeague.Model;

    public interface IChampionsLeagueContext
    {
        IDbSet<Town> Towns { get; set; }

        IDbSet<Stadium> Stadiums { get; set; }
        
        IDbSet<Team> Teams { get; set; }
        
        IDbSet<Player> Players { get; set; }
        
        IDbSet<Match> Matches { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
