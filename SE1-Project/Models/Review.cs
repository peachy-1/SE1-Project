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

        [DataType(DataType.Date)]
        public DateTime InteractionDate { get; set; }
        public int rating { get; set; }
        public string reviewText { get; set; }

        [Required]
        public virtual Professional Professional { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
    }
}
