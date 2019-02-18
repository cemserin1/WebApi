using InGame.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Interfaces
{
    public interface ICategoryRepository
    {
        Category Get(long id);
        IQueryable<Category> Get();
        List<Category> GetChildCategories(long categoryId);
        void Add(Category p);
        long Update(Category p);
        void Delete(long id);
    }
}
