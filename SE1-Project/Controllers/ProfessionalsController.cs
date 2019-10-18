using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1_Project.Models;
using SE1_Project.Models.ViewModels;

namespace SE1_Project.Controllers
{
    public class ProfessionalsController : Controller
    {
        private readonly SE1_ProjectContext _context;

        public ProfessionalsController(SE1_ProjectContext context)
        {
            _context = context;
        }

        // GET: Professionals
        public async Task<IActionResult> Index(string professionalprofession, string nameString, string cityString, string stateString, decimal rating = 0)
        {
            IQueryable<string> professionQuery = from p in _context.Professional
                                                 orderby p.profession
                                                 select p.profession;

            var professionals = from p in _context.Professional
                                select p;

            if (!String.IsNullOrEmpty(nameString))
            {
                professionals = professionals.Where(s => s.fName.Contains(nameString) || s.lName.Contains(nameString) 
                || (nameString.Contains(s.fName) && nameString.Contains(s.lName)));
            }

            if (!String.IsNullOrEmpty(cityString))
            {
                professionals = professionals.Where(s => s.city.Contains(cityString));
            }

            if (!String.IsNullOrEmpty(stateString))
            {
                professionals = professionals.Where(s => s.state.Contains(stateString));
            }

            professionals = professionals.Where(r => r.averageRating >= rating);

            if (!string.IsNullOrEmpty(professionalprofession))
            {
                professionals = professionals.Where(x => x.profession == professionalprofession);
            }

            

            var professionalProfessionVM = new ProfessionalProfessionViewModel
            {
                Professions = new SelectList(await professionQuery.Distinct().ToListAsync()),
                Professionals = await professionals.ToListAsync()
            };
            return View(professionalProfessionVM);
            //return View(await professionals.ToListAsync());
            //return View(await _context.Professional.ToListAsync());
        }

        // GET: Professionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professional
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // GET: Professionals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,fName,lName,streetAddress,city,state,phoneNumber,email,profession,rate,averageRating,company,image")] Professional professional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professional);
        }

        // GET: Professionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professional.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }
            return View(professional);
        }

        // POST: Professionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,fName,lName,streetAddress,city,state,phoneNumber,email,profession,rate,averageRating,company,image")] Professional professional)
        {
            if (id != professional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalExists(professional.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(professional);
        }

        // GET: Professionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professional
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // POST: Professionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professional = await _context.Professional.FindAsync(id);
            _context.Professional.Remove(professional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionalExists(int id)
        {
            return _context.Professional.Any(e => e.Id == id);
        }
    }
}
