using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace ClassRegistration.Domain.Models
{
    public class Class
    {
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public Guid ClassID { get; set; } = Guid.NewGuid();
        public int Occupancy { get; set; }
        public int MaxOccupancy { get; set; } = 20;
        public bool IsClassFull { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public Class(string className, string classType, int maxOccupancy = 20)
        {
            ClassName = className;
            ClassType = classType;
            MaxOccupancy = maxOccupancy;
            Occupancy = 0;
            IsClassFull = false;
        }
    }
}
