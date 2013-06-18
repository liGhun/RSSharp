using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly.Model
{
    public class Profile
    {
        public string id { get; set; }
        public string email { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }
        public string reader { get; set; }
        public string google { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string wave { get; set; }
    }
}
