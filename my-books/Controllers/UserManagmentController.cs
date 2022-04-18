using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_books.Auth;
using my_books.Data.Models.ViewModels;
using System.Security.Claims;

namespace my_books.Controllers
{
    [Authorize(Roles = UserRoles.SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserManagmentController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPut("add-permission")]
        public async Task<IActionResult> AddPermission([FromBody] RoleVM roleVM)
        {
            IdentityUser user = await _userManager.FindByIdAsync(roleVM.RoleId);

            if (user == null)
                return BadRequest(user);
            else
            {
                await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, roleVM.Permission));
                return Ok(user);
            }
        }
    }
}
