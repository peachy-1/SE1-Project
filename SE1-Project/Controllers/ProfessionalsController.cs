using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1_Project.Migrations.SE1_P;
using SE1_Project.Migrations.SE1_Project_;
using SE1_Project.Models;
using SE1_Project.Models.ViewModels;

namespace SE1_Project.Controllers
{
    public class ProfessionalsController : Controller
    {
        private readonly SE1_ProjectContext _context;
        private readonly SE1_PContext _dbcontext;
        //private readonly SE1_Project_Context _dbcontext;
        //private ApplicationDbContext

        public ProfessionalsController(SE1_ProjectContext context, SE1_PContext dbcontext)
        {
            _context = context;
            _dbcontext = dbcontext;
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

        [HttpGet]
        public ActionResult Professional_Roles(string nameString, string cityString, string stateString, string professionalProfession, decimal rating= 0)
        {
            //List<Professional> professionals = new List<Professional>();
            //Professional professional;
            
            var prof = (from user in _dbcontext.Users
                        join r in _dbcontext.UserRoles on user.Id equals r.UserId
                        where r.RoleId == "3"
                        select new
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            City = user.City,
                            State = user.State,
                            Profession = user.Profession,
                            Email = user.Email

                        }).ToList();
            foreach(var user in prof)
            {
                checkAverageRating(user.UserId);
            }
            //prof = prof.Where(r => r.a)
                //professionals = professionals.Where(r => r.averageRating >= rating);

            /*List<Professional_Roles_ViewModel> viewModel = new List<Professional_Roles_ViewModel>();
            string oneStarQuery, twoStarQuery, threeStarQuery, fourStarQuery, fiveStarQuery, totalReviewsQuery;
            int oneStarReviews, twoStarReviews, threeStarReviews, fourStarReviews, fiveStarReviews, totalReviews;
            decimal averageRating;
            SqlConnection connection;
            SqlParameter param = new SqlParameter();
            SqlCommand command;
            foreach (var user in prof)
            {
                oneStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 1 AND professionalEmail=@id";

                twoStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 2 AND professionalEmail=@id";

                threeStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 3 AND professionalEmail=@id";

                fourStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 4 AND professionalEmail=@id";

                fiveStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 5 AND professionalEmail=@id";

                totalReviewsQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE professionalEmail=@id";

                using (connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    param.ParameterName = "@id";
                    param.Value = user.UserId;
                    using (command = new SqlCommand(oneStarQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        oneStarReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }
                    using (command = new SqlCommand(twoStarQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        twoStarReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }
                    using (command = new SqlCommand(threeStarQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        threeStarReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }
                    using (command = new SqlCommand(fourStarQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        fourStarReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }
                    using (command = new SqlCommand(fiveStarQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        fiveStarReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }
                    using (command = new SqlCommand(totalReviewsQuery, connection))
                    {
                        command.Parameters.Add(param);
                        connection.Open();
                        totalReviews = (int)command.ExecuteScalar();
                        connection.Close();
                        command.Parameters.Clear();
                    }

                    if (totalReviews > 0)
                    {
                        averageRating = (decimal)(5 * fiveStarReviews + 4 * fourStarReviews + 3 * threeStarReviews + 2 * twoStarReviews + 1 * oneStarReviews) / totalReviews;
                    }
                    else
                    {
                        averageRating = 0;
                    }
                    viewModel.Add(new Professional_Roles_ViewModel()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        City = user.City,
                        State = user.State,
                        Profession = user.Profession,
                        Email = user.Email,
                        Rating = averageRating
                    });
                }
            }*/

            var professionals = (from user in _dbcontext.Users
                                 join r in _dbcontext.UserRoles on user.Id equals r.UserId
                                 where r.RoleId == "3"                                 
                                 select new
                                 {
                                     UserId = user.Id,
                                     Username = user.UserName,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     City = user.City,
                                     State = user.State,
                                     Profession = user.Profession,
                                     Email = user.Email,
                                     Rating = user.avgRating
                            

                                 }).ToList().Select(vp => new Professional_Roles_ViewModel()
                                 {
                                     UserId = vp.UserId,
                                     Username = vp.Username,
                                     FirstName = vp.FirstName,
                                     LastName = vp.LastName,
                                     City = vp.City,
                                     State = vp.State,
                                     Profession = vp.Profession,
                                     Email = vp.Email,
                                     Rating = vp.Rating
                                     
                                 });

            //IQueryable<string> p
            var p = from pro in _dbcontext.Users
                    join r in _dbcontext.UserRoles on pro.Id equals r.UserId
                    where r.RoleId == "3"
                    select pro;

            if (!String.IsNullOrEmpty(nameString))
            {
                p = p.Where(s => s.FirstName.Contains(nameString) || s.LastName.Contains(nameString)
                || (nameString.Contains(s.FirstName) && nameString.Contains(s.LastName)));
            }

            if (!String.IsNullOrEmpty(cityString))
            {
                p = p.Where(s => s.City.Contains(cityString));
            }

            if (!String.IsNullOrEmpty(stateString))
            {
                p = p.Where(s => s.State.Contains(stateString));
            }

            p = p.Where(r => r.avgRating >= rating);

            if (!string.IsNullOrEmpty(professionalProfession))
            {
                p = p.Where(x => x.Profession == professionalProfession);
            }

            var profRoles = new Professional_Roles_ViewModel
            {
                Professionals = p.ToList()
            };


            //return View(viewModel.ToList());
            //return View(p);
            return View(profRoles);
        }

