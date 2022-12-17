using System;
namespace CRUDAPP.Models
{
    public interface IMovie
    {
        public string Title { get; set; }
        public DateTime Production { get; set; }
        public string Genere { get; set; }
        public int Rating { get; set; }
    }
}

