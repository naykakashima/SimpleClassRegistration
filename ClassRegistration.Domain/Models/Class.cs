namespace ClassRegistration.Domain.Models
{
    public class Class
    {
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public Guid ClassID { get; set; } = Guid.NewGuid();
        public int MaxOccupancy { get; set; }

    }
    
}
