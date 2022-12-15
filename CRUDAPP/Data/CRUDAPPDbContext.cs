using System;
using CRUDAPP.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPP.Data
{
    public class CRUDAPPDbContext : DbContext
    {

        public CRUDAPPDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}

