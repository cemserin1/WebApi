using InGame.Api.Interfaces;
using InGame.DAL;
using InGame.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void Add(Category c)
        {
            context.Category.Add(c);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            var category = context.Category.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                context.Category.Remove(category);
                context.SaveChanges();
            }
        }

        public Category Get(long id)
        {
            return context.Category.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Category> Get()
        {

            return context.Category.AsQueryable();
        }

        public List<Category> GetChildCategories(long categoryId)
        {
            List<Category> categoryList = new List<Category>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetChildCategories";
                command.Parameters.Add(new SqlParameter("CategoryId", categoryId));
                command.CommandType = CommandType.StoredProcedure;
                context.Database.OpenConnection();
                var dataReader = command.ExecuteReader();
                
                while (dataReader.Read())
                {
                    string Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                    long Id = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                    long ParentCategoryId = dataReader.GetInt64(dataReader.GetOrdinal("ParentCategoryId"));
                    categoryList.Add(new Category() { Id = Id, Name = Name, ParentCategoryId = ParentCategoryId });
                }
            }
            return categoryList;
        }

        public long Update(Category c)
        {
            var category = context.Category.FirstOrDefault(cc => cc.Id == c.Id);
            if (category != null)
            {
                context.Entry(category).CurrentValues.SetValues(c);
                context.SaveChanges();
            }
            return c.Id;
        }
    }
}
