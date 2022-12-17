using System;
using CRUDAPP.Models.Domain;

namespace CRUDAPP.Models.Movies
{
	public class ShowMovieViewModel : IMovie
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Production { get; set; }
        public string Genere { get; set; }
        public int Rating { get; set; }

        public Director Director { get; set; }
    }
}

