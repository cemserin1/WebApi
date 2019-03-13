using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InGame.Api.Helpers;
using InGame.Api.Models;
using InGame.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;

namespace InGame.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly Uri Uri;
        private IRestClient restClient;

        public ProductController(IRestClient _restClient)
        {
            Uri = new Uri("https://localhost:44358");
            restClient = _restClient;
            restClient.BaseUrl = Uri;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await RestHelper.GetObjects<ProductResponseModel>(new Uri(Uri, "/api/product"));
            //var childCategories = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category/GetChildCategories?categoryId=2"));
            return View(productList);
        }
        public async Task<IActionResult> Edit(long id, string name, long parentid)
        {
            var request = new RestRequest(Uri + "api/product/" + id, Method.GET, DataFormat.Json);
            //request.AddParameter("id", id);
            IRestResponse response = restClient.Execute(request);
            var content = response.Content; // raw content as string
            var model = JsonConvert.DeserializeObject<ProductModel>(content);
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    var jsonObject = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(new Uri(Uri, "/api/category"), content);
                    return RedirectToAction("Index");
                }
            }
            return View("Create", model);
        }

        public async Task<IActionResult> Create()
        {
            var productList = await RestHelper.GetObjects<ProductResponseModel>(new Uri(Uri, "/api/product"));
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
            ViewBag.Products = new SelectList(productList, "Id", "Name");
            return View(new ProductModel() { Id = 0 });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    var jsonObject = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(Uri, "/api/product"), content);
                    return RedirectToAction("Index");
                }
            }
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
            return View(model);
        }
    }
}