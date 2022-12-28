using System;
using CRUDAPP.Models.Directors;
using CRUDAPP.Models.Domain;

namespace CRUDAPP.Models.Directors
{
    public class AddDirectorViewModel : IDirector
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public float Rating { get; set; }
        public string? Link { get; set; }
    }
}

