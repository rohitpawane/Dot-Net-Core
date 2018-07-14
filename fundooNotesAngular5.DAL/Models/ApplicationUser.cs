
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistrationApp.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string aadharno { get; set; }
        public string countrycode { get; set; }
        public string mobileno { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string postalcode { get; set; }
        public string password { get; set; }

     
    }
}
