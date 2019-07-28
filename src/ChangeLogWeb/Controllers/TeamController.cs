using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogWeb.Domain.Interfaces;
using ChangeLogWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogWeb.Controllers
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
            _teamRepository.Insert(new Domain.Team(obj.Name));
            return Ok();
        }
    }
}