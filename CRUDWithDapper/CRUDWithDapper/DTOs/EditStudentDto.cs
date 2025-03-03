namespace CRUDWithDapper.DTOs
{
    public class EditStudentDto
    {
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required double GPA { get; set; }
        public required string Major { get; set; }
    }
}
