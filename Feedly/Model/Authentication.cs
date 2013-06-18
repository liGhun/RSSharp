using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RSSharp.Feedly.Model
{
    public class Authentication
    {
        public class auth_response
        {
            public bool success
            {
                get
                {
                    return !string.IsNullOrWhiteSpace(code);
                }
            }

            public string code { get; set; }
            public string error_message { get; set; }
        }

        public string code { get; set; }

        public class token
        {
            public string id { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public long expires_in
            {
                get
                {
                    return _expires_in;
                }
                set
                {
                    expiration_date = DateTime.Now.AddSeconds(value).ToLocalTime();
                    _expires_in = value;
                }
            }
            private long _expires_in {get;set;}
            public string token_type { get; set; }

            public DateTime expiration_date { get; set; }
        }
    }
}
