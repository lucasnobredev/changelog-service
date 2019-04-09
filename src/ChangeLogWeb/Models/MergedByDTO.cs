using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogWeb.Models
{
    public class MergedByDTO
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
