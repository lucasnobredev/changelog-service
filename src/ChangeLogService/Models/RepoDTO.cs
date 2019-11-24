using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogService.Models
{
    public class RepoDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
