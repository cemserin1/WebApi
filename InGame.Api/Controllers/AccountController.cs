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
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        //GET: api/Account
        //[HttpGet(Name = "GetAccount")]
        //[Route("api/[controller]/[action]")]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Account
        [HttpPost]
        public async Task<UserResponseModel> Login(UserRequestModel _user)
        {
            var user = await userManager.FindByEmailAsync(_user.Email);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(user.UserName, _user.Password, true, false);
                //var result = await signInManager.PasswordSignInAsync(user, passwordHash, false, false);
                if (result.Succeeded)
                {
                    return new UserResponseModel()
                    {
                        Email = user.Email,
                        Name = user.UserName,
                        IsSuccessful = true
                    };
                }
            }
            return new UserResponseModel()
            {
                IsSuccessful = false
            };

        }
        //[HttpPost]
        //public async Task<ApplicationUser> Test(string email)
        //{
        //    var myEm = email;
        //    return null;
        //}


        // GET: api/Account/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Account
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //    var mmm = value;
        //}

        // PUT: api/Account/5
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
