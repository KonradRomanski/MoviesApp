using System;
namespace CRUDAPP.Models
{
    public class UpdateMovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime Production { get; set; }
        public string Genere { get; set; }
        public int Rating { get; set; }
    }
}

