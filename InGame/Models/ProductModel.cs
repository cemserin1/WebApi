using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public long CategoryId { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
