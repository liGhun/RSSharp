using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSharp.Feedly.Model;

namespace RSSharp.Feedly.ApiCalls
{
    public class Entries
    {
        public static Entry get(string access_token, string entry_id)
        {
            string requestUrl = string.Format("{0}/v3/entries/{1}", Configuration.base_url, entry_id);
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

            return JsonConvert.DeserializeObject<Entry>(response.Content, settings);
        }

        public static List<Entry> mget(string access_token, List<string> entry_ids, string continuation = null)
        {
            string requestUrl = string.Format("{0}/v3/entries/.mget", Configuration.base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            json_parameters_mget_entries json = new json_parameters_mget_entries();
            json.ids = entry_ids;
            json.continuation = continuation;

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

            
            return JsonConvert.DeserializeObject<List<Entry>>(response.Content, settings);
        }

        private class json_parameters_mget_entries
        {
            public List<string> ids { get; set; }
            public string continuation { get; set; }
        }
    }
}
