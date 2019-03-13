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
using RestSharp;

namespace InGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly Uri Uri;
        private IRestClient restClient;

        public HomeController(IRestClient _restClient)
        {
            restClient = _restClient;
            Uri = new Uri("https://localhost:44358");
            restClient.BaseUrl = Uri;
        }

        public async Task<IActionResult> Index()
        {
            //var request = new RestRequest(Uri + "api/product/1", Method.GET, DataFormat.Json);
            //IRestResponse response = restClient.Execute(request);
            //var content = response.Content; // raw content as string
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
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
                        return View("Index", model);
                    }
                }
            }
            return View("Index", model);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
