using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Models
{
    public partial class FactbookContext : DbContext
    {
        public FactbookContext() { }

        public FactbookContext(DbContextOptions<FactbookContext> options)
            : base(options) { }

        public virtual DbSet<ArmedForce> ArmedForce { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\AndyS\\OneDrive\\Documents\\Visual Studio 2017\\Projects\\MvcFactbook\\MvcFactbook\\Database\\Factbook.mdf; Integrated Security = True;Connect Timeout=30");
                //optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\dev\\MvcPlaces\\MvcPlaces\\Databases\\Travel.mdf; Integrated Security = True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArmedForce>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("((X))");

                entity.Property(e => e.IsActive)
                    .IsRequired();
            });
        }
    }
}
