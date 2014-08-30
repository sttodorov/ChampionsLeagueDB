namespace ChampionsLeague.MongoDb.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    using ChampionsLeague.MongoDb.Model;

    public class MongoDbRepository<TEntity>
        : IRepository<TEntity> where
            TEntity : EntityBase
    {
        public MongoDatabase Database { get; private set; }
        public MongoCollection<TEntity> Collection { get; private set; }

        public MongoDbRepository(MongoDatabase db)
        {
            this.Database = db;
            this.Collection = this.Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void Insert(TEntity entity)
        {
            this.Collection.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            this.Collection.Save(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            //this.Collection.Remove(Query.EQ("id", entity.Id));
            return entity;
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> perdicate)
        {
            return this.Collection.FindAll().Where(perdicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return this.Collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(ObjectId id)
        {
            return this.Collection.FindOneByIdAs<TEntity>(id);
        }


    }
}
