using InGame.Api.Models;
using InGame.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Dtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public long ParentCategoryId { get; set; }
        public string Name { get; set; }

        public CategoryDto()
        {

        }

        public CategoryDto(Category c)
        {
            Id = c.Id;
            Name = c.Name;
            ParentCategoryId = c.ParentCategoryId;
        }       

        public Category FromDto()
        {
            Category c = new Category
            {
                Name = Name,
                Id = Id,
                ParentCategoryId = ParentCategoryId
            };
            return c;
        }

        public CategoryResponseModel ToResponseModel()
        {
            CategoryResponseModel p = new CategoryResponseModel
            {
                Id = Id,
                Name = Name,
                ParentCategoryId = ParentCategoryId
            };
            return p;
        }
    }
}
