using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using DxWand.UI.Enums;
using DxWand.UI.Models;
using Microsoft.AspNetCore.Http;

namespace DxWand.UI.Services
{
    public class ApplicationService : IApplicationService
    {
        private ISession session;
        public ApplicationService(IHttpContextAccessor httpContextAccessor)
        {
            session = httpContextAccessor.HttpContext.Session;
        }
        private readonly string TOKEN_NAME = "token";
        
        public void ClearToken()
        {
            session.Remove(TOKEN_NAME);
        }

        public string GetToken()
        {
            return session.GetString(TOKEN_NAME);
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(session.GetString(TOKEN_NAME));
        }

        public ApplicationUser UserInfo()
        {
            var token = GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var calims = jwtSecurityToken.Claims;

            var user = new ApplicationUser {
                Email = calims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                IsAdmin = calims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Admin",
                BirthDate = DateTime.Parse(calims.FirstOrDefault(x => x.Type == "BirthDate").Value),
                Gender = (GenderEnum) Convert.ToInt32(calims.FirstOrDefault(x => x.Type == "Gender").Value) 
            };

            return user;
        }

        public void SetToken(string token)
        {
            session.SetString(TOKEN_NAME, token);
        }
    }
}
