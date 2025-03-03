namespace CRUDWithDapper.Models
{
    public class Student
    {
        public int student_id { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required double GPA { get; set; }
        public required string Major { get; set; }
        public required DateTime Enrollment_Date { get; set; }

    }
}
