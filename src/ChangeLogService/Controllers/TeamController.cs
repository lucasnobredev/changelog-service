using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogService.Domain.Interfaces;
using ChangeLogService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(
            ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TeamRequest obj)
        {
            var team = new Domain.Team(obj.Name);

            if (obj.ChildrenTeam != null)
            {
                team.ChildrenTeams = new List<Domain.Team>();
                foreach(var child in obj.ChildrenTeam)
                    team.ChildrenTeams.Add(new Domain.Team(child.Name));
            }

            _teamRepository.Insert(team);
            return Ok(team);
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_teamRepository.GetAll());
        }
    }
}