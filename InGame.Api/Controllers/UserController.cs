using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Api.Models;
using InGame.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        // GET: api/User
        //[HttpGet(Name ="GetUser")]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
        //[Route("api/[controller]/[action]")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        public async Task<IActionResult> Post(UserRequestModel model)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "Admin";
            user.Email = model.Email;
            user.PasswordHash = model.Password;

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }

            return null;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
