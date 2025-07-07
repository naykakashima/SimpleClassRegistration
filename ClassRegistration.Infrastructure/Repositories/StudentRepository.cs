using ClassRegistration.Domain.Models;
using ClassRegistration.Infrastructure.Database;
using ClassRegistration.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly ClassDbContext _context;

        public StudentRepository(ClassDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student?> FindStudentByNameAsync(string studentName)
        {
            return await _context.Set<Student>().FirstOrDefaultAsync(s => s.StudentName.ToLower() == studentName.ToLower());
        }

    }
}
