using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSharp.Common
{
    public class GetParameter
    {
        public static string get(Dictionary<string, object> parameters) {
            string parameter_string = "";
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                    {
                        parameter_string += string.Format("{0}={1}&", parameter.Key, System.Web.HttpUtility.UrlEncode(parameter.Value.ToString()));
                    }
                }
            }
            parameter_string = parameter_string.TrimEnd('&');

            return parameter_string;
        }
    }
}
