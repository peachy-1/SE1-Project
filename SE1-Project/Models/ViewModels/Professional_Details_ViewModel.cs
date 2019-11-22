using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models.ViewModels
{
    public class Professional_Details_ViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Profession { get; set; }
        public string Rate { get; set; }
        public string Company { get; set; }
        public decimal Rating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
