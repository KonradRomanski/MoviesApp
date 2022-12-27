using System;
using CRUDAPP.Models.Domain;

namespace CRUDAPP.Models.Movies
{
	public class ShowMovieViewModel : IMovie
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Production { get; set; }
        public EMovieType Types { get; set; }
        public string TypesString { get; set; }
        public int Rating { get; set; }

        
        public Director Director { get; set; }
    }
}

