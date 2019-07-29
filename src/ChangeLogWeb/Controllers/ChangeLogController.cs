using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogWeb.Domain.Interfaces;
using ChangeLogWeb.Models.ViewModels;
using MarkedNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeLogController : ControllerBase
    {
        private readonly IPullRequestEventRepository _pullRequestEventRepository;

        public ChangeLogController(
            IPullRequestEventRepository pullRequestEventRepository)
        {
            _pullRequestEventRepository = pullRequestEventRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pullrequestEvents = _pullRequestEventRepository.GetAll(null, null);

            var viewModel = new List<PullRequestEventViewModel>();

            var marked = new Marked();
            foreach (var pullRequestEvent in pullrequestEvents)
            {
                var itemViewModel = new PullRequestEventViewModel()
                {
                    Action = pullRequestEvent.Action,
                    Body = marked.Parse(pullRequestEvent.Body),
                    CreatedAt = pullRequestEvent.CreatedAt,
                    Merged = pullRequestEvent.Merged,
                    MergedAt = pullRequestEvent.MergedAt,
                    MergedBy = pullRequestEvent.MergedBy,
                    RepositoryName = pullRequestEvent.RepositoryName,
                    Title = pullRequestEvent.Title,
                    Id = pullRequestEvent.Id
                };

                itemViewModel.Team = new Models.TeamResponse()
                {
                    Id = pullRequestEvent.Team.Id,
                    Name = pullRequestEvent.Team.Name,
                };

                if(pullRequestEvent.Team.ChildrenTeams != null)
                {
                    itemViewModel.Team.ChildrenTeam = new List<Models.TeamResponse>();
                    foreach (var childTeam in pullRequestEvent.Team.ChildrenTeams)
                    {
                        var childTeamResponse = new Models.TeamResponse()
                        {
                            Id = childTeam.Id,
                            Name = childTeam.Name
                        };
                        itemViewModel.Team.ChildrenTeam.Add(childTeamResponse);
                    }
                }

                itemViewModel.Labels = new List<LabelDTO>();
                foreach (var label in pullRequestEvent.Labels)
                {
                    var labelDTO = new LabelDTO()
                    {
                        Color = label.Color,
                        Name = label.Name
                    };

                    itemViewModel.Labels.Add(labelDTO);
                }

                viewModel.Add(itemViewModel);
            }

            return Ok(viewModel);
        }
    }
}