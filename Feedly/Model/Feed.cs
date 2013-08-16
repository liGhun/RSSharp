using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly.Model
{
    public class Feed
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<string> keywords { get; set; }
        public string website { get; set; }
        public decimal velocity { get; set; }
        public bool featured { get; set; }
        public bool sponsored { get; set; }
        public bool curated { get; set; }
        public int subscribers { get; set; }
        public string state { get; set; }

        public override string ToString()
        {
            if (title != null)
            {
                return "Feed: " + title;
            }
            else
            {
                return base.ToString();
            }
        }
    }

    
}
