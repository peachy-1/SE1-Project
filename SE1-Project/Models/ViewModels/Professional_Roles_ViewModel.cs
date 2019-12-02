using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models.ViewModels
{
    public class Professional_Roles_ViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public decimal Rating { get; set; }

        //Table fields for filtering
        public string nameString { get; set; }
        public string cityString { get; set; }
        public string stateString { get; set; }
        public string professionString { get; set; }
        public string sortBy { get; set; }
        public List<Areas.Identity.Data.SE1_ProjectUser> Professionals { get; set; }
    }
}
