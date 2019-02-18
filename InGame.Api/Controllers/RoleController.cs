using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static InGame.Api.Models.ApplicationUserModel;

namespace InGame.Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        // GET: api/Role
        [HttpGet(Name = "GetRoles")]
        public List<IdentityRole> Get()
        {
            var roleList = roleManager.Roles.ToList();
            return roleList;
        }

        [HttpGet(Name = "IsInRole")]
        public async Task<bool> IsInRole(string email, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            bool isInRole = false;
            if (user != null)
            {
                isInRole = await userManager.IsInRoleAsync(user, role);
            }            
            return isInRole;
        }

        // GET: api/Role/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<List<RoleDetails>> Get(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var members = new List<ApplicationUser>();
                var nonMembers = new List<ApplicationUser>();

                foreach (var user in userManager.Users)
                {
                    var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                    list.Add(user);
                }
                var model = new RoleDetails()
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                };
                return new List<RoleDetails>() { model };
            }
            return null;
        }


        // POST: api/Role
        [HttpPost]
        public async Task Post(RoleEditModel model)
        {
            IdentityResult result;
            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var errors in result.Errors)
                        {
                            ModelState.AddModelError("", errors.Description);
                        }
                    }
                }
            }
            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var errors in result.Errors)
                        {
                            ModelState.AddModelError("", errors.Description);
                        }
                    }
                }
            }
        }

        // PUT: api/Role/5
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
