using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogWeb.Models.ViewModels
{
    public class PullRequestEventViewModel
    {
        public object Id { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime MergedAt { get; set; }
        public bool Merged { get; set; }
        public string MergedBy { get; set; }
        public string RepositoryName { get; set; }
        public IList<LabelDTO> Labels { get; set; }
        public TeamResponse Team { get; set; }
    }
}
