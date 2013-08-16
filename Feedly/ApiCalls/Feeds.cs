using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSharp.Feedly.Model;

namespace RSSharp.Feedly.ApiCalls
{
    public class Feeds
    {
        public static Feed get(string access_token, string feed_id)
        {
            string requestUrl = string.Format("{0}/v3/feeds/{1}?ct={2}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(feed_id), System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
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

            return JsonConvert.DeserializeObject<Feed>(response.Content, settings);
        }

        public static List<Feed> mget(string access_token, List<string> feed_ids)
        {
            string requestUrl = string.Format("{0}/v3/feeds/.mget?ct={1}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            string jsonString = JsonConvert.SerializeObject(feed_ids);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPostRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            return JsonConvert.DeserializeObject<List<Feed>>(response.Content, settings);
        }
    }
}
