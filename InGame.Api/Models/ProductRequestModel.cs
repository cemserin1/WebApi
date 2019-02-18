using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Models
{
    public class ProductRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public ProductDto ToDto()
        {
            ProductDto dto = new ProductDto();
            dto.CategoryId = CategoryId;
            dto.Name = Name;
            dto.Price = Price;
            dto.ImageUrl = ImageUrl;
            dto.IsActive = IsActive;
            dto.Description = Description;
            return dto;
        }
    }
}
