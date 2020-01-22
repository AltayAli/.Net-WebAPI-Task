using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductAPİ.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductAPİ.Operations
{
    public class TranslateOperation
    {
        public static async Task<string> TranslateTextAsync(string text)
        {
            RestClient restClient = new RestClient("https://localhost:44319/api/translator");
            RestRequest request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            TextModel model = new TextModel
            {
                Text = text
            };
            var data = JsonConvert.SerializeObject(model);
            JObject obj = JObject.Parse(data);
            request.AddParameter("application/json", obj, ParameterType.RequestBody);
            var response = await restClient.ExecuteAsync(request, CancellationToken.None);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.Trim('"','\\');
            }
            else
                return "";
        }
    }
}
