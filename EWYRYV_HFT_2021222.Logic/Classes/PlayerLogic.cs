using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWYRYV_HFT_202223.Repository;
using EWYRYV_HFT_202223.Models;

namespace EWYRYV_HFT_202223.Logic
{
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> playerRepo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.playerRepo = repo;
        }
        public void Create(Player item)
        {
            if(item.Name == null)
            {
                throw new NullReferenceException("Player's Name cannot be null!");
            }
            else if(item.KitNumber<1 && item.KitNumber > 99)
            {
                throw new ArgumentOutOfRangeException("Kit number must be between 1 and 99!");
            }
            else if (item.TeamId < 1)
            {
                throw new ArgumentOutOfRangeException("Team ID must be equal or higher to 1!");
            }
            this.playerRepo.Create(item);
        }
        public void Delete(int id)
        {
            var manager = this.playerRepo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Player doesn't exist!");
            }
            this.playerRepo.Delete(id);
        }
        public Player Read(int id)
        {
            var manager = this.playerRepo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Player doesn't exist!");
            }
            return manager;
        }

        public IQueryable<Player> ReadAll()
        {
            return this.playerRepo.ReadAll();
        }

        public void Update(Player item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Player doesn't exist!");
            }
            else if (item.Name == null)
            {
                throw new NullReferenceException("Player's name cannot be null!");
            }
            this.playerRepo.Update(item);
        }

        public IEnumerable<object> TeamValue()
        {
            var data = from x in playerRepo.ReadAll()
                       group x by x.Team.Name into g
                       orderby g.Sum(t => t.Value) descending
                       select new
                       {
                           TeamName = g.Key,
                           TeamValue = g.Sum(t => t.Value),
                       };

            return data;
        }

        public IEnumerable<object> MostValuable()
        {

            var data = from x in playerRepo.ReadAll()
                       where x.Value.Equals(playerRepo.ReadAll().Max(t => t.Value))
                       select new
                       {
                           PlayerName = x.Name,
                           ManagerName = x.Team.Manager.Name
                       };
            return data;
        }

        public IEnumerable<object> CountPlayers()
        {
            var data = from x in playerRepo.ReadAll()
                       group x by x.Team.Name into g
                       orderby g.Count() descending
                       select new
                       {
                           TeamName = g.Key,
                           PlayerCount = g.Count(),
                       };

            return data;
        }
    }
}

