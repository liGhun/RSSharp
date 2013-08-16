using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSharp.Feedly.Model;

namespace RSSharp.Feedly.ApiCalls
{
    public class Streams
    {
        public static id_list get_ids_in_stream(string access_token, string stream_id, int? count = null, string ranked = null, bool? unread_only = null, long? newer_than = null, string continuation = null)
        {
            string requestUrl = string.Format("{0}/v3/streams/{1}/ids?ct={2}", Configuration.base_url, stream_id);
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("count", count);
            parameter.Add("ranked", ranked);
            parameter.Add("unreadOnly", unread_only);
            parameter.Add("newerThan", newer_than);
            parameter.Add("continuation", continuation);
            string parameter_string = Common.GetParameter.get(parameter);
            if (!string.IsNullOrWhiteSpace(parameter_string))
            {
                requestUrl += "&" + parameter_string;
            }

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

            return JsonConvert.DeserializeObject<id_list>(response.Content, settings);
        }

        public static entries_list get_entries_in_stream(string access_token, string stream_id, int? count = null, string ranked = null, bool? unread_only = null, long? newer_than = null, string continuation = null)
        {
            string requestUrl = string.Format("{0}/v3/streams/{1}/contents?ct={2}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(stream_id), System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("count", count);
            parameter.Add("ranked", ranked);
            parameter.Add("unreadOnly", unread_only);
            parameter.Add("newerThan", newer_than);
            parameter.Add("continuation", continuation);
            string parameter_string = Common.GetParameter.get(parameter);
            if (!string.IsNullOrWhiteSpace(parameter_string))
            {
                requestUrl += "&" + parameter_string;
            }

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

            return JsonConvert.DeserializeObject<entries_list>(response.Content, settings);
        }

        public class id_list
        {
            public List<string> ids { get; set; }
            public string continuation { get; set; }
        }

        public class entries_list
        {
            public string direction { get; set; }
            public string id { get; set; }
            public string title { get; set; }
            public string continuation { get; set; }
            public List<Link> self { get; set; }
            public List<Link> alternate { get; set; }
            public long updated { get; set; }
            public List<Entry> items { get; set; }
        }
    }
}
