using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSharp.Feedly.Model;

namespace RSSharp.Feedly.ApiCalls
{
    public class Categories
    {
        public static List<Category> get_all(string access_token)
        {
            string requestUrl = string.Format("{0}/v3/categories", Configuration.base_url);
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

            return JsonConvert.DeserializeObject<List<Category>>(response.Content, settings);
        }
    }
}
