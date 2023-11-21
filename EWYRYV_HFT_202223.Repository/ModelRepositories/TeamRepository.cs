using EWYRYV_HFT_202223.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Repository
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    { 
        public TeamRepository(TeamDbContext ctx) : base(ctx)
        {

        }
        public override Team Read(int id)
        {
            return ctx.Teams.FirstOrDefault(t => t.TeamId == id);
        }

        public override void Update(Team item)
        {
            var updated = Read(item.TeamId);
            updated.Name = item.Name;
            ctx.SaveChanges();
        }
    }
}
