namespace ChampionsLeague.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Match
    {
        public int MatchId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int StadiumId { get; set; }

        //[Required]
        public virtual Stadium Stadium { get; set; }


        public int HostTeamId { get; set; }

        //[Required]
        public virtual Team HostTeam { get; set; }


        public int GuestTeamId { get; set; }

        //[Required]
        public virtual Team GuestTeam { get; set; }

        public int Attendance { get; set; }
    }
}
