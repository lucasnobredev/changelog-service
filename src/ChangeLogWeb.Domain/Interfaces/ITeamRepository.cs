using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain.Interfaces
{
    public interface ITeamRepository
    {
        void Insert(Team team);
        IList<Team> GetAll();
        Team GetByKeys(string majorTeam, string secretKey, string childTeam = null);
    }
}
