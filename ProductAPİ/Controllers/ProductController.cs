using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPİ.Data.Entites;
using ProductAPİ.Models;
using ProductAPİ.Operations;

namespace ProductAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _database;
        private readonly IMapper _mapper;
        public ProductController(ProductDBContext productDBContext,IMapper mapper)
        {
            _database = productDBContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var products = await _database.Products.ToListAsync();
            List<ProductModel> productModels = new List<ProductModel>();
            foreach(var product in products)
            {
                ProductModel model = _mapper.Map<ProductModel>(product);
                model.DesscriptionENG =await TranslateOperation.TranslateTextAsync(product.Description);
                productModels.Add(model);
            }
            return productModels;
        }
        [HttpGet("{id}")]
        public async Task<ProductModel> GetProductByID(int id)
        {
            var product = await _database.Products.FindAsync(id);
            ProductModel model = _mapper.Map<ProductModel>(product);
            model.DesscriptionENG = await TranslateOperation.TranslateTextAsync(product.Description);
            return model;
        }

        [HttpPost]
        public async Task AddProduct(ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            await _database.Products.AddAsync(product);
            await _database.SaveChangesAsync();
        }
        [HttpPut("{id}")]
        public async Task UpdateProduct(int id,[FromBody] ProductModel model)
        {
            var product =await _database.Products.FindAsync(id);
            product.Description = model.Description;
            product.Name = model.Name;
            product.Price = model.Price;
            await _database.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            var product = await _database.Products.FindAsync(id);
            if (product != null)
            {
                _database.Products.Remove(product);
                await _database.SaveChangesAsync();
            }
        }
    }
}