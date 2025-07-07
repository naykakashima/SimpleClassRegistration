namespace ClassRegistration.Domain.Models
{
    public class Student
    {
        public Guid StudentId { get; set; } = Guid.NewGuid();
        public string StudentName { get; set; }

        public List<Class> EnrolledClasses { get; set; }

        public Student (string studentName)
        {
            this.StudentName = studentName;
        }

    }

    
}
