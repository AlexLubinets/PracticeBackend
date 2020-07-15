using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.BLL.Services;

namespace ProductApp.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IAuthService authService;

        public UserProfileController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        [Authorize]
        //GET: /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "Id").Value;
            var user = await authService.FindUserById(userId);
            return new
            {
                user.UserName,
                user.FullName,
                user.Email,
            };
        }
    }
}