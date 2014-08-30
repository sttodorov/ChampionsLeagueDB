namespace ChampionsLeague.MongoDb.Model
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public abstract class EntityBase
    {
        [BsonId]

        ObjectId Id { get; set; }
    }
}
