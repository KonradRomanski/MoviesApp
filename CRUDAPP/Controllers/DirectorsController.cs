using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPP.Data;
using CRUDAPP.Models;
using CRUDAPP.Models.Directors;
using CRUDAPP.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDAPP.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly CRUDAPPDbContext crudAppDbContext;

        public DirectorsController(CRUDAPPDbContext crudAppDbContext)
        {
            this.crudAppDbContext = crudAppDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchTerm)
        {
            var directors = from d in crudAppDbContext.Directors.AsEnumerable() select new { Director = d};

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                directors = directors.Where(m => m.Director.Name.ToLower().Contains(searchTerm)
                                                || m.Director.Surname.ToLower().Contains(searchTerm)
                                                || m.Director.DateOfBirth.ToString("ddd dd MMMM yyyy").ToLower().Contains(searchTerm)
                                                || (m.Director.DateOfDeath != null && m.Director.DateOfDeath.ToString().ToLower().Contains(searchTerm))
                                                || m.Director.Rating.ToString().ToLower().Contains(searchTerm));

            }

            switch (sortOrder)
            {
                case "name_asc":
                    directors = directors.OrderBy(d => d.Director.Name);
                    break;
                case "name_desc":
                    directors = directors.OrderByDescending(d => d.Director.Name);
                    break;
                case "surname_asc":
                    directors = directors.OrderBy(d => d.Director.Surname);
                    break;
                case "surname_desc":
                    directors = directors.OrderByDescending(d => d.Director.Surname);
                    break;
                case "dateofbirth_asc":
                    directors = directors.OrderBy(d => d.Director.DateOfBirth);
                    break;
                case "dateofbirth_desc":
                    directors = directors.OrderByDescending(d => d.Director.DateOfBirth);
                    break;
                case "dateofdeath_asc":
                    directors = directors.OrderBy(d => d.Director.DateOfDeath);
                    break;
                case "dateofdeath_desc":
                    directors = directors.OrderByDescending(d => d.Director.DateOfDeath);
                    break;
                case "rating_asc":
                    directors = directors.OrderBy(d => d.Director.Rating);
                    break;
                case "rating_desc":
                    directors = directors.OrderByDescending(d => d.Director.Rating);
                    break;
            }

            var viewModel = directors.Select(d => d.Director).ToList();


            ViewData["currentSortOrder"] = sortOrder;
            ViewData["currentSearchTerm"] = searchTerm;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDirectorViewModel addDirectorViewModel)
        {
            var director = new Director()
            {
                Id = Guid.NewGuid(),
                Name = addDirectorViewModel.Name,
                Surname = addDirectorViewModel.Surname,
                DateOfBirth = addDirectorViewModel.DateOfBirth,
                DateOfDeath = addDirectorViewModel.DateOfDeath,
                Rating = addDirectorViewModel.Rating
            };

            await crudAppDbContext.Directors.AddAsync(director);
            await crudAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var director = await crudAppDbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

            if (director != null)
            {
                var viewDirector = new UpdateDirectorViewModel()
                {
                    Id = director.Id,
                    Name = director.Name,
                    Surname = director.Surname,
                    DateOfBirth = director.DateOfBirth,
                    DateOfDeath = director.DateOfDeath,
                    Rating = director.Rating,
                    Movies = director.Movies
                };
                Console.WriteLine(viewDirector.Movies);
                return await Task.Run(() => View("View", viewDirector));
            }
            else
            {
                return RedirectToAction("View");
            }
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateDirectorViewModel model)
        {
            var director = await crudAppDbContext.Directors.FindAsync(model.Id);

            if (director != null)
            {
                director.Id = model.Id;
                director.Name = model.Name;
                director.Surname = model.Surname;
                director.DateOfBirth = model.DateOfBirth;
                director.DateOfDeath = model.DateOfDeath;
                director.Rating = model.Rating;

                await crudAppDbContext.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }
    }
}

