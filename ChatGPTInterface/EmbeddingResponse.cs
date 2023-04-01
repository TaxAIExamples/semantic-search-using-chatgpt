using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class EmbeddingResponse
    {
        [JsonProperty("object")]
        public string ObjectType { get; set; }
        [JsonProperty("data")]
        public EmbeddingData[] Data { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("usage")]
        public EmbeddingUsage Usage { get; set; }
    }
}
