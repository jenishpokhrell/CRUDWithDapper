namespace CRUDWithDapper.Models
{
    public class Major
    {
        public required string ProgramName { get; set; }
        public DateTime ProgramStartDate { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
