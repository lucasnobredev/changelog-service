using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogWeb.Domain;
using ChangeLogWeb.Domain.Interfaces;
using ChangeLogWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VWebhookController : ControllerBase
    {
        private readonly IPullRequestEventRepository _pullRequestEventRepository;

        public VWebhookController(
            IPullRequestEventRepository pullRequestEventRepository)
        {
            _pullRequestEventRepository = pullRequestEventRepository;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] WebhookRequest obj)
        {
            var pullRequestEvent = new PullRequestEvent()
            {
                Action = obj.Action,
                Body = obj.PullRequest.Body,
                CreatedAt = obj.PullRequest.CreatedAt,
                Merged = obj.PullRequest.Merged,
                MergedAt = obj.PullRequest.MergedAt,
                MergedBy = obj.PullRequest.MergedBy.Login,
                RepositoryName = obj.PullRequest.Head.Repo.Name,
                Title = obj.PullRequest.Title
            };

            _pullRequestEventRepository.Insert(pullRequestEvent);

            return Ok(obj);
        }
    }
}
