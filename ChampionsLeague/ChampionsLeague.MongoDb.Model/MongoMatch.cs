namespace ChampionsLeague.MongoDb.Model
{
    using System;

    public class MongoMatch : EntityBase
    {
        public string Date { get; set; }
        public string HostTeam { get; set; }
        public string GuestTeam { get; set; }
        public string Stadium { get; set; }

        public int Attendance { get; set; }
        public string Town { get; set; }
    }
}
