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

        public void DataInitilizer()
        {
            this.Towns.Insert(new MongoTown() { TownName = "Liverpool" });
            this.Towns.Insert(new MongoTown() { TownName = "San Sebastián" });
            this.Towns.Insert(new MongoTown() { TownName = "Barcelona" });
            this.Towns.Insert(new MongoTown() { TownName = "Valencia" });
            this.Towns.Insert(new MongoTown() { TownName = "Islington" });
            this.Towns.Insert(new MongoTown() { TownName = "Villarreal" });
            this.Towns.Insert(new MongoTown() { TownName = "Elche" });
            this.Towns.Insert(new MongoTown() { TownName = "La Coruña" });
            this.Towns.Insert(new MongoTown() { TownName = "Witton" });
            this.Towns.Insert(new MongoTown() { TownName = "Torino" });
            this.Towns.Insert(new MongoTown() { TownName = "Leicester" });
            this.Towns.Insert(new MongoTown() { TownName = "Swansea" });
            this.Towns.Insert(new MongoTown() { TownName = "Manchester" });
            this.Towns.Insert(new MongoTown() { TownName = "Sevilla" });
            this.Towns.Insert(new MongoTown() { TownName = "Bilbao" });
            this.Towns.Insert(new MongoTown() { TownName = "Milan" });
            this.Towns.Insert(new MongoTown() { TownName = "Madrid" });
            this.Towns.Insert(new MongoTown() { TownName = "Parma" });
            this.Towns.Insert(new MongoTown() { TownName = "Udine" });
            this.Towns.Insert(new MongoTown() { TownName = "Palermo" });
            this.Towns.Insert(new MongoTown() { TownName = "London" });
            this.Towns.Insert(new MongoTown() { TownName = "Birmingham" });
            this.Towns.Insert(new MongoTown() { TownName = "Vigo" });
            this.Towns.Insert(new MongoTown() { TownName = "Turin" });
            this.Towns.Insert(new MongoTown() { TownName = "Getafe" });
            this.Towns.Insert(new MongoTown() { TownName = "Sassuolo" });
            this.Towns.Insert(new MongoTown() { TownName = "West Bromwich" });
            this.Towns.Insert(new MongoTown() { TownName = "Southampton" });
            this.Towns.Insert(new MongoTown() { TownName = "Malaga" });
            this.Towns.Insert(new MongoTown() { TownName = "Almeria" });
            this.Towns.Insert(new MongoTown() { TownName = "Naples" });
            this.Towns.Insert(new MongoTown() { TownName = "Kingston upon Hull" });
            
            this.Stadiums.Insert(new MongoStadium { Name = "Anfield", Town = "Liverpool", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Anoeta Stadium", Town = "San Sebastián", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Balaidos", Town = "San Sebastián", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Ciutat de Valencia", Town = "Valencia", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Camp Nou", Town = "Barcelona", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Emirates Stadium", Town = "London", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Estadi Cornella-El Prat", Town = "Villarreal", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Estadio El Madrigal", Town = "Villarreal", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Estadio Manuel Martinez Valero", Town = "Elche", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Estadio Nuevo Los Carmenes", Town = "Madrid", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Estadio Riazor", Town = "La Coruña", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Etihad Stadium", Town = "Witton", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Juventus Stadium", Town = "Torino", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "King Power Stadium", Town = "Leicester", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Liberty Stadium", Town = "Swansea", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Mestalla Stadium", Town = "Valencia", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Old Trafford", Town = "Manchester", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Ramon Sanchez Pizjuan Stadium", Town = "Sevilla", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "San Mames Stadium", Town = "Bilbao", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "San Siro", Town = "Milan", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Santiago Bernabeu", Town = "Madrid", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Stadio Ennio Tardini", Town = "Parma", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Stadio Friuli", Town = "Udine", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Stadio Renzo Barbera", Town = "Palermo", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Stamford Bridge", Town = "London", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "Villa Park", Town = "Birmingham", Capacity = 45000 });
            this.Stadiums.Insert(new MongoStadium { Name = "White Hart Lane", Town = "London", Capacity = 45000 });
            
            this.Teams.Insert(new MongoTeam() { Name = "Liverpool", Town = "Liverpool" });
            this.Teams.Insert(new MongoTeam() { Name = "Real Sociedad", Town = "San Sebastián" });
            this.Teams.Insert(new MongoTeam() { Name = "Barcelona", Town = "Barcelona" });
            this.Teams.Insert(new MongoTeam() { Name = "Espanyol", Town = "Barcelona" });
            this.Teams.Insert(new MongoTeam() { Name = "Valencia", Town = "Valencia" });
            this.Teams.Insert(new MongoTeam() { Name = "Granada", Town = "Valencia" });
            this.Teams.Insert(new MongoTeam() { Name = "Levante", Town = "Valencia" });
            this.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Islington" });
            this.Teams.Insert(new MongoTeam() { Name = "Villarreal", Town = "Villarreal" });
            this.Teams.Insert(new MongoTeam() { Name = "Elche", Town = "Elche" });
            this.Teams.Insert(new MongoTeam() { Name = "Dep.La Coruna", Town = "La Coruña" });
            this.Teams.Insert(new MongoTeam() { Name = "Aston Villa", Town = "Witton" });
            this.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Torino" });
            this.Teams.Insert(new MongoTeam() { Name = "Arsenal", Town = "Leicester" });
            this.Teams.Insert(new MongoTeam() { Name = "Leicester", Town = "Leicester" });
            this.Teams.Insert(new MongoTeam() { Name = "Swansea", Town = "Swansea" });
            this.Teams.Insert(new MongoTeam() { Name = "Manchester City", Town = "Manchester" });
            this.Teams.Insert(new MongoTeam() { Name = "Manchester United", Town = "Manchester" });
            this.Teams.Insert(new MongoTeam() { Name = "Sevilla", Town = "Sevilla" });
            this.Teams.Insert(new MongoTeam() { Name = "Ath Bilbao", Town = "Bilbao" });
            this.Teams.Insert(new MongoTeam() { Name = "Ac Milan", Town = "Milan" });
            this.Teams.Insert(new MongoTeam() { Name = "Inter", Town = "Milan" });
            this.Teams.Insert(new MongoTeam() { Name = "Real Madrid", Town = "Madrid" });
            this.Teams.Insert(new MongoTeam() { Name = "Parma", Town = "Parma" });
            this.Teams.Insert(new MongoTeam() { Name = "Udinese", Town = "Udine" });
            this.Teams.Insert(new MongoTeam() { Name = "Palermo", Town = "Palermo" });
            this.Teams.Insert(new MongoTeam() { Name = "Chelsea", Town = "London" });
            this.Teams.Insert(new MongoTeam() { Name = "Tottenham", Town = "London" });
            this.Teams.Insert(new MongoTeam() { Name = "Celta Vigo", Town = "Vigo" });
            this.Teams.Insert(new MongoTeam() { Name = "Levski", Town = "Birmingham" });
            this.Teams.Insert(new MongoTeam() { Name = "Juventus", Town = "Turin" });

            this.Teams.Insert(new MongoTeam() { Name = "QPR", Town = "London" });
            this.Teams.Insert(new MongoTeam() { Name = "Getafe", Town = "Getafe" });
            this.Teams.Insert(new MongoTeam() { Name = "Atl. Madrid", Town = "Madrid" });
            this.Teams.Insert(new MongoTeam() { Name = "Sassuolo", Town = "Sassuolo" });
            this.Teams.Insert(new MongoTeam() { Name = "West Brom", Town = "West Bromwich" });
            this.Teams.Insert(new MongoTeam() { Name = "Southampton", Town = "Southampton" });
            this.Teams.Insert(new MongoTeam() { Name = "Malaga", Town = "Malaga" });
            this.Teams.Insert(new MongoTeam() { Name = "Everton", Town = "Liverpool" });
            this.Teams.Insert(new MongoTeam() { Name = "Rayo Vallecano", Town = "Madrid" });
            this.Teams.Insert(new MongoTeam() { Name = "Almeria", Town = "Almeria" });
            this.Teams.Insert(new MongoTeam() { Name = "Napoli", Town = "Naples" });
            this.Teams.Insert(new MongoTeam() { Name = "Hull City", Town = "Kingston upon Hull" });
        }
    }
}
