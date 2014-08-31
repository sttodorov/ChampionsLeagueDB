namespace ChampionsLeague.ExcelReport
{
    using System.Collections.Generic;

    using ChampionsLeague.Model;

    public class DeserializedObject
    {
        public int Id { get; set; }
        public string Team { get; set; }
        public string Town { get; set; }
        public IList<Player> Players { get; set; }
    }
}
