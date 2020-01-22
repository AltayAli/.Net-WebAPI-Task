using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslateAPI.Models;
using YandexTranslator;

namespace TranslateAPI.Infrastructur
{
    public class TranslatorRepository : ITranslatorRepository
    {
        private readonly Translator _translator;
        private IConfiguration _configuration;
        private string Key { get; set; }
        private string Lang { get; set; }
        public TranslatorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Key = _configuration.GetSection("ApiKey").Value;
            Lang = _configuration.GetSection("Lang").Value;
            _translator = new Translator(Key, Lang);
        }
        public async Task<string> TranslateTextAsync(string text)
        {
            string result = await _translator.TranslateTextAsync(text);
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(result);
            var response = model.Text[0].Trim('"', '\\');
            return response;
        }
    }
}
