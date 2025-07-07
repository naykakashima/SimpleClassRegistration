using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassRegistration.Domain.Models;

using Microsoft.EntityFrameworkCore;


namespace ClassRegistration.Infrastructure.Database
{
    public class ClassDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        public ClassDbContext(DbContextOptions<ClassDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(c => c.EnrolledStudents)
                .WithMany();

            modelBuilder.Entity<Student>()
                .HasMany(s => s.EnrolledClasses)
                .WithMany(c => c.EnrolledStudents);
        }


    }

}
