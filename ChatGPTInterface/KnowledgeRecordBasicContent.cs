using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class KnowledgeRecordBasicContent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double SimilarityScore { get; set; }

        public string DisplayText
        {
            get {
                double percentageValue = SimilarityScore * 100;
                return $"({percentageValue:0.00}%) " + Title; 
            }
        }
    }
}
