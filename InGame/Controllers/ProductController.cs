using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Api.Helpers;
using InGame.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InGame.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly Uri Uri;

        public ProductController()
        {
            Uri = new Uri("https://localhost:44358");
        }

        public async Task<IActionResult> Index()
        {
            var productList = await RestHelper.GetObjects<ProductResponseModel>(new Uri(Uri, "/api/product"));
            //var childCategories = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category/GetChildCategories?categoryId=2"));
            return View(productList);
        }
    }
}