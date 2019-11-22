using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models
{
    public class Review
    {
        public int ID { get; set; }


        public DateTime InteractionDate { get; set; }
        public int rating { get; set; }
        public string reviewText { get; set; }

        public string reviewerName { get; set; }
        public string professionalId { get; set; }
    }
}
