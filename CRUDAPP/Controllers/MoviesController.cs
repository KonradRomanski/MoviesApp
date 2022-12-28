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
        public async Task<IActionResult> Index(string sortOrder, string searchTerm)
        {
            // Set the default sort order to title ascending
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "title_asc" : sortOrder;

            var movies = from m in crudAppDbContext.Movies.AsEnumerable()
                         join d in crudAppDbContext.Directors.AsEnumerable() on m.DirectorId equals d.Id
                         select new { Movie = m, Director = d };

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                movies = movies.Where(m => m.Movie.Title.ToLower().Contains(searchTerm)
                                        || m.Director.Name.ToLower().Contains(searchTerm)
                                        || m.Director.Surname.ToLower().Contains(searchTerm)
                                        || m.Movie.Types.ToString().ToLower().IndexOf(searchTerm) >= 0
                                        || m.Movie.Production.ToString("ddd dd MMMM yyyy").ToLower().Contains(searchTerm)
                                        || m.Movie.Rating.ToString().ToLower().Contains(searchTerm));
            }



            switch (sortOrder)
            {
                case "title_asc":
                    movies = movies.OrderBy(m => m.Movie.Title);
                    break;
                case "title_desc":
                    movies = movies.OrderByDescending(m => m.Movie.Title);
                    break;
                case "director_asc":
                    movies = movies.OrderBy(m => m.Director.Name);
                    break;
                case "director_desc":
                    movies = movies.OrderByDescending(m => m.Director.Name);
                    break;
                case "production_asc":
                    movies = movies.OrderBy(m => m.Movie.Production);
                    break;
                case "production_desc":
                    movies = movies.OrderByDescending(m => m.Movie.Production);
                    break;
                case "type_asc":
                    movies = movies.OrderBy(m => m.Movie.Types);
                    break;
                case "type_desc":
                    movies = movies.OrderByDescending(m => m.Movie.Types);
                    break;
                case "rating_asc":
                    movies = movies.OrderBy(m => m.Movie.Rating);
                    break;
                case "rating_desc":
                    movies = movies.OrderByDescending(m => m.Movie.Rating);
                    break;
            }

            var viewModel = movies.Select(m => new ShowMovieViewModel
            {
                Id = m.Movie.Id,
                Title = m.Movie.Title,
                Production = m.Movie.Production,
                Types = m.Movie.Types,
                Rating = m.Movie.Rating,
                Director = m.Director,
                Link = m.Movie.Link
            }).ToList();
            ViewData["currentSortOrder"] = sortOrder;
            ViewData["currentSearchTerm"] = searchTerm;

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
            var director = await crudAppDbContext.Directors.FindAsync(AddMovieReques.SelectedDirectorId);
            if (director == null)
            {
                return NotFound("Director not found");
            }
            var movie = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = AddMovieReques.Title,
                DirectorId = AddMovieReques.SelectedDirectorId,
                Production = AddMovieReques.Production,
                Types = AddMovieReques.Types,
                Rating = AddMovieReques.Rating,
                Link = AddMovieReques.Link
            };
            director.Movies = director.Movies ?? new List<Movie>();
            //director.Movies.Add(movie);


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
                    Types = movie.Types,
                    Rating = movie.Rating,
                    Directors = await crudAppDbContext.Directors.ToListAsync(),
                    SelectedDirectorId = movie.DirectorId,
                    Link = movie.Link

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
                movie.Types = model.Types;
                movie.Rating = model.Rating;
                movie.Link = model.Link;

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

