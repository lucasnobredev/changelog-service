using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain
{
    public class Team
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public IList<Team> ChildrenTeams { get; set; }

        public Team(string name, IList<Team> childrenTeams = null)
        {
            Name = name;
            ChildrenTeams = childrenTeams;
        }
    }
}
