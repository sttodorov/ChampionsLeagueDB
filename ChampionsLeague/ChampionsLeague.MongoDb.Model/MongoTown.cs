namespace ChampionsLeague.MongoDb.Model
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class MongoTown : EntityBase
    {
        public string TownName { get; set; }
    }
}
