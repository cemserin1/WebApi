using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InGame.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using InGame.Api.Models;
using InGame.Api.Helpers;
using InGame.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Newtonsoft.Json;

namespace InGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly Uri Uri;

        public HomeController()
        {
            Uri = new Uri("https://localhost:44358");
        }

        public async Task<IActionResult> Index()
        {            
            var productList = await RestHelper.GetObjects<ProductResponseModel>(new Uri(Uri, "/api/product"));
            return View();
        }


        public IActionResult Login()
        {
            return View("Test");
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            using (HttpClient client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(new Uri(Uri, "/api/account"), content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserResponseModel>(responseContent);
                if (user.IsSuccessful)
                {
                    //Login successful
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
