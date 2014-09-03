namespace ChampionsLeague.Tests
{
    using System;
    using System.Linq;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using ChampionsLeague.Data;
    using ChampionsLeague.Model;

    [TestClass]
    public class MSSQLDatabase
    {
        private ChampionsLeagueListData testDb;
        
        [TestInitialize]
        public void Initialize()
        {
            this.testDb = new ChampionsLeagueListData();
        }

        [TestMethod]
        public void ShouldCreateEmptyListsOnInitialize()
        {
            Assert.AreEqual(this.testDb.Towns.Count, 0);
            Assert.AreEqual(this.testDb.Teams.Count, 0);
            Assert.AreEqual(this.testDb.Stadiums.Count, 0);
            Assert.AreEqual(this.testDb.Players.Count, 0);
            Assert.AreEqual(this.testDb.Matches.Count, 0);
        }

        [TestMethod]
        public void ShouldAddSingleObject()
        {
            var townToAdd = new Town() { TownName = "TestTownCreating" };
            var stadiumToAdd = new Stadium(){Name = "TestStadiumName", Town = townToAdd};
            var teamToAdd = new Team(){TeamName = "TestTeamName", Town = townToAdd};
            var playerToAdd = new Player(){FirstName="TestFirstName", LastName = "TestLastName", Team = teamToAdd, Salary = 2000};

            this.testDb.Towns.Add(townToAdd);
            this.testDb.Stadiums.Add(stadiumToAdd);
            this.testDb.Teams.Add(teamToAdd);
            this.testDb.Players.Add(playerToAdd);

            Assert.AreEqual(this.testDb.Towns.Count, 1);
            Assert.AreEqual(this.testDb.Teams.Count, 1);
            Assert.AreEqual(this.testDb.Stadiums.Count, 1);
            Assert.AreEqual(this.testDb.Players.Count, 1);
        }

        [TestMethod]
        public void ShouldAddSingleMatchBetwenGivenTeams()
        {
            var townToAdd = new Town() { TownName = "TestTownCreating" };
            var stadiumToAdd = new Stadium() { Name = "TestStadiumName", Town = townToAdd };
            var hostTeamToAdd = new Team() { TeamName = "HOSTTeamName", Town = townToAdd };
            var hostPlayerToAdd = new Player() { FirstName = "HOSTFirstName", LastName = "HOSTLastName", Team = hostTeamToAdd, Salary = 2000 };

            this.testDb.Towns.Add(townToAdd);
            this.testDb.Stadiums.Add(stadiumToAdd);
            this.testDb.Teams.Add(hostTeamToAdd);
            this.testDb.Players.Add(hostPlayerToAdd);

            var guestTeamToAdd = new Team() { TeamName = "GUESTTeamName", Town = townToAdd };
            var guestPlayerToAdd = new Player() { FirstName = "GUESTFirstName", LastName = "GUESTLastName", Team = guestTeamToAdd, Salary = 2000 };

            this.testDb.Teams.Add(guestTeamToAdd);
            this.testDb.Players.Add(guestPlayerToAdd);

            this.testDb.Matches.Add(new Match() { HostTeam = hostTeamToAdd, GuestTeam = guestTeamToAdd, Stadium = stadiumToAdd, Date = DateTime.Now, Attendance = 12000 });

            Assert.AreEqual(this.testDb.Matches.Count, 1);

            var firstMatch = this.testDb.Matches.FirstOrDefault();
            Assert.AreEqual(firstMatch.HostTeam.TeamName, hostTeamToAdd.TeamName);
            Assert.AreEqual(firstMatch.GuestTeam.TeamName, guestTeamToAdd.TeamName);

        }
    }
}
