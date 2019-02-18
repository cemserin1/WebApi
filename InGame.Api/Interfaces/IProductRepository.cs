using InGame.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Interfaces
{
    public interface IProductRepository
    {
        Product Get(long id);
        IQueryable<Product> Get();
        void Add(Product p);
        long Update(Product p);
        void Delete(long id);
    }
}
