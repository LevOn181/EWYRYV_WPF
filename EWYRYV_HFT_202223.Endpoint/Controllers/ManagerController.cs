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
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;

        public ManagerController(IManagerLogic logic)
        {
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
        }

        [HttpPut]
        public void Update([FromBody] Manager value)
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
