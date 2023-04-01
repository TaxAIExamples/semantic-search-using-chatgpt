using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class CompletionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("object")]
        public string ObjectType { get; set; }
        [JsonProperty("created")]
        public int Created { get; set; }
        [JsonProperty("choices")]
        public CompletionChoices[] Choices { get; set; }
        [JsonProperty("usage")]
        public CompletionUsage Usage { get; set; }
    }
}
