using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWYRYV_HFT_202223.Models;

namespace EWYRYV_HFT_202223.Logic
{
    public interface IPlayerLogic
    {
        // CRUD
        void Create(Player item);
        void Delete(int id);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);

        // OTHER
        public IEnumerable<object> CountPlayers();
        public IEnumerable<object> TeamValue();
        public IEnumerable<object> MostValuable();

    }
}
