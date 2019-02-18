using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Interfaces
{
    public interface IProductService
    {
        ProductDto Get(long id);
        IQueryable<ProductDto> Get();
        void Add(ProductDto p);
        long Update(ProductDto p);
        void Delete(long id);
    }
}
