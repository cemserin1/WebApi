using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InGame.Api.Helpers;
using InGame.Api.Models;
using InGame.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace InGame.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Uri Uri;

        public CategoryController()
        {
            Uri = new Uri("https://localhost:44358");
        }
        //cookie consent'in startup ayarini ayaga kaldiramadigim icin bu hata donuyor..
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            //var childCategories = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category/GetChildCategories?categoryId=2"));
            return View(categoryList);
        }

        public async Task<IActionResult> Edit(long id, string name, long parentid)
        {
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
            return View("Create", new CategoryModel() { Name = name, Id = id, ParentCategoryId = parentid });
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
            var categoryList = await RestHelper.GetObjects<CategoryResponseModel>(new Uri(Uri, "/api/category"));
            categoryList.Insert(0, new CategoryResponseModel() { Id = 0, Name = "No Category", ParentCategoryId = 0 });
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
            return View(new CategoryModel() { Id = 0 });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    var jsonObject = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(new Uri(Uri, "/api/category"), content);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {                    
                    var response = await client.DeleteAsync(new Uri(Uri, "/api/category/" + model.Id));
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}