 using System;
using System.IO;
using CRUDAPP.Data;
using CRUDAPP.Models;
using CRUDAPP.Models.Domain;
using CRUDAPP.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CRUDAPP.Controllers
{
    public class MoviesController : Controller
    {

        private readonly CRUDAPPDbContext crudAppDbContext;

        public MoviesController(CRUDAPPDbContext crudAppDbContext)
        {
            this.crudAppDbContext = crudAppDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = from m in crudAppDbContext.Movies
                         join d in crudAppDbContext.Directors on m.DirectorId equals d.Id
                         select new { Movie = m, Director = d };
            var viewModel = movies.Select(m => new ShowMovieViewModel
            {
                Id = m.Movie.Id,
                Title = m.Movie.Title,
                Production = m.Movie.Production,
                Genere = m.Movie.Genere,
                Rating = m.Movie.Rating,
                Director = m.Director
            }).ToList();

            return View(viewModel);
        }

        public CRUDAPPDbContext CrudAppDbContext { get; set; }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var directors = await crudAppDbContext.Directors.ToListAsync();
            var viewModel = new AddMovieViewModel { Directors = directors };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel AddMovieReques)
        {
            var movie = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = AddMovieReques.Title,
                DirectorId = AddMovieReques.SelectedDirectorId,
                Production = AddMovieReques.Production,
                Genere = AddMovieReques.Genere,
                Rating = AddMovieReques.Rating
            };

            await crudAppDbContext.Movies.AddAsync(movie);
            await crudAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var movie = await crudAppDbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie != null)
            {
                var viewMovie = new UpdateMovieViewModel()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    DirectorId = movie.DirectorId,
                    Production = movie.Production,
                    Genere = movie.Genere,
                    Rating = movie.Rating,
                    Directors = await crudAppDbContext.Directors.ToListAsync(),
                    SelectedDirectorId = movie.DirectorId

                };
                return await Task.Run(() => View("View", viewMovie));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateMovieViewModel model)
        {
            var movie = await crudAppDbContext.Movies.FindAsync(model.Id);

            if (movie != null)
            {
                movie.Id = model.Id;
                movie.Title = model.Title;
                movie.DirectorId = model.SelectedDirectorId;
                movie.Production = model.Production;
                movie.Genere = model.Genere;
                movie.Rating = model.Rating;

                await crudAppDbContext.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateMovieViewModel model)
        {
            var movie = await crudAppDbContext.Movies.FindAsync(model.Id);

            if (movie != null)
            {
                crudAppDbContext.Movies.Remove(movie);
                await crudAppDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}

