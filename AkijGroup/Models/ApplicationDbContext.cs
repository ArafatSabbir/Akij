using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AkijGroup.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options ) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColdDrinks>()
                .HasIndex(c => c.ColdDrinksName)
                .IsUnique();
        }

        public DbSet<ColdDrinks> ColdDrinks { get; set; }

    }
}
