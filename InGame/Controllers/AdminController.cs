using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace InGame.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Create(UserModel model)
        {            
            return View();
        }
    }
}