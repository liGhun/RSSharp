using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.GeneralGoogleReaderAPI.Model
{
    public class User
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string userProfileId { get; set; }
        public string userEmail { get; set; }
        public bool isBloggerUser { get; set; }
        public long signupTimeSec { get; set; }
    }
}
