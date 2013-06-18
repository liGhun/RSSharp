using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly.Model
{
    public class Subscription
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<Category> categories { get; set; }
        public string sortid { get; set; }
        public long updated { get; set; }
        public string website { get; set; }
    }
}
