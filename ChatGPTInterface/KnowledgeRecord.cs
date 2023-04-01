using ChatGPTInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticSearch
{
    public class KnowledgeRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content{ get; set; }
        public int Tokens { get; set; }
        public List<KnowledgeVectorItem> KnowledgeVector { get; set; }
    }
}
