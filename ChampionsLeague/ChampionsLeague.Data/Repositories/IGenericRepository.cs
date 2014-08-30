namespace ChampionsLeague.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
     
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All();

        IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions);

        void Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        void Detach(T entity);

        void SaveChanges();
    }
}
