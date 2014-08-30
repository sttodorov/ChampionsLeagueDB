namespace ChampionsLeague.Data
{
    using ChampionsLeague.Data.Repositories;
    using ChampionsLeague.Model;
    using System;
    using System.Collections.Generic;

    public class ChampionsLeagueData : IChamionsLeagueData
    {
        private IChampionsLeagueContext context;
        private IDictionary<Type, object> repositories;

        public ChampionsLeagueData()
            :this(new ChampionsLeagueContext())
        {
        }

        public ChampionsLeagueData(IChampionsLeagueContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Town> Towns
        {
            get
            {
                return this.GetRepository<Town>();
            }
        }

        public IGenericRepository<Stadium> Stadiums
        {
            get
            {
                return this.GetRepository<Stadium>();
            }
        }

        public IGenericRepository<Team> Teams
        {
            get
            {
                return this.GetRepository<Team>();
            }
        }

        public IGenericRepository<Player> Players
        {
            get
            {
                return this.GetRepository<Player>();
            }
        }

        public IGenericRepository<Match> Matches
        {
            get
            {
                return this.GetRepository<Match>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T:class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
