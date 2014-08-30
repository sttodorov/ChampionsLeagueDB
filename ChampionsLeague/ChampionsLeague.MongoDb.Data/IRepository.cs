namespace ChampionsLeague.MongoDb.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB.Bson;
    using System.Collections.Generic;

    using ChampionsLeague.MongoDb.Model;

    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Insert(TEntity entity); // Should be bool

        void Update(TEntity entity);// Should be bool

        TEntity Delete(TEntity entity);// Should be bool

        IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> perdicate); // Should be IList

        IList<TEntity> GetAll();

        TEntity GetById(ObjectId id);
        
    }
}
