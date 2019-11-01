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
        public SelectList Professions { get; set; }
        public string ProfessionalProfession { get; set; }
        public string NameString { get; set; }
        public string CityString { get; set; }
        public string StateString { get; set; }
        public decimal Rating { get; set; }
    }
}
