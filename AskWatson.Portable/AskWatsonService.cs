using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AskWatson.Portable
{
    public class AskWatsonService
    {
        #region privates
        private static string urlBase_ = "https://gateway.watsonplatform.net/qagw/service";
        private static string username_ = null;
        private static string password_ = null;
        #endregion

        #region public methods
        // Method to call the Watson Question and Answer service
        public static async Task<Models.AskWatsonResponse.Rootobject> AskWatson(
            string knowledgeDomain,
            string question)
        {
            // TODO: enter your own service-specific credentials
            username_ = "{yourserviceusername}";
            password_ = "{yourservicepassword}";
            
            knowledgeDomain = knowledgeDomain.ToLower();
            string jsonRequestString = "{\"question\": {\"questionText\": \"" + question.Replace("\"", "'") + "\"}}";

            string url = string.Format(
                "{0}/v1/question/{1}",
                urlBase_,
                knowledgeDomain);

            string jsonResponse = await _GetJsonResponse(
                url,
                jsonRequestString);
            
            List<JToken> tokens = JArray.Parse(jsonResponse).ToList();

            Models.AskWatsonResponse.Rootobject questionModel = JsonConvert.DeserializeObject<Models.AskWatsonResponse.Rootobject>(
                tokens[0].ToString(),
                _GetSettings());

            return questionModel;
        }

        public static Models.UserModelingResponse.Rootobject ModelUser(string jsonResponse)
        {
            Models.UserModelingResponse.Rootobject rootObject = JsonConvert.DeserializeObject<Models.UserModelingResponse.Rootobject>(jsonResponse);

            return rootObject;
        }

        public static async Task<string> GetModelUserJsonResponse(string body)
        {
            // TODO: enter your own service-specific credentials
            username_ = "{yourserviceusername}";
            password_ = "{yourservicepassword}";
            
            string url = string.Format(
                "{0}/api/v2/profile",
                urlBase_.Replace("qagw", "systemu"));

            string jsonRequestString = "{\"contentItems\": [{\"content\": \"" + body.Replace("\"", "'").Replace("\n", "") + "\"}]}";

            string jsonResponse = await _GetJsonResponse(
                url,
                jsonRequestString).ConfigureAwait(false);

            return jsonResponse;
        }
        #endregion

        public static async Task<string> VisualizeUserModel(string jsonContent, int height, int width, string profileImageUrl)
        {
            // TODO: enter your own service-specific credentials
            username_ = "{yourserviceusername}";
            password_ = "{yourservicepassword}";
            
            string url = string.Format(
                "{0}/api/v2/visualize?w={1}&h={2}&imgurl={3}",
                urlBase_.Replace("qagw", "systemu"),
                width,
                height,
                System.Net.WebUtility.UrlEncode(profileImageUrl));

            string jsonResponse = await _GetJsonResponse(
                url,
                jsonContent).ConfigureAwait(false);

            return jsonResponse;
        }

        #region private methods
        private static async Task<string> _GetJsonResponse(
            string url,
            string requestContent)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username_, password_))));

                var httpContent = new StringContent(
                    requestContent,
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(
                    url,
                    httpContent).ConfigureAwait(false);

                string responseText = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                return responseText;
            }
            catch{
                return null;
            }
        }

        private static JsonSerializerSettings _GetSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Error = _DeserializerErrorHandler
            };

            return settings;
        }

        private static void _DeserializerErrorHandler(object sender, ErrorEventArgs args)
        {
            if (args != null)
            {
                args.ErrorContext.Handled = true;
            }
        }
        #endregion
    }
}
