using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class Similarity
    {
        public int KnowledgeRecordId { get; set; }
        public double SimilarityScore { get; set; }
        public int Tokens { get; set; }
        public string Text { get; set; }
    }
}
