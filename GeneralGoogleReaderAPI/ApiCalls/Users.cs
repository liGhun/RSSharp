using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSharp.GeneralGoogleReaderAPI.Model;

namespace RSSharp.GeneralGoogleReaderAPI.ApiCalls
{
    public class Users
    {
        public static UserInfo info(string base_url, string access_token)
        {
            string requestUrl = string.Format("{0}/reader/user/info", base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("Bearer {0}", access_token));

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendGetRequest(
                            requestUrl,
                            headers);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            return JsonConvert.DeserializeObject<UserInfo>(response.Content, settings);
        }
    }
}
