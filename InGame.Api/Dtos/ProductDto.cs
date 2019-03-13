using InGame.Api.Models;
using InGame.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public CategoryDto CategoryDto { get; set; }

        public ProductDto()
        {

        }
        
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            CategoryId = product.CategoryId;
            Price = product.Price;
            ImageUrl = product.ImageUrl;
            IsActive = product.IsActive;
            Description = product.Description;
            if (product.Category != null)
            {
                CategoryDto = new CategoryDto(product.Category);
            }
        }

        public Product FromDto()
        {
            Product p = new Product();
            p.Id = Id;
            p.Name = Name;
            p.CategoryId = CategoryId;
            p.Price = Price;
            p.ImageUrl = ImageUrl;
            p.IsActive = IsActive;
            p.Description = Description;
            return p;
        }

        public ProductResponseModel ToResponseModel()
        {
            ProductResponseModel p = new ProductResponseModel();
            p.Id = Id;
            p.Name = Name;
            p.CategoryId = CategoryId;
            p.Price = Price;
            p.ImageUrl = ImageUrl;
            p.IsActive = IsActive;
            p.Description = Description;
            p.CategoryDto = CategoryDto;
            return p;
        }
    }
}
