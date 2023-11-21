using System;
using System.Collections.Generic;
using System.Linq;
using EWYRYV_HFT_202223.Models;
using EWYRYV_HFT_202223.Repository;

namespace EWYRYV_HFT_202223.Logic
{
    public class TeamLogic : ITeamLogic
    {

        IRepository<Team> teamRepo;

        public TeamLogic(IRepository<Team> repo)
        {
            this.teamRepo = repo;
        }

        public void Create(Team item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Team's name cannot be null!");
            }
            if(item.FoundationYear < 1800 || item.FoundationYear > 2022)
            {
                throw new ArgumentOutOfRangeException("The year of foundation must be between 1800 and 2022!");
            }
            this.teamRepo.Create(item);
        }

        public void Delete(int id)
        {
            var team = this.teamRepo.Read(id);
            if (team == null)
            {
                throw new ArgumentException("Team doesn't exist!");
            }
            this.teamRepo.Delete(id);
        }

        public Team Read(int id)
        {
            var team = this.teamRepo.Read(id);
            if (team == null)
            {
                throw new ArgumentException("Team doesn't exist!");
            }
            return team;
        }

        public IQueryable<Team> ReadAll()
        {
            return this.teamRepo.ReadAll();
        }

        public void Update(Team item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Team's name cannot be null!");
            }
            if (item.FoundationYear < 1800 || item.FoundationYear > 2022)
            {
                throw new ArgumentOutOfRangeException("The year of foundation must be between 1800 and 2022!");
            }
            this.teamRepo.Update(item);
        }
        public IEnumerable<object> HungarianManagers()
        {
            var data = from x in teamRepo.ReadAll()
                       where x.Manager.Nationality.ToLower().Equals("hungarian") || x.Manager.Nationality.ToLower().Equals("hungary") || x.Manager.Nationality.ToLower().Equals("hu")
                       select new { teamName = x.Name, ManagerName = x.Manager.Name };
            return data;
        }

    }
}
