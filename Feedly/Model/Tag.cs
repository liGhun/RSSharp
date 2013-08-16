using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly.Model
{
    public class Tag
    {
        public string id { get; set; }
        public string label { get; set; }

        public static string get_tag_id_for_label(string label, string user_id)
        {
            return string.Format("user/{0}/tag/{1}", System.Web.HttpUtility.UrlEncode(user_id),System.Web.HttpUtility.UrlEncode(label));
        }
    }
}
