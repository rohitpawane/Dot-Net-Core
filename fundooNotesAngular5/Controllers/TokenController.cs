using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserRegistrationApp.DAL.Data;
using UserRegistrationApp.DAL.Models;

namespace fundooNotesAngular5.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private ApplicationDbContext _context;
        public TokenController(IConfiguration config,ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user.Count >0)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString,user });
            }

            return response;
        }

        private string BuildToken(List<ApplicationUser> user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<ApplicationUser> Authenticate(LoginModel login)
        {
            //UserModel user = null;

            var list = new List<ApplicationUser>();

            var user = from a in _context.Users
                       where a.Email == login.Username
                       select a;

            foreach (ApplicationUser item in user)
            {
                list.Add(item);
            }

            //if (login.Username == "mario" && login.Password == "secret")
            //{
            //    user = new UserModel { Name = "Mario Rossi", Email = "mario.rossi@domain.com" };
            //}
            return list;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
        }

    }
}