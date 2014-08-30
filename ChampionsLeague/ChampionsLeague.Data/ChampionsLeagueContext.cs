namespace ChampionsLeague.Data
{
    using System.Data.Entity;

    using ChampionsLeague.Model;
    using ChampionsLeague.Data.Migrations;

    public class ChampionsLeagueContext : DbContext, IChampionsLeagueContext
    {
        public ChampionsLeagueContext()
            : base("ChampionsLeagueDb")
        {

            //Database.SetInitializer(new DropCreateDatabaseAlways<ChampionsLeagueContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChampionsLeagueContext, Configuration>());
        }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Stadium> Stadiums { get; set; }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<Player> Players { get; set; }

        public IDbSet<Match> Matches { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Match>()
            //    .HasRequired(s => s.Stadium)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasRequired(t => t.HostTeam)
                .WithMany(e => e.MatchesAsHost)
                .HasForeignKey(m => m.HostTeamId)//m.HostTeam
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasRequired(t => t.GuestTeam)
                .WithMany(e => e.MatchesAsGuest)
                .HasForeignKey(m => m.GuestTeamId)// m.GuestTeam
                .WillCascadeOnDelete(false);
        }
    }
}
