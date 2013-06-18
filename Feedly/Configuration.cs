using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Feedly
{
    public class Configuration
    {
        public static string user_agent
        {
            get
            {
                return RSSharp.Common.HTTPCommunications.user_agent;
            }
            set
            {
                RSSharp.Common.HTTPCommunications.user_agent = value;
            }
        }

        public static string base_url = "http://cloud.feedly.com";

        public static void activate_sandbox()
        {
            base_url = "http://sandbox.feedly.com";
        }

    }
}
