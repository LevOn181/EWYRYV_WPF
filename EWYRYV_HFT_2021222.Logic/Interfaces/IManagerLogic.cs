using EWYRYV_HFT_202223.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Logic
{
    public interface IManagerLogic
    {
        void Create(Manager item);
        void Delete(int id);
        Manager Read(int id);
        IQueryable<Manager> ReadAll();
        void Update(Manager item);
        public IEnumerable<object> TopPlayerData();
    }
}

