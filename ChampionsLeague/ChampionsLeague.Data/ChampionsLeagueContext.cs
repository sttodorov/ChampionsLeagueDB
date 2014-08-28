namespace ChampionsLeague.Data
{
    using System.Data.Entity;

    using ChampionsLeague.Model;

    public class ChampionsLeagueContext : DbContext
    {
        public ChampionsLeagueContext()
            : base("ChampionsLeagueDb")
        {
        }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Stadium> Stadiums { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
