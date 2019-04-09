using System;

namespace ChangeLogWeb.Domain
{
    public class PullRequestEvent
    {
        public string Action { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime MergedAt { get; set; }
        public bool Merged { get; set; }
        public string MergedBy { get; set; }
        public string RepositoryName { get; set; }
    }
}
