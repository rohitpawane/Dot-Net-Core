using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp.DAL.Core;
using UserRegistrationApp.DAL.Data;
using UserRegistrationApp.DAL.Models;

namespace fundooNotesAngular5.Controllers
{
    public class RegisterController:Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //public RegisterController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
        [HttpPost]
        public async Task<ApplicationUser> RegisterUser([FromBody]ApplicationUser model)
        {
            AccountManager account = new AccountManager();
            //await account.CreateUserAsync(model,model.password);
            return model;
        }
    }
}
