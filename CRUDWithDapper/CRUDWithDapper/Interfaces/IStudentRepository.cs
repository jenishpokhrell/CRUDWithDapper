using CRUDWithDapper.DTOs;
using CRUDWithDapper.Models;

namespace CRUDWithDapper.Interfaces
{
    public interface IStudentRepository
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task<Student> CreateStudent(StudentDto studentDto);
        public Task UpdateStudent(int id, EditStudentDto editStudentDto);
        public Task DeleteStudent(int id);
        public Task<string> GetMajorByStudentId(int id);
    }
}
