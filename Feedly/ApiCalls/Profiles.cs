using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSharp.Feedly.Model;
using Newtonsoft.Json;

namespace RSSharp.Feedly.ApiCalls
{
    public class Profiles
    {
        public static Profile get(string access_token)
        {
            string requestUrl = string.Format("{0}/v3/profile?ct={1}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendGetRequest(
                            requestUrl,
                            headers);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            return JsonConvert.DeserializeObject<Profile>(response.Content, settings);
        }
    }
}
