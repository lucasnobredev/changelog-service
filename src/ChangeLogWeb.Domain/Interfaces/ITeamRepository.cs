using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain.Interfaces
{
    public interface ITeamRepository
    {
        void Insert(Team team);
    }
}
