using InGame.Api.Dtos;
using InGame.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public void Add(ProductDto p)
        {
            productRepository.Add(p.FromDto());
        }
        public void Delete(long id)
        {
            productRepository.Delete(id);
        }
        public ProductDto Get(long id)
        {
            return new ProductDto(productRepository.Get(id));            
        }
        public IQueryable<ProductDto> Get()
        {
            var products = productRepository.Get().Select(p => new ProductDto(p));
            return products;
        }
        public long Update(ProductDto p)
        {
            return productRepository.Update(p.FromDto());
        }
    }
}
