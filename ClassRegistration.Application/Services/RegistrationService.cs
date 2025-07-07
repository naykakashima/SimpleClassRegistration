using ClassRegistration.Infrastructure.Repositories;
using ClassRegistration.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClassRegistration.Domain.Models;

namespace ClassRegistration.Application.Services
{
    public class RegistrationService
    {
        private IClassRepository _classRepository;
        private IStudentRepository _studentRepository;
        

        public RegistrationService(IClassRepository repository, IStudentRepository studentRepository)
        {
            _classRepository = repository;
            _studentRepository = studentRepository;
        }

        public async Task<(bool Success, string Message)>AddClass(string ClassName, string ClassType, int MaxOccupancy)
        {
            var allClasses = await _classRepository.GetClassesAsync();
            if (allClasses.Any(b => b.ClassName.Equals(ClassName, StringComparison.OrdinalIgnoreCase)))
                return (false, "Class Name Is Already Registered!");

            await _classRepository.AddAsync(new Class(ClassName, ClassType, MaxOccupancy));
            return (true, "Class Successfully Added");

        }

        public async Task<(bool Success, string Message)> AddStudentToClass(string ClassName,  string StudentName)
        {
            var @class = await _classRepository.FindClassByTitle(ClassName);
            var student = await _studentRepository.FindStudentByNameAsync(StudentName);

            if (student == null) {
                return (false, "Student could not be found!");
            }

            if (@class == null)
            {
                return (false, "Class Doesn't Exist!");
            }

            if (@class.IsClassFull == true)
            {
                return (false, "Class Is Full!");
            }

            if (@class.EnrolledStudents.Any(s => s.StudentId == student.StudentId))
                return (false, "Student already enrolled");

            @class.EnrolledStudents.Add(student);

            @class.Occupancy = @class.Occupancy + 1;

            if (@class.Occupancy >= @class.MaxOccupancy)
            {
                @class.IsClassFull = true;
            }

            await _classRepository.SaveChangesAsync();
            return (true,  $"{StudentName} successfully added to ${ClassName}" );

        }

        public async Task<(bool Success, string Message)> RemoveStudentFromClass(string ClassName, string StudentName)
        {
            var @class = await _classRepository.FindClassByTitle(ClassName);
            var student = await _studentRepository.FindStudentByNameAsync(StudentName);

            if (student == null)
            {
                return (false, "Student could not be found!");
            }

            if (@class == null)
            {
                return (false, "Class Doesn't Exist!");
            }

            if (@class.EnrolledStudents.Any(s => s.StudentId != student.StudentId))
                return (false, "Student not enrolled in this class");

            @class.EnrolledStudents.Remove(student);

            @class.Occupancy = @class.Occupancy - 1;

            await _classRepository.SaveChangesAsync();
            return (true, $"{StudentName} successfully removed from ${ClassName}");
        }

        public async Task<List<Class>> StudentEnrolledInClasses(string StudentName)
        {
            var student = await _studentRepository.FindStudentByNameAsync(StudentName);

            if (student == null)
            {
                return null ;
            }

            if (student.EnrolledClasses == null)
            {
                return null;
            }

            var enrolledStudentClasses = student.EnrolledClasses.ToList();

            return (enrolledStudentClasses);
        }

        public async Task<IEnumerable<Class>> GetAvailableClasses()
        {
            return await _classRepository.GetAvailableClassesAsync();

        }
        
    }
}
