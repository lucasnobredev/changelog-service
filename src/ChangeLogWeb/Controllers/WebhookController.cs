using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private readonly ITeamRepository _teamRepository;

        public WebhookController(
            IPullRequestEventRepository pullRequestEventRepository,
            ITeamRepository teamRepository)
        {
            _pullRequestEventRepository = pullRequestEventRepository;
            _teamRepository = teamRepository;
        }

        // POST api/values
        [HttpPost]
        [Route("/api/webhook/{majorTeam}")]
        [Route("/api/webhook/{majorTeam}/child/{childTeam}")]
        public IActionResult Post(string majorTeam, string childTeam, [FromBody] WebhookRequest obj)
        {
            if (string.IsNullOrEmpty(majorTeam) || string.IsNullOrEmpty(childTeam))
                return Unauthorized();

            var team = _teamRepository.GetByKeys(majorTeam, Request.Headers["X-Hub-Signature"], childTeam);

            if (team == null)
                return Unauthorized();
            
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

            //bool isChild = team.Id == teamId;
            //
            //pullRequestEvent.Team = team;
            //if (!isChild)
            //{
            //    pullRequestEvent.Team.ChildrenTeams = new List<Team>()
            //    {
            //        team.ChildrenTeams.FirstOrDefault(x => x.Id == teamId)
            //    };
            //}

            pullRequestEvent.Labels = new List<Label>();
            if(obj.PullRequest.Labels != null)
            {
                foreach (var labelRequest in obj.PullRequest.Labels)
                {
                    if(labelRequest.Name.ToLower().Contains("team:"))
                    {
                        var childTeamName = labelRequest.Name.Split(':')[1];
                        var childTeam2 = team.ChildrenTeams.FirstOrDefault(x => x.Code.ToLower() == childTeamName.ToLower());
                        if (childTeam != null)
                        {
                            pullRequestEvent.Team.ChildrenTeams = new List<Team>()
                            { 
                                childTeam2
                            };
                        }
                    }
                    else
                    {
                        var label = new Label()
                        {
                            Color = labelRequest.Color,
                            Name = labelRequest.Name
                        };

                        pullRequestEvent.Labels.Add(label);
                    }
                }
            }
            
            _pullRequestEventRepository.Insert(pullRequestEvent);

            return Ok(pullRequestEvent);
        }
    }
}
