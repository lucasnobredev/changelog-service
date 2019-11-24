using System;
using System.Collections.Generic;

namespace ChangeLogService.Domain
{
    public class PullRequestEvent : Entity
    {
        public string Action { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime MergedAt { get; set; }
        public bool Merged { get; set; }
        public string MergedBy { get; set; }
        public string RepositoryName { get; set; }
        public IList<Label> Labels { get; set; }
        public string MajorTeam { get; set; }
        public string ChildTeam { get; set; }
    }
}
