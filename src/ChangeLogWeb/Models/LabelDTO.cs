using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogWeb.Models
{
    public class LabelDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
