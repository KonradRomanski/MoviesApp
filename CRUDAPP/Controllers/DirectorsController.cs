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
        public async Task<IActionResult> Index()
        {
            var directors = await crudAppDbContext.Directors.ToListAsync();
            return View(directors);
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

