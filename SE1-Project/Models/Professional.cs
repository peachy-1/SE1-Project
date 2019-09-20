using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models
{
    public class Professional
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string fName { get; set; }

        [Display(Name = "Last Name")]
        public string lName { get; set; }

        [Display(Name = "Address")]
        public string streetAddress { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string phoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Profession")]
        public string profession { get; set; }

        [Display(Name = "Rate")]
        public Decimal rate { get; set; }

        [Display(Name = "Rating")]
        public Decimal averageRating { get; set; }

        [Display(Name = "Company")]
        public string company { get; set; }
        public string image { get; set; }
    }   
}
