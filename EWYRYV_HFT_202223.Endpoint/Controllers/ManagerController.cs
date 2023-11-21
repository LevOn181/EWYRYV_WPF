using EWYRYV_HFT_202223.Endpoint.Services;
using EWYRYV_HFT_202223.Logic;
using EWYRYV_HFT_202223.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController] 
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;
        IHubContext<SignalRHub> hub;

        public ManagerController(IManagerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.hub = hub;
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Manager> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Manager Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Manager value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("ManagerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Manager value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ManagerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var managerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ManagerDeleted", managerToDelete);
        }
    }
}
