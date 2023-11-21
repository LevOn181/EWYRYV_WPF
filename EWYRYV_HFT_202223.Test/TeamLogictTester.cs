using EWYRYV_HFT_202223.Logic;
using EWYRYV_HFT_202223.Models;
using EWYRYV_HFT_202223.Repository;
using static EWYRYV_HFT_202223.Logic.PlayerLogic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EWYRYV_HFT_202223.Test
{
    [TestFixture]
    public class TeamLogictTester
    {
        TeamLogic teamLogic;
        ManagerLogic managerLogic;
        PlayerLogic playerLogic;

        Mock<IRepository<Team>> mockTeamRepo;
        Mock<IRepository<Manager>> mockManagerRepo;
        Mock<IRepository<Player>> mockPlayerRepo;

        [SetUp]
        public void Init()
        {
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockManagerRepo = new Mock<IRepository<Manager>>();
            mockPlayerRepo = new Mock<IRepository<Player>>();

            var players1 = new List<Player>()
            {
                new Player("1#11#1#Test Player0#1991.01.11#100"),
                new Player("2#16#1#Test Player1#1987.12.24#200"),
                new Player("3#23#1#Test Player2#1996.06.12#300"),
                new Player("4#2#1#Test Player3#1996.11.01#400")
            };
            var players2 = new List<Player>()
            {
                new Player("5#1#2#Test Player4#2001.09.11#500"),
                new Player("6#4#2#Test Player5#1981.08.09#400"),
                new Player("7#5#2#Test Player6#1997.07.21#300"),
                new Player("8#10#2#Test Player7#1985.12.01#200"),
            };
            var players3 = new List<Player>()
            {
                new Player("9#69#3#Test Player8#2004.03.21#300"),
                new Player("10#99#3#Test Player9#1999.04.14#600"),
                new Player("11#13#3#Test Player10#1979.02.11#100"),
                new Player("12#22#3#Test Player11#1986.01.01#50"),
            };


            Manager manager1 = new Manager
            {
                Name = "Test Manager0",
                Nationality = "USA",
                TeamId = 1,
            };
            Manager manager2 = new Manager
            {
                Name = "Test Manager1",
                Nationality = "HU",
                TeamId = 2,
            };
            Manager manager3 = new Manager
            {
                Name = "Test Manager2",
                Nationality = "UK",
                TeamId = 2,
            };
            var managers = new List<Manager>()
            {
                manager1, manager2, manager3
            }.AsQueryable();
            

            Team team1 = new Team
            {
                TeamId = 1,
                Name = "Test0 FC",
                FoundationYear = 2000,
                Manager = manager1,
                Players = players1,
            };
            Team team2 = new Team
            {
                TeamId = 2,
                Name = "Test1 SC",
                FoundationYear = 1950,
                Manager = manager2,
                Players = players2
            };
            Team team3 = new Team
            {
                TeamId = 3,
                Name = "Test2 AC",
                FoundationYear = 1880,
                Manager = manager3,
                Players = players3
            };
            var teams = new List<Team>()
            {
                team1, team2, team3,
            }.AsQueryable();
            

            manager1.Team = team1;
            manager2.Team = team2;
            manager3.Team = team3;

            foreach (var item in players1)
            {
                item.Team = team1;
            }
            foreach (var item in players2)
            {
                item.Team = team2;
            }
            foreach (var item in players3)
            {
                item.Team = team3;
            }

            var ps = new List<Player>();
            foreach (var item in players1)
            {
                ps.Add(item);
            }
            foreach (var item in players2)
            {
                ps.Add(item);
            }
            foreach (var item in players3)
            {
                ps.Add(item);
            }

            var players = ps.AsQueryable();
            mockPlayerRepo.Setup(p => p.ReadAll()).Returns(players);
            mockManagerRepo.Setup(m => m.ReadAll()).Returns(managers);
            mockTeamRepo.Setup(t => t.ReadAll()).Returns(teams);

            teamLogic = new TeamLogic(mockTeamRepo.Object);
            playerLogic = new PlayerLogic(mockPlayerRepo.Object);
            managerLogic = new ManagerLogic(mockManagerRepo.Object);
        }

        #region CRUD

        //CRUD tests
        // ----> Create Test(s)
        [Test]
        public void ManagerTestCreateSuccess()
        {
            var manager = new Manager() { Name = "Test Manager3", Nationality = "CZK", TeamId = 4 };
            managerLogic.Create(manager);
            mockManagerRepo.Verify(m => m.Create(manager), Times.Once);
        }
        [Test]
        public void ManagerTestCreateThrowsException()
        {
            // Arrange
            var manager = new Manager() { Nationality = "RO", TeamId = 4 };

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => managerLogic.Create(manager));
            //mockManagerRepo.Verify(m => m.Create(manager), Times.Never);
        }
        [Test]
        public void ManagerTestCreateTimesNever()
        {
            // Arrange
            var manager = new Manager() { Nationality = "GB", TeamId = 5 };
            try
            {
                // Act
                managerLogic.Create(manager);
            }
            catch { }

            // Assert
            mockManagerRepo.Verify(m => m.Create(manager), Times.Never);
        }

        // ----> Delete Test(s)
        [Test]
        public void TeamTestDeleteSuccessful()
        {
            Team expected = new Team()
            {
                TeamId = 4,
                Name = "Test3 FC",
                FoundationYear = 2022,
            };
            mockTeamRepo.Setup(t => t.Read(4)).Returns(expected);
            teamLogic.Delete(4);
            mockTeamRepo.Verify(t => t.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void TeamTestDeleteReturnsException()
        {
            try
            {
                teamLogic.Delete(5);
            }
            catch
            {

            }
            mockTeamRepo.Verify(m => m.Delete(5), Times.Never);
        }

        // ----> Read Test(s)
        [Test]
        public void PlayerTestReadReturnsExpectedObject()
        {
            Player expected = new Player() //1#11#1#Test Player0#1991.01.11#100
            {
                PlayerId = 1,
                KitNumber = 11,
                TeamId = 1,
                Name = "Test Player0",
                BirthDate = "1991.01.11",
                Value = 100
            };

            mockPlayerRepo.Setup(p => p.Read(0)).Returns(expected);

            var actual = playerLogic.Read(0);
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion

        #region Non-CRUD

        // Non-CRUD
        // ----> teamLogic test(s)
        [Test]
        public void HungarianManagersTest()
        {
            var result = teamLogic.HungarianManagers().ToList();
            var expected = new List<object>()
            {
                new { TeamName = "Test1 SC", ManagerName = "Test Manager1"}
            };

            Assert.That(result.ToString(), Is.EqualTo(expected.ToString()));
        }

        // ----> managerLogic test(s)
        [Test]
        public void TopPlayerDataTest()
        {
            var result = managerLogic.TopPlayerData().ToList();
            var expected = new List<object>()
            {
                new { ManagerName = "Test Manager0", TeamName = "Test0 FC", PlayerName = "Test Player3" },
                new { ManagerName = "Test Manager1", TeamName = "Test1 SC", PlayerName = "Test Player4" },
                new { ManagerName = "Test Manager2", TeamName = "Test2 AC", PlayerName = "Test Player9" },
            };
            Assert.That(result.ToString, Is.EqualTo(expected.ToString()));
        }

        // ----> playerLogic test(s)
        [Test]
        public void TeamValueTest()
        {
            var result = playerLogic.TeamValue().ToList();
            var expected = new List<object>()
            {
                new { TeamName = "Test1 SC", TeamValue = 1400 },
                new { TeamName = "Test2 AC", TeamValue = 1050 },
                new { TeamName = "Test0 FC", TeamValue = 1000 },
            };
            Assert.That(result.ToString(), Is.EqualTo(expected.ToString()));
        }
        [Test]
        public void MostValueableTest()
        {
            var result = playerLogic.MostValuable().ToList();
            var expected = new List<object>()
            {
                new { PlayerName = "Test Player9", ManagerName = "Test Manager2" },
            };
            Assert.That(result.ToString(), Is.EqualTo(expected.ToString()));
        }
        [Test]
        public void CountPlayersTest()
        {
            var result = playerLogic.CountPlayers().ToList();
            var expected = new List<object>()
            {
                new { TeamName = "Test0 FC", PlayerCount = 4 },
                new{ TeamName = "Test1 SC", PlayerCount = 4 },
                new { TeamName = "Test2 AC", PlayerCount = 4 },
            };

            Assert.That(result.ToString(), Is.EqualTo(expected.ToString()));
        }

        #endregion
    }
}
