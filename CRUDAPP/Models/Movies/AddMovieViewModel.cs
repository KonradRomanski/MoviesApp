using System;
using CRUDAPP.Models.Domain;

namespace CRUDAPP.Models
{
    public class AddMovieViewModel : IMovie
    {
        public string Title { get; set; }
        public Guid DirectorId { get; set; }
        public DateTime Production { get; set; }
        public string Genere { get; set; }
        public int Rating { get; set; }

        public List<Director> Directors { get; set; }
        public Guid SelectedDirectorId { get; set; }
    }
}

