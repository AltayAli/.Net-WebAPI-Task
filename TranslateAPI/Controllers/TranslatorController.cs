using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.Infrastructur;
using TranslateAPI.Models;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private readonly ITranslatorRepository _repository;
        public TranslatorController(ITranslatorRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
       public async Task<IActionResult> TranslateText([FromBody]TextModel textModel)
        {
            try
            {
                var response=await _repository.TranslateTextAsync(textModel.Text);
                return Ok(response);

            }
            catch (Exception)
            {

                return BadRequest("Error!!!");
            }
        }
    }
}