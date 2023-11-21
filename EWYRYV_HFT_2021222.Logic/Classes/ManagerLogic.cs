using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWYRYV_HFT_202223.Repository;
using EWYRYV_HFT_202223.Models;

namespace EWYRYV_HFT_202223.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        IRepository<Manager> managerRepo;
        public ManagerLogic(IRepository<Manager> repo)
        {
            this.managerRepo = repo;
        }
        public void Create(Manager item)
        {
            if(item.Name == null)
            {
                throw new ArgumentNullException("Manager's Name cannot be null!");
            }
            else if(item.TeamId<1)
            {
                throw new ArgumentOutOfRangeException("Team ID must be equal or higher to 1!");
            }
            this.managerRepo.Create(item);
        }

        public void Delete(int id)
        {
            var manager = this.managerRepo.Read(id);
            if(manager == null)
            {
                throw new ArgumentException("Manager doesn't exist!");
            }
            this.managerRepo.Delete(id);
        }

        public Manager Read(int id)
        {
            var manager = this.managerRepo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Manager not exists!");
            }
            return manager;
        }

        public IQueryable<Manager> ReadAll()
        {
            return this.managerRepo.ReadAll();
        }

        public void Update(Manager item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Manger doesn't exist!");
            }
            else if(item.Name == null)
            {
                throw new NullReferenceException("Manager's name cannot be null!");
            }
            this.managerRepo.Update(item);
        }

        public IEnumerable<object> TopPlayerData()
        {
            var data = from x in managerRepo.ReadAll()
                       select new
                       { 
                           ManagerName = x.Name,
                           TeamName = x.Team.Name,
                           PlayerName = x.Team.Players.OrderByDescending(t => t.Value).First().Name,
                       };
            return data;
        }
    }
}
