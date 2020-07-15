using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductApp.BLL.Constants;
using ProductApp.BLL.Models;
using ProductApp.DAL.Entities;

namespace ProductApp.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppSettings appSettings;

        public AuthService(UserManager<AppUser> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        public async Task<IdentityResult> CreateUser(RegisterModel model)
        {
            var appUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            return await userManager.CreateAsync(appUser, model.Password);
        }

        public async Task<AppUser> FindUserByName(LoginModel model)
        {
            return await userManager.FindByNameAsync(model.UserName);
        }

        public async Task<AppUser> FindUserById(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UserExists(LoginModel model)
        {
            var user = await FindUserByName(model);
            return user != null;
        }
        public async Task<bool> IsLoginValid(LoginModel model)
        {
            var user = await FindUserByName(model);
            return await userManager.CheckPasswordAsync(user, model.Password);
        }

        public async Task<string> CreateJwtToken(LoginModel model)
        {
            var user = await FindUserByName(model);
            if (await UserExists(model) && await IsLoginValid(model))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSettings.Jwt_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
                throw new ArgumentException();
        }
    }
}