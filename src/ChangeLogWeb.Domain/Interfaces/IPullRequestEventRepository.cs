using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain.Interfaces
{
    public interface IPullRequestEventRepository
    {
        void Insert(PullRequestEvent pullRequestEvent);
        IList<PullRequestEvent> GetAll();
        IList<PullRequestEvent> GetAll(string repositoryName, string labelName);
    }
}
