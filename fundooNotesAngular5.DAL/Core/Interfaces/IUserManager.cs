using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationApp.DAL.Models;

namespace UserRegistrationApp.DAL.Core.Interfaces
{
    public interface IUserManager
    {
        Tuple<bool, string[]> CreateUserAsync(ApplicationUser model, string password) ;
        Task<ApplicationUser> FindUserByEmail(string email);


    }
}
