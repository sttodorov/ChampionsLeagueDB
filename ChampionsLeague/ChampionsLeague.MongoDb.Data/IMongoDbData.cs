using ChampionsLeague.MongoDb.Model;
namespace ChampionsLeague.MongoDb.Data
{
    public interface IMongoDbData
    {
        IRepository<MongoTown> Towns { get; }

        IRepository<MongoStadium> Stadiums { get; }
    }
}
