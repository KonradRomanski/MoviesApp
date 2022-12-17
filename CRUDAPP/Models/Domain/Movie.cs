using System;
namespace CRUDAPP.Models.Domain
{
    public class Movie : IMovie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Production { get; set; }
        public string Genere { get; set; }
        public int Rating { get; set; }

        public Guid DirectorId { get; set; }
        public Director Director { get; set; }
    }
}

