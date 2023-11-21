using EWYRYV_HFT_202223.Logic;
using EWYRYV_HFT_202223.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController] 
    public class StatController : ControllerBase
    {
        ITeamLogic teamLogic;
        IManagerLogic managerLogic;
        IPlayerLogic playerLogic;
        public StatController(ITeamLogic tlogic, IPlayerLogic plogic, IManagerLogic mlogic)
        {
            this.teamLogic = tlogic;
            this.managerLogic = mlogic;
            this.playerLogic = plogic;
        }

        [HttpGet]
        public IEnumerable<object?> countPlayers()
        {
           return this.playerLogic.CountPlayers();
        }

        [HttpGet]
        public IEnumerable<object> teamValue()
        {
            return this.playerLogic.TeamValue();
        }

        [HttpGet]
        public IEnumerable<object> mostValuable()
        {
            return this.playerLogic.MostValuable();
        }

        [HttpGet]
        public IEnumerable<object> hungarianManagers()
        {
            return this.teamLogic.HungarianManagers();
        }

        [HttpGet]
        public IEnumerable<object>topPlayerData()
        {
            return this.managerLogic.TopPlayerData();
        }
    }
}