using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateAPI.Infrastructur
{
    public interface ITranslatorRepository
    {
        Task<string> TranslateTextAsync(string text);
    }
}
