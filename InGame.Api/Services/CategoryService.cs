using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Api.Dtos;

namespace InGame.Api.Interfaces
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        public void Add(CategoryDto p)
        {
            if (!categoryRepository.Get().Any(c => c.Name == p.Name))
            {
                categoryRepository.Add(p.FromDto());
            }            
        }

        public void Delete(long id)
        {
            categoryRepository.Delete(id);
        }

        public CategoryDto Get(long id)
        {
            return new CategoryDto(categoryRepository.Get(id));
        }

        public IQueryable<CategoryDto> Get()
        {
            var categoryList = categoryRepository.Get();
            return categoryList.Select(c => new CategoryDto(c));
        }

        public List<CategoryDto> GetChildCategories(long categoryId)
        {
            var childCategories = categoryRepository.GetChildCategories(categoryId).Select(c => new CategoryDto(c)).ToList();
            return childCategories;
        }

        public long Update(CategoryDto p)
        {
            categoryRepository.Update(p.FromDto());
            return p.Id;
        }
    }
}
