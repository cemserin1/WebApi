using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto Get(long id);
        IQueryable<CategoryDto> Get();
        List<CategoryDto> GetChildCategories(long categoryId);
        void Add(CategoryDto p);
        long Update(CategoryDto p);
        void Delete(long id);
    }
}
