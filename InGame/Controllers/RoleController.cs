using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InGame.Api.Helpers;
using InGame.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static InGame.Api.Models.ApplicationUserModel;

namespace InGame.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly Uri Uri;

        public RoleController()
        {
            Uri = new Uri("https://localhost:44358");
        }

        //[Authorize("Admin")]
        public async Task<IActionResult> Index()
        {
            var roleList = await RestHelper.GetObjects<IdentityRole>(new Uri(Uri, "/api/role"));
            return View(roleList);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roleList = await RestHelper.GetObjects<RoleDetails>(new Uri(Uri, "/api/role/" + id));
            return View(roleList.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(new Uri(Uri, "/api/role"), content);
                return View();
            }
        }
    }
}
