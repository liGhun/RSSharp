using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.GeneralGoogleReaderAPI.Model
{
    public class Feed
    {
        public string id { get; set; }
        public Link self { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public long updated { get; set; }
        public List<Article> items { get; set; }
        public string direction { get; set; }
        public string continuation { get; set; }
    }

}
