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
    public class WebhookController : ControllerBase
    {
        private readonly IPullRequestEventRepository _pullRequestEventRepository;

        public WebhookController(
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

            pullRequestEvent.Labels = new List<Label>();
            if(obj.PullRequest.Labels != null)
            {
                foreach (var labelRequest in obj.PullRequest.Labels)
                {
                    var label = new Label()
                    {
                        Color = labelRequest.Color,
                        Name = labelRequest.Name
                    };

                    pullRequestEvent.Labels.Add(label);
                }
            }
            else
            {
                var label = new Label()
                {
                    Color = "CCCCCC",
                    Name = "none"
                };

                pullRequestEvent.Labels.Add(label);
            }
            
            _pullRequestEventRepository.Insert(pullRequestEvent);

            return Ok(obj);
        }
    }
}
