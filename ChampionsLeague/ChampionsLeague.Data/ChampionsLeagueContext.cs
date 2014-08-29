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
