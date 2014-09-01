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
                return this.GetRepository<MongoTown>();
            }
        }
        public IRepository<MongoStadium> Stadiums
        {
            get
            {
                return this.GetRepository<MongoStadium>();
            }
        }

        public IRepository<MongoTeam> Teams
        {
            get
            {
                return this.GetRepository<MongoTeam>();
            }
        }
        public IRepository<MongoMatch> Matches
        {
            get
            {
                return this.GetRepository<MongoMatch>();
            }
        }

        private IRepository<T> GetRepository<T>() where T:EntityBase
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(MongoDbRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type,this.database));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
