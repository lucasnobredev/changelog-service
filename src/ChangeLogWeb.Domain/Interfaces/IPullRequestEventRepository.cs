using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain.Interfaces
{
    public interface IPullRequestEventRepository
    {
        void Insert(PullRequestEvent pullRequestEvent);
    }
}
