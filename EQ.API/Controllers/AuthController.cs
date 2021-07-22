using EQ.API.Models;
using EQ.BLL.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using EQ.Models.Models.Configuration;
using Microsoft.Extensions.Configuration;
using EQ.Constants;
using EQ.Helpers.Tokens;

namespace EQ.API.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return this.Ok("SUKA");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login != null)
            {
                var authenticationService = this.serviceProvider.GetService<IAuthenticationService>();
                var userInRole = authenticationService.SignIn(login.Email, login.Password);

                if (userInRole != null)
                {
                    var jwtSettings = new JwtSettings();
                    var configuration = this.serviceProvider.GetService<IConfiguration>();
                    configuration.GetSection(ConfigurationConstants.JwtSectionName).Bind(jwtSettings);

                    var token = TokenHelper.CreateToken(jwtSettings.SigningKey, jwtSettings.Issuer, jwtSettings.Audience, userInRole.Email, userInRole.UserRole);

                    return this.Ok(new AccessTokenModel(token));
                }
            }

            return this.BadRequest();
        }
    }
}
