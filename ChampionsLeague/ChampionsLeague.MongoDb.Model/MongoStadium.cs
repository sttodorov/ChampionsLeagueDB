namespace ChampionsLeague.MongoDb.Model
{
    public class MongoStadium : EntityBase
    {
        public string Name { get; set; }

        public string Town { get; set; }

        public int Capacity { get; set; }
    }
}
