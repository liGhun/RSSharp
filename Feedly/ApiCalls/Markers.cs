using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RSSharp.Feedly.ApiCalls
{
    public class Markers
    {
        public static bool mark_entry_as_read(string access_token, string id)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            return mark_entries_as_read(access_token, ids);
        }

        public static bool mark_entries_as_read(string access_token, List<string> ids)
        {
            return marker_action(access_token, "markAsRead", "entries", entry_ids: ids);
        }

        public static bool mark_entry_as_keepUnread(string access_token, string id)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            return mark_entries_as_keepUnread(access_token, ids);
        }

        public static bool mark_entries_as_keepUnread(string access_token, List<string> ids)
        {
            return marker_action(access_token, "keepUnread", "entries", entry_ids: ids);
        }

        public static bool mark_feed_as_read(string access_token, string id)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            return mark_feeds_as_read(access_token, ids);
        }

        public static bool mark_feeds_as_read(string access_token, List<string> ids)
        {
            return marker_action(access_token, "markAsRead", "feeds", feed_ids: ids);
        }

        public static bool mark_category_as_read(string access_token, string id)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            return mark_categories_as_read(access_token, ids);
        }

        public static bool mark_categories_as_read(string access_token, List<string> ids)
        {
            return marker_action(access_token, "markAsRead", "categories", category_ids: ids);
        }

        private static bool marker_action(string access_token, string action, string type, List<string> entry_ids = null, List<string> feed_ids = null, List<string> category_ids = null, long? asOf = null)
        {
            string requestUrl = string.Format("{0}/v3/markers", Configuration.base_url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            json_marker_action json = new json_marker_action();
            json.action = action;
            json.type = type;
            json.entryIds = entry_ids;
            json.feedIds = feed_ids;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            string jsonString = JsonConvert.SerializeObject(json, settings);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPostRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true);

            return response.Success;
        }

        public list_of_unread_counts get_list_of_unread_counts(string access_token, bool? autorefresh = null)
        {
            string requestUrl = string.Format("{0}/v3/markers/counts", Configuration.base_url);
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("autorefresh", autorefresh);
            string parameter_string = Common.GetParameter.get(parameter);
            if (!string.IsNullOrWhiteSpace(parameter_string))
            {
                requestUrl += "?" + parameter_string;
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

            return JsonConvert.DeserializeObject<list_of_unread_counts>(response.Content, settings);
        }

        public class list_of_unread_counts
        {
            public int max { get; set; }
            public List<unread_count> unreadcounts { get; set; }

            public class unread_count
            {
                public string id { get; set; }
                public int count { get; set; }
                public long updated { get; set; }
            }
        }

        private class json_marker_action
        {
            public string action { get; set; }
            public string type { get; set; }
            public List<string> entryIds { get; set; }
            public List<string> feedIds { get; set; }
            public List<string> categoryIds { get; set; }
            public long? asOf { get; set; }
        }
    }
}
