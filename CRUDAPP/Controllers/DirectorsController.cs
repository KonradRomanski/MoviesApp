using System;
using System.Collections.Generic;
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
    }
}

