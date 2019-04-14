using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangeLogWeb.Domain.Interfaces;
using ChangeLogWeb.Models.ViewModels;
using MarkedNet;
using Microsoft.AspNetCore.Mvc;

namespace ChangeLogWeb.Controllers
{
    [Route("changelog")]
    public class ChangeLogController : Controller
    {
        private readonly IPullRequestEventRepository _pullRequestEventRepository;

        public ChangeLogController(
            IPullRequestEventRepository pullRequestEventRepository)
        {
            _pullRequestEventRepository = pullRequestEventRepository;
        }

        public IActionResult Index()
        {
            var pullrequestEvents =_pullRequestEventRepository.GetAll();

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
                    Id = pullRequestEvent.Id,
                    LabelsName = pullRequestEvent.LabelsName
                };

                viewModel.Add(itemViewModel);
            }

            return View(viewModel);
        }
    }
}