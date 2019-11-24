using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogService.Models
{
    public class TeamRequest
    {
        public string Name { get; set; }
        public IList<TeamRequest> ChildrenTeam { get; set; }
    }
}
