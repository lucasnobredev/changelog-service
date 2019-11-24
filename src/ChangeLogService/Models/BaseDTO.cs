using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogService.Models
{
    public class BaseDTO
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}
