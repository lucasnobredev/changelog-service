using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogService.Domain.Interfaces;
using ChangeLogService.Models.ViewModels;
using MarkedNet;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogService.Controllers
{
    [Route("changelog")]
    public class ChangeLogDeprecatedController : Controller
    {
        private readonly IPullRequestEventRepository _pullRequestEventRepository;

        public ChangeLogDeprecatedController(
            IPullRequestEventRepository pullRequestEventRepository)
        {
            _pullRequestEventRepository = pullRequestEventRepository;
        }

        public IActionResult Index(string repositoryName, string labelName)
        {
            var pullrequestEvents =_pullRequestEventRepository.GetAll(repositoryName, labelName);

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
                    //LabelsName = pullRequestEvent.LabelsName
                };

                itemViewModel.Labels = new List<LabelDTO>();
                foreach(var label in pullRequestEvent.Labels)
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

            return View(viewModel);
        }
    }
}