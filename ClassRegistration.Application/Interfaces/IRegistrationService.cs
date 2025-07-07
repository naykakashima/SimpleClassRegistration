using ClassRegistration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Application.Interfaces
{
    public interface IRegistrationService
    {
        Task<(bool Success, string Message)> AddClass(string ClassName, string ClassType, int MaxOccupancy);
        Task<(bool Success, string Message)> AddStudentToClass(string ClassName, string StudentName);
        Task<(bool Success, string Message)> RemoveStudentFromClass(string ClassName, string StudentName);
        Task<List<Class>> StudentEnrolledInClasses(string StudentName);
        Task<(bool Success, string Message)> AddStudent(string StudentName);
        Task<IEnumerable<Class>> GetAvailableClasses();

    }
}
