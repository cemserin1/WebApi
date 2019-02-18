using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Api.Interfaces;
using InGame.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InGame.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        // GET: api/Category
        [HttpGet(Name = "GetCategories")]
        public List<CategoryResponseModel> Get()
        {
            #region Recursive Stored Procedure
            //Girilen ID'nin tum child node'larini getiriyor. Benzer sekilde parent'a giden versiyonu da yazilabilir. (Metoda parametre eklenmeli vs.)
            //var categoryList = categoryService.GetChildCategories(2).Select(c => new CategoryResponseModel() { Id = c.Id, Name = c.Name, ParentCategoryId = c.ParentCategoryId }).ToList();
            //return categoryList;
            #endregion
            var categoryList = categoryService.Get().ToList().Select(c => new CategoryResponseModel() { Id = c.Id, Name = c.Name, ParentCategoryId = c.ParentCategoryId }).ToList();
            return categoryList;
        }

        //[HttpGet(Name = "GetChildCategories")]
        //public List<CategoryResponseModel> Get(long categoryId)
        //{
         
        //}

        //// GET: api/Category/5
        //[HttpGet("{id}", Name = "GetCategoryById")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Category
        [HttpPost]
        public void Post(CategoryRequestModel model)
        {
            if (model.Id > 0)
            {
                categoryService.Update(model.ToDto());
            }
            else
            {
                categoryService.Add(model.ToDto());
            }
        }

        // PUT: api/Category/5
        [HttpPut]
        public void Put(CategoryRequestModel model)
        {
            categoryService.Update(model.ToDto());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            categoryService.Delete(id);
        }
    }
}
