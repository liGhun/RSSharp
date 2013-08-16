using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSharp.Feedly.Model;
using Newtonsoft.Json;

namespace RSSharp.Feedly.ApiCalls
{
    public class Tags
    {
        public static List<Tag> get_all(string access_token)
        {
            string requestUrl = string.Format("{0}/v3/tags?ct={1}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
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

            return JsonConvert.DeserializeObject<List<Tag>>(response.Content, settings);
        }

        public static bool add_to_entry(string access_token, string entry_id, string tag_id)
        {
            List<string> tag_ids = new List<string>();
            tag_ids.Add(tag_id);
            List<string> entry_ids = new List<string>();
            entry_ids.Add(entry_id);
            return add_multiple_to_entries(access_token, entry_ids, tag_ids);
        }

        public static bool add_to_entries(string access_token, List<string> entry_ids, string tag_id)
        {
            List<string> tag_ids = new List<string>();
            tag_ids.Add(tag_id);
            return add_multiple_to_entries(access_token, entry_ids, tag_ids);
        }

        public static bool add_multiple_to_entry(string access_token, string entry_id, List<string> tag_ids)
        {
            List<string> entry_ids = new List<string>();
            entry_ids.Add(entry_id);
            return add_multiple_to_entries(access_token, entry_ids, tag_ids);
        }

        public static bool add_multiple_to_entries(string access_token, List<string> entry_ids, List<string> tag_ids) {
            string requestUrl = string.Format("{0}/v3/tags/{1}?ct={2}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(string.Join(",", tag_ids)), System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            json_tagging json = new json_tagging();
            json.entryIds = entry_ids;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            string jsonString = JsonConvert.SerializeObject(json, settings);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPutRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true);

            return response.Success;
        }

        public static bool delete_multiple_from_entries(string access_token, List<string> entry_ids, List<string> tag_ids)
        {
            string requestUrl = string.Format("{0}/v3/tags/{1}/{2}?ct={3}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(string.Join(",", tag_ids)), System.Web.HttpUtility.UrlEncode(string.Join(",", entry_ids)), System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendDeleteRequest(
                            requestUrl,
                            headers
                            );

            return response.Success;
        }

        public static bool delete_multiple(string access_token, List<string> tag_ids)
        {
            string requestUrl = string.Format("{0}/v3/tags/{1}?ct={2}", Configuration.base_url, System.Web.HttpUtility.UrlEncode(string.Join(",", tag_ids)), System.Web.HttpUtility.UrlEncode(Configuration.user_agent));
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", string.Format("OAuth {0}", access_token));

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendDeleteRequest(
                            requestUrl,
                            headers
                            );

            return response.Success;
        }


        private class json_tagging
        {
            public List<string> entryIds {get;set;}
        }
    }
}
