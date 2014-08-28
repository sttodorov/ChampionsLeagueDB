namespace ChampionsLeague.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Stadium
    {
        public int StadiumId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        public virtual Town Town { get; set; }

        [Range(1000, int.MaxValue)]
        public int Capacity { get; set; }
    }
}
