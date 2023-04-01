using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class EmbeddingData
    {
        [JsonProperty("object")]
        public string EmbeddingObject { get; set; }
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("embedding")]
        public double[] Embedding { get; set; }

    }
}
