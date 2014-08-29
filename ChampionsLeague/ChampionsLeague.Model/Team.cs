namespace ChampionsLeague.Model
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        private ICollection<Player> players;
        private ICollection<Match> matchesAsHost;
        private ICollection<Match> matchesAsGuest;

        public Team()
        {
            this.Players = new HashSet<Player>();
            this.MatchesAsHost = new HashSet<Match>();
            this.MatchesAsGuest = new HashSet<Match>();
        }

        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        public virtual Town Town { get; set; }

        public virtual ICollection<Player> Players
        {
            get
            {
                return this.players;
            }
            set
            {
                this.players = value;
            }
        }

        public virtual ICollection<Match> MatchesAsHost
        {
            get
            {
                return this.matchesAsHost;
            }
            set
            {
                this.matchesAsHost = value;
            }
        }

        public ICollection<Match> MatchesAsGuest
        {
            get
            {
                return this.matchesAsGuest;
            }
            set
            {
                this.matchesAsGuest = value;
            }
        }

    }
}
