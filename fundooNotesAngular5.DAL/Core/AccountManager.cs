using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationApp.DAL.Core.Interfaces;
using UserRegistrationApp.DAL.Data;
using UserRegistrationApp.DAL.Models;

namespace UserRegistrationApp.DAL.Core
{


    public class AccountManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        //public AccountManager(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager,ApplicationDbContext context)
        //{
        //    _userManager = usermanager;
        //    _signInManager = signinmanager;
        //    _context = context;
        //}

        public AccountManager()
        {

        }

        public AccountManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tuple<bool, string[]> CreateUserAsync(ApplicationUser model, string password)
        {
            //ApplicationDbContext _context = new ApplicationDbContext();
            try
            {

                _context.Users.Add(model);
                int i = _context.SaveChanges();


                //var result = await _userManager.CreateAsync(model, model.password);
                //if (result.Succeeded)
                //{
                //    return Tuple.Create(true, new string[] { });
                //}
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
          
            return Tuple.Create(true, new string[] { });
        }

        public async Task<ApplicationUser> FindUserByEmail(string  email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return user;
        }


    }
}
