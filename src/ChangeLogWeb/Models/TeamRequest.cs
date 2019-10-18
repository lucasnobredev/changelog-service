using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogWeb.Models
{
    public class TeamRequest
    {
        public string Name { get; set; }
        public IList<TeamRequest> ChildrenTeam { get; set; }
    }
}
