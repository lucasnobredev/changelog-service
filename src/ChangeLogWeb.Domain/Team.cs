﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Domain
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public IList<Team> ChildrenTeams { get; set; }

        public Team(string name, IList<Team> childrenTeams = null)
        {
            Name = name;
            ChildrenTeams = childrenTeams;
        }
    }
}
