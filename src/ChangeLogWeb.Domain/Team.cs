using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain
{
    public class Team : Entity
    {
        public string Code { get; set; }
        public string SecretKey { get; set; }
        public IList<Team> ChildrenTeams { get; set; }

        public Team(string code, IList<Team> childrenTeams = null)
        {
            Code = code;
            ChildrenTeams = childrenTeams;
        }
    }
}
