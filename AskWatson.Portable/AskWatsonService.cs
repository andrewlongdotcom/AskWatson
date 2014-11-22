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
        private static string username_ = "{your_service_username}";
        private static string password_ = "{your_service_password}";
        #endregion

        #region public methods
        public static async Task<Models.Rootobject> AskWatson(
            string knowledgeDomain,
            string question)
        {
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

            Models.Rootobject questionModel = JsonConvert.DeserializeObject<Models.Rootobject>(
                tokens[0].ToString(),
                _GetSettings());

            return questionModel;
        }
        #endregion

        #region private methods
        private static async Task<string> _GetJsonResponse(
            string url,
            string requestContent)
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
                httpContent);

            return await response.Content.ReadAsStringAsync();
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
