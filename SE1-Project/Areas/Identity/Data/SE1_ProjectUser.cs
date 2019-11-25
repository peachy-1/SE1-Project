using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SE1_Project.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SE1_ProjectUser class
    public class SE1_ProjectUser : IdentityUser
    {
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string State { get; set; }
        [PersonalData]
        public string Profession { get; set; }
        [PersonalData]
        public string Rate { get; set; }
        [PersonalData]
        public string Company { get; set; }
        public decimal avgRating { get; set; }
    }
}
