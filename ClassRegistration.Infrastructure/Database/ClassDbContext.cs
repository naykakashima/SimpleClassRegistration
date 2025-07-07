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
            modelBuilder.Entity<Class>().ToTable("Classes");
            modelBuilder.Entity<Class>().HasKey(c => c.ClassID);

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);

             modelBuilder.Entity<Class>()
                .HasMany(c => c.EnrolledStudents)
                .WithMany(s => s.EnrolledClasses);
            }


    }

}
