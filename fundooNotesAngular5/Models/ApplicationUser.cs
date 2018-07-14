using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace fundooNotesAngular5.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
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
