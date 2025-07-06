namespace ClassRegistration.Domain.Models
{
    public class Student
    {
        public Guid StudentId { get; set; } = Guid.NewGuid();
        public string StudentName { get; set; }
    }
}
