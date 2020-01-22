using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YandexTranslator
{
    public class Translator
    {
        private  string ApiKEY{get;set;}
        private string Lang { get; set; }
        private string MainUrl { get; set; }

        public Translator(string key,string lang)
        {
            ApiKEY = key;
            Lang = lang;
            MainUrl= @"https://translate.yandex.net/api/v1.5/tr.json/translate";
        }

        public async Task<string> TranslateTextAsync(string text) {

            var url = MainUrl + $"?key={ApiKEY}&lang={Lang}&text=\"{text}\"";
            RestClient restClient = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = await restClient.ExecuteAsync(request, CancellationToken.None);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ToString();
            }
            return "";
        }
    }
}
