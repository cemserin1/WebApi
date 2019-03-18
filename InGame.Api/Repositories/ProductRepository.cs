using InGame.Api.Interfaces;
using InGame.DAL;
using InGame.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InGame.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void Add(Product p)
        {
            context.Product.Add(p);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            var product = context.Product.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                context.Product.Remove(product);
                context.SaveChanges();
            }
        }

        public Product Get(long id)
        {
            return context.Product.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Product> Get()
        {
            return context.Product.Include(p => p.Category).AsQueryable();
        }

        public long Update(Product p)
        {
            var product = context.Product.FirstOrDefault(pr => pr.Id == p.Id);
            if (product != null)
            {
                context.Entry(product).CurrentValues.SetValues(p);
                context.SaveChanges();
            }
            return p.Id;
        }
    }
}
