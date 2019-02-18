using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Models
{
    public class CategoryRequestModel
    {
        public long Id { get; set; }
        public long ParentCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public CategoryDto ToDto()
        {
            CategoryDto dto = new CategoryDto
            {
                Name = Name,
                Id = Id,
                ParentCategoryId = ParentCategoryId
            };
            return dto;
        }
    }
}
