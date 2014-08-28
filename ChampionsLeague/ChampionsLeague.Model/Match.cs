using System;
using System.ComponentModel.DataAnnotations;
namespace ChampionsLeague.Model
{
    public class Match
    {
        public int MatchId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int HostTeamId { get; set; }

        public virtual Team HostTeam { get; set; }

        [Required]
        public int GuestTeamId { get; set; }

        public virtual Team GuestTeam { get; set; }
    }
}
