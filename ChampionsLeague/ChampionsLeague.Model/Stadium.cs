namespace ChampionsLeague.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stadium")]
    public class Stadium
    {
        private ICollection<Match> matches;

        public Stadium()
        {
            this.Matches = new HashSet<Match>();
        }

        public int StadiumId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        public virtual Town Town { get; set; }

        [Range(1000, int.MaxValue)]
        public int Capacity { get; set; }

        public virtual ICollection<Match> Matches
        {
            get
            {
                return this.matches;
            }
            set
            {
                this.matches = value;
            }
        }
    }
}
