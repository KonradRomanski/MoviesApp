using System;
using CRUDAPP.Models.Directors;

namespace CRUDAPP.Models.Domain
{
    public class Director : IDirector
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public float Rating { get; set; }
        public ICollection<Movie> Movies { get; set; }
        public string? Link { get; set; }
    }
}

