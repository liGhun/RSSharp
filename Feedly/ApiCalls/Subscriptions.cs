using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSharp.Feedly.Model;
using RSSharp.Common;
using Newtonsoft.Json;

namespace RSSharp.Feedly.ApiCalls
{
    public class Subscriptions
    {
        public static List<Subscription> get(string access_token)
        {
            string requestUrl = string.Format("{0}/v3/subscriptions", Configuration.base_url);
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

            return JsonConvert.DeserializeObject<List<Subscription>>(response.Content, settings);
        }

        public static bool subscribe(string access_token, string id, string title, string sortid, List<Category> categories) {
            string requestUrl = string.Format("{0}/v3/subscriptions", Configuration.base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            json_parameter_subscribe json = new json_parameter_subscribe();
            json.id = id;
            json.title = title;
            json.sortid = sortid;
            json.categories = categories;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            string jsonString = JsonConvert.SerializeObject(json, settings);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPutRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true,
                            use_PATCH_instead: true);

            return response.Success;
        }

        public static bool update(string access_token, string id, string title, string sortid, List<Category> categories)
        {
            string requestUrl = string.Format("{0}/v3/subscriptions", Configuration.base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            json_parameter_subscribe json = new json_parameter_subscribe();
            json.id = id;
            json.title = title;
            json.sortid = sortid;
            json.categories = categories;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            string jsonString = JsonConvert.SerializeObject(json, settings);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPostRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true);

            return response.Success;
        }

   /*     public static bool update(string access_token, string id, string title, string sortid, List<Category> categories)
        {
            string requestUrl = string.Format("{0}/v3/subscriptions", Configuration.base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendDeleteRequest(requestUrl, headers);

            return response.Success;

        }
    * */

        private class json_parameter_subscribe
        {
            public string id { get; set; }
            public string title { get; set; }
            public string sortid { get; set; }
            public List<Category> categories { get; set; }
        }
    }
}
