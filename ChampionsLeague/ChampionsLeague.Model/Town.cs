namespace ChampionsLeague.Model
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        private ICollection<Stadium> stadiums;
        private ICollection<Team> teams;

        public Town()
        {
            this.Stadiums = new HashSet<Stadium>();
            this.Teams = new HashSet<Team>();
        }

        public int TownId { get; set; }

        [Required]
        public string TownName { get; set; }

        public virtual ICollection<Stadium> Stadiums 
        {
            get 
            {
                return this.stadiums;
            }
            set
            {
                this.stadiums = value;
            }
        }

        public virtual ICollection<Team> Teams
        {
            get
            {
                return this.teams;
            }
            set
            {
                this.teams = value;
            }
        }

    }
}
