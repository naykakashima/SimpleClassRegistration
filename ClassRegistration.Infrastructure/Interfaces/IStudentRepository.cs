using ClassRegistration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Infrastructure.Interfaces
{
    internal interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task<Student?> FindStudentByNameAsync(string studentName);
    }
}
