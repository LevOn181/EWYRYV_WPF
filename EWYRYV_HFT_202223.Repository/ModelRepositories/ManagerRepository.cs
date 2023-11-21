using EWYRYV_HFT_202223.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Repository
{
    public class ManagerRepository : Repository<Manager>, IRepository<Manager>
    { 
        public ManagerRepository(TeamDbContext ctx) : base(ctx)
        {

        }
        public override Manager Read(int id)
        {
            return ctx.Managers.FirstOrDefault(t => t.ManagerId == id);
        }

        public override void Update(Manager item)
        {
            var updated = Read(item.ManagerId);
            updated.Name = item.Name;
            updated.Nationality = item.Nationality;
            updated.TeamId = item.TeamId;
            ctx.SaveChanges();
        }
    }
}
