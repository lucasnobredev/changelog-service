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
            if (string.IsNullOrEmpty(majorTeam))
                return Unauthorized();

            childTeam = GetChildTeam(childTeam, obj.PullRequest.Labels);
            var team = _teamRepository.GetByKeys(majorTeam, childTeam);

            if (team == null)
                return Unauthorized();

            var pullRequestEvent = CreatePullRequestEvent(childTeam, obj, team);

            _pullRequestEventRepository.Insert(pullRequestEvent);

            return Ok(pullRequestEvent);
        }

        private PullRequestEvent CreatePullRequestEvent(string childTeam, WebhookRequest obj, Team team)
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
                Title = obj.PullRequest.Title,
                MajorTeam = team.Code,
                ChildTeam = team.ChildrenTeams?.FirstOrDefault(x => x.Code == childTeam).Code
            };

            pullRequestEvent.Labels = new List<Label>();
            if (obj.PullRequest.Labels != null)
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

            return pullRequestEvent;
        }

        private string GetChildTeam(string childTeamParameter, IList<LabelDTO> labels)
        {
            if (childTeamParameter != null)
                return childTeamParameter;

            var childTeamByLabel = labels.FirstOrDefault(
                    x => string.IsNullOrEmpty(x.Name) == false &&
                    x.Name.ToLower().Contains("team:")).Name;

            return childTeamByLabel.Split(':')[1];

            //if (labelRequest.Name.ToLower().Contains("team:"))
            //{
            //    var childTeamName = labelRequest.Name.Split(':')[1];
            //    if (team.ChildrenTeams.Any(x => x.Code == childTeamName.ToLower()))
            //    {
            //        pullRequestEvent.ChildTeam = team.ChildrenTeams?.FirstOrDefault(x => x.Code == childTeam).Code;
            //    }
            //}
        }
    }
}