        public ActionResult getSpecificUserDetails(string id, string uId)
        {
            List<Review> reviews = new List<Review>();
            string queryString = "SELECT reviewId, reviewText, reviewerName, rating, professionalEmail FROM dbo.UserReviews WHERE professionalEmail=@id";
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(param);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Review() { ID = (int)reader[0], reviewText = reader[1].ToString(), reviewerName = reader[2].ToString(), rating = (int)reader[3], professionalId = reader[4].ToString() });
                    }
                }
                connection.Close();
            }

            string oneStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 1 AND professionalEmail=@id";
            int oneStarQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(oneStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    oneStarQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            string twoStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 2 AND professionalEmail=@id";
            int twoStarQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(twoStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    twoStarQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            string threeStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 3 AND professionalEmail=@id";
            int threeStarQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(threeStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    threeStarQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            string fourStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 4 AND professionalEmail=@id";
            int fourStarQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(fourStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    fourStarQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            string fiveStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 5 AND professionalEmail=@id";
            int fiveStarQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(fiveStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    fiveStarQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            string totalRating = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE professionalEmail=@id";
            int totalQueries;
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = uId;
                using (SqlCommand command = new SqlCommand(totalRating, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    totalQueries = (int)command.ExecuteScalar();
                }
                connection.Close();
            }

            decimal averageRating = 0;

            if (totalQueries > 0)
            {
                averageRating = (decimal)(5 * fiveStarQueries + 4 * fourStarQueries + 3 * threeStarQueries + 2 * twoStarQueries + 1 * oneStarQueries) / totalQueries;
            }


            var pro = _dbcontext.Users.Where(p => p.Email == id).FirstOrDefault();
            if (pro == null)
            {
                return new NotFoundResult();
            }
            Professional_Details_ViewModel vm = new Professional_Details_ViewModel()
            {
                Email = pro.Email,
                FirstName = pro.FirstName,
                LastName = pro.LastName,
                Address = pro.Address,
                City = pro.City,
                State = pro.State,
                Profession = pro.Profession,
                Rate = pro.Rate,
                Company = pro.Company,
                Reviews = reviews,
                Rating = averageRating
            };
            ViewBag.vm = vm;
            return View(vm);
        }

        [HttpPost]
        public ActionResult addReview(string reviewer, string review, int rating, string email)
        {
            string insertQuery = "INSERT INTO dbo.UserReviews(reviewerName,reviewText,rating,professionalEmail) VALUES(@reviewer,@review,@rating,@email)";
            using (SqlConnection connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                SqlParameter param1 = new SqlParameter();
                SqlParameter param2 = new SqlParameter();
                SqlParameter param3 = new SqlParameter();
                SqlParameter param4 = new SqlParameter();

                param1.ParameterName = "@reviewer";
                param2.ParameterName = "@review";
                param3.ParameterName = "@rating";
                param4.ParameterName = "@email";

                param1.Value = reviewer;
                param2.Value = review;
                param3.Value = rating;
                param4.Value = email;

                connection.Open();

                SqlCommand command = new SqlCommand(insertQuery, connection);

                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);

                command.ExecuteNonQuery();

                connection.Close();

            }
            return View("Professional_Roles");
        }

        public void checkAverageRating(string professionalEmail)
        {
            string oneStarQuery, twoStarQuery, threeStarQuery, fourStarQuery, fiveStarQuery, totalReviewsQuery;
            int oneStarReviews, twoStarReviews, threeStarReviews, fourStarReviews, fiveStarReviews, totalReviews;
            decimal averageRating;
            SqlConnection connection;
            SqlParameter param = new SqlParameter();
            SqlCommand command;
            oneStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 1 AND professionalEmail=@id";

            twoStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 2 AND professionalEmail=@id";

            threeStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 3 AND professionalEmail=@id";

            fourStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 4 AND professionalEmail=@id";

            fiveStarQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE rating = 5 AND professionalEmail=@id";

            totalReviewsQuery = "SELECT COUNT(*) FROM [dbo].[UserReviews] WHERE professionalEmail=@id";

            using (connection = new SqlConnection("Server=tcp:se1-ratemyprofessional.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=rmpadmin;Password=TeamOne1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                param.ParameterName = "@id";
                param.Value = professionalEmail;
                using (command = new SqlCommand(oneStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    oneStarReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                using (command = new SqlCommand(twoStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    twoStarReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                using (command = new SqlCommand(threeStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    threeStarReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                using (command = new SqlCommand(fourStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    fourStarReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                using (command = new SqlCommand(fiveStarQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    fiveStarReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                using (command = new SqlCommand(totalReviewsQuery, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    totalReviews = (int)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }

                if (totalReviews > 0)
                {
                    averageRating = (decimal)(5 * fiveStarReviews + 4 * fourStarReviews + 3 * threeStarReviews + 2 * twoStarReviews + 1 * oneStarReviews) / totalReviews;
                }
                else
                {
                    averageRating = 0;
                }

                decimal checkRating;
                string ratingCheck = "SELECT avgRating FROM [dbo].[AspNetUsers] WHERE Id=@id";
                using (command = new SqlCommand(ratingCheck, connection))
                {
                    command.Parameters.Add(param);
                    connection.Open();
                    checkRating = (decimal)command.ExecuteScalar();
                    connection.Close();
                    command.Parameters.Clear();
                }
                if(averageRating != checkRating)
                {
                    string setAvgRating = "UPDATE [dbo].[AspNetUsers] SET avgRating=@rating WHERE Id=@id";
                    SqlParameter ratingParam = new SqlParameter();
                    ratingParam.ParameterName = "@rating";
                    ratingParam.Value = averageRating;
                    using (command = new SqlCommand(setAvgRating, connection))
                    {
                        command.Parameters.Add(param);
                        command.Parameters.Add(ratingParam);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                }

            }
        
        }
    }
}
