using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWYRYV_HFT_202223.Models;

namespace EWYRYV_HFT_202223.Repository
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    { 
        public PlayerRepository(TeamDbContext ctx) : base(ctx)
        {

        }
        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.PlayerId == id);
        }

        public override void Update(Player item)
        {
            var updated = Read(item.PlayerId);
            updated.Name = item.Name;
            updated.TeamId = item.TeamId;
            updated.KitNumber = item.KitNumber;
            updated.BirthDate = item.BirthDate;
            updated.Value = item.Value;
            ctx.SaveChanges();
        }
    }
}
