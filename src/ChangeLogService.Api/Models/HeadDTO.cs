using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogService.Models
{
    public class HeadDTO
    {
        [JsonProperty("repo")]
        public RepoDTO Repo { get; set; }
    }
}
