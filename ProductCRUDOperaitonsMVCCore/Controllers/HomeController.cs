using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductCRUDOperaitonsMVCCore.Models;
using RestSharp;

namespace ProductCRUDOperaitonsMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                RestClient client = new RestClient("https://localhost:44350/api/product");
                RestRequest request = new RestRequest(Method.POST);
                string data = JsonConvert.SerializeObject(model);
                JObject obj = JObject.Parse(data);
                request.AddParameter("application/json", obj, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("List");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            RestClient client = new RestClient($"https://localhost:44350/api/product/{id}");
            RestRequest request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var obj = JsonConvert.DeserializeObject<ProductModel>(response.Content);
                return View(obj);
            }
            else
            {
                return View("List");
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int id, ProductModel model)
        {
            RestClient client = new RestClient($"https://localhost:44350/api/product/{id}");
            RestRequest request = new RestRequest(Method.PUT);
            string data = JsonConvert.SerializeObject(model);
            JObject obj = JObject.Parse(data);
            request.AddParameter("application/json", obj, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> List()
        {
            RestClient client = new RestClient("https://localhost:44350/api/product");
            RestRequest request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var obj = JsonConvert.DeserializeObject<List<ProductModel>>(response.Content);
                return View(obj);
            }
            else
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> Delete(int productid)
        {
            RestClient client = new RestClient($"https://localhost:44350/api/product/{productid}");
            RestRequest request = new RestRequest(Method.DELETE);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var obj = JsonConvert.DeserializeObject<List<ProductModel>>(response.Content);
                return RedirectToAction("List");
            }
            else
            {
                return View("Error");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
