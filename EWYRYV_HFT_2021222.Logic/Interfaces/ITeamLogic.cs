using EWYRYV_HFT_202223.Models;
using System.Collections.Generic;
using System.Linq;

namespace EWYRYV_HFT_202223.Logic
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        Team Read(int id);
        IQueryable<Team> ReadAll();
        void Update(Team item);
        public IEnumerable<object> HungarianManagers();
    }
}