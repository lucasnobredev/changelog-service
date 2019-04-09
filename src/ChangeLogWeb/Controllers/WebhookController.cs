using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VWebhookController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] WebhookRequest obj)
        {
            return Ok(obj);
        }
    }
}
