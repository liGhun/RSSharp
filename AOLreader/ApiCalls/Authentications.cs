using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSharp.AOLreader.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace RSSharp.AOLreader.ApiCalls
{
    public class Authentications
    {
        public static string get_authentication_url(string response_type = "code", string client_id = "", string redirect_uri = "", string scope ="", string state = "") {
            return string.Format("https://api.screenname.aol.com/auth/authorize?response_type={0}&client_id={1}&redirect_uri={2}&scope={3}&state={4}", response_type, System.Web.HttpUtility.UrlEncode(client_id), System.Web.HttpUtility.UrlEncode(redirect_uri), System.Web.HttpUtility.UrlEncode(scope), System.Web.HttpUtility.UrlEncode(state));
        }



        public static Authentication.auth_response parse_authentication_reponse(string url)
        {
            Authentication.auth_response auth_response = new Authentication.auth_response();
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (url.Contains("code="))
                {
                    Uri uri = new Uri(url);
                    string code = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("code");
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        auth_response.code = code;
                        auth_response.error_message = null;
                    }
                    else
                    {
                        auth_response.error_message = "Code parsing failed";
                    }
                }
            }
            else if (url.Contains("error="))
            {
                Uri uri = new Uri(url);
                string error = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("error");
                if (!string.IsNullOrWhiteSpace(error))
                {
                    auth_response.error_message = error;
                }
                else
                {
                    auth_response.error_message = "Error unreadable in string " + url;
                }
            }

            return auth_response;
        }

        public static Authentication.token get_access_token(string code, string client_id, string client_secret, string redirect_uri, string grant_type = "authorization_code")
        {
            json_parameters_get_access_token_by_code json = new json_parameters_get_access_token_by_code();
            json.code = code;
            json.client_id = client_id;
            json.client_secret = client_secret;
            json.redirect_uri = redirect_uri;
            json.grant_type = grant_type;

            string requestUrl = string.Format("https://api.screenname.aol.com/auth/access_token");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> content = new Dictionary<string,string>();
            content.Add("code", System.Web.HttpUtility.UrlEncode(code));
            content.Add("client_id",client_id);
            content.Add("client_secret", System.Web.HttpUtility.UrlEncode(client_secret));
            content.Add("redirect_uri", redirect_uri);
            content.Add("grant_type",grant_type);

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPostRequest(
                requestUrl,
                content,
                headers,
                true);


            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            return JsonConvert.DeserializeObject<AOLreader.Model.Authentication.token>(response.Content, settings);
        }

        public static Authentication.token get_access_token_by_refresh_token(string refresh_token, string client_id, string client_secret, string grant_type = "refresh_token")
        {
            string requestUrl = string.Format("https://api.screenname.aol.com/auth/access_token");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("refresh_token", System.Web.HttpUtility.UrlEncode(refresh_token));
            content.Add("client_id", System.Web.HttpUtility.UrlEncode(client_id));
            content.Add("client_secret", System.Web.HttpUtility.UrlEncode(client_secret));
            content.Add("grant_type", grant_type);


            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                throw args.ErrorContext.Error;
            };

            Common.HTTPCommunications.Response response = Common.HTTPCommunications.SendPostRequest(
                            requestUrl,
                            content,
                            headers,
                            true);

            return JsonConvert.DeserializeObject<Model.Authentication.token>(response.Content, settings);
        }

        private class json_parameters_get_access_token_by_code
        {
            public string code { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string redirect_uri { get; set; }
            public string grant_type { get; set; }
        }

        private class json_parameters_get_access_token_by_refresh_token
        {
            public string refresh_token { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string grant_type { get; set; }
        }

    }
}
