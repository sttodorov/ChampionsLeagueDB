namespace ChampionsLeague.MongoDb.Data
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using ChampionsLeague.MongoDb.Model;

    public class MongoDbData : IMongoDbData
    {
        private MongoDatabase database;
        private IDictionary<Type, object> repositories;

        public MongoDbData()
        {
            //In separate method with connection string
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            this.database = mongoServer.GetDatabase("ChampionsLeague");

            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<MongoTown> Towns 
        {
            get
            {
                //return this.GetRepository<MongoTown>();
                return new MongoDbRepository<MongoTown>(this.database);
            }
        }
        public IRepository<MongoStadium> Stadiums
        {
            get
            {
                //return this.GetRepository<MongoStadium>();
                return new MongoDbRepository<MongoStadium>(this.database);
            }
        }

        private IRepository<T> GetRepository<T>() where T:EntityBase
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(IRepository<T>);

                //TODO: Fix Activator
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
