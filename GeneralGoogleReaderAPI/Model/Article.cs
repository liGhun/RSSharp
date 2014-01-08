using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.GeneralGoogleReaderAPI.Model
{
    public class Article
    {
        public string id { get; set; }
        public long crawlTimeMsec { get; set; }
        public long timestampUsec { get; set; }
        public string title { get; set; }
        public DateTime published { get; set; }
        public DateTime updated { get; set; }
        public List<Enclosure> enclosure { get; set; }
        public List<Link> alternate { get; set; }
        //public List< MyProperty { get; set; }
        public Content summary { get; set; }
        public string author { get; set; }
        public List<User> likingUsers { get; set; }
        public List<Comment> comments { get; set; }
        public List<Annotation> annotations { get; set; }
        public Origin origin { get; set; }
    }
}
