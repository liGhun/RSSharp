using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly.Model
{
    public class Entry
    {
        public string id { get; set; }
        public bool unread { get; set; }
        public List<Category> categories { get; set; }
        public List<Tag> tags { get; set; }
        public string title { get; set; }
        public List<string> keywords { get; set; }
        public long published { get; set; }
        public long updated { get; set; }
        public long crawled { get; set; }
        public List<Link> alternate { get; set; }
        public Content content { get; set; }
        public Content summary { get; set; }
        public string author { get; set; }
        public List<Link> canonical { get; set; }
        public Origin origin { get; set; }

        public string html
        {
            get
            {
                if (content != null)
                {
                    return content.content;
                }
                else if (summary != null)
                {
                    return summary.content;
                }
                else
                {
                    return "";
                }
            }
        }
    }

    public class Link
    {
        public string href { get; set; }
        public string type { get; set; }
    }

    public class Content
    {
        public string direction { get; set; }
        public string content { get; set; }
    }

    public class Origin
    {
        public string streamId { get; set; }
        public string title { get; set; }
        public string htmlUrl { get; set; }
    }

}
