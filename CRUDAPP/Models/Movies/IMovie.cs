using System;
using CRUDAPP.Models.Movies;

namespace CRUDAPP.Models
{
    public interface IMovie
    {
        public string Title { get; set; }
        public DateTime Production { get; set; }
        public EMovieType Types { get; set; }
        public int Rating { get; set; }
    }
}

