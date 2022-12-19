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
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId);
        }
    }
}

