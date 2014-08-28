namespace ChampionsLeague.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
