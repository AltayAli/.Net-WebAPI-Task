using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateAPI.Models
{
    public class ResponseModel
    {
        public int Code { get; set; }
        public string Lang { get; set; }
        public string[] Text { get; set; }
    }
}
