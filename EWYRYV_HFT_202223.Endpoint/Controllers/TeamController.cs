using EWYRYV_HFT_202223.Logic;
using EWYRYV_HFT_202223.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController] 
    public class TeamController : ControllerBase
    {
        ITeamLogic logic;

        public TeamController(ITeamLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Team Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Team value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
