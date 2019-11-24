using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogService.Models
{
    public class TeamResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<TeamResponse> ChildrenTeam { get; set; }
    }
}
