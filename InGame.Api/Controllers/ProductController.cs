using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Api.Interfaces;
using InGame.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        // GET: api/Product
        [HttpGet(Name = "GetProduct")]
        public List<ProductResponseModel> Get()
        {
            var products = productService.Get().Select(p => p.ToResponseModel());
            return products.ToList();
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProductById")]
        public ProductResponseModel Get(int id)
        {
            var p = productService.Get(id);
            return p.ToResponseModel();
        }

        // POST: api/Product
        [HttpPost]
        public void Post(ProductRequestModel productModel)
        {
            if (ModelState.IsValid)
            {
                productService.Add(productModel.ToDto());
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, ProductRequestModel model)
        {
            var productToBeUpdated = productService.Get(id);
            productToBeUpdated.CategoryId = model.CategoryId;
            productToBeUpdated.Description = model.Description;
            productToBeUpdated.ImageUrl = model.ImageUrl;
            productToBeUpdated.Name = model.Name;
            productToBeUpdated.Price = model.Price;
            productService.Update(productToBeUpdated);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productService.Delete(id);
        }
    }
}
