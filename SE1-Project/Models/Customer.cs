using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string fName { get; set; }
        public string lName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}
