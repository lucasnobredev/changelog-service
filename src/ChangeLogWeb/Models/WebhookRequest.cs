using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLogWeb.Models
{
    public class WebhookRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("pull_request")]
        public PullRequestDTO PullRequest { get; set; }
    }
}
