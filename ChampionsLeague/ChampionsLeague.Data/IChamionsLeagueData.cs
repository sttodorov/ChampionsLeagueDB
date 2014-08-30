namespace ChampionsLeague.Data
{
    using ChampionsLeague.Data.Repositories;
    using ChampionsLeague.Model;

    public interface IChamionsLeagueData
    {
        IGenericRepository<Town> Towns {get;}

        IGenericRepository<Stadium> Stadiums { get; }

        IGenericRepository<Team> Teams { get; }

        IGenericRepository<Player> Players { get; }

        IGenericRepository<Match> Matches { get; }

    }
}
