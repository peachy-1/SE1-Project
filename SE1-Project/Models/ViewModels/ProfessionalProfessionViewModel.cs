using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE1_Project.Models.ViewModels
{
    public class ProfessionalProfessionViewModel
    {
        public List<Professional> Professionals { get; set; }
        public SelectList professions { get; set; }
        public string professionalProfession { get; set; }
        public string searchString { get; set; }
    }
}
