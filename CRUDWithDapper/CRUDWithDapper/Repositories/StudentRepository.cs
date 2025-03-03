using CRUDWithDapper.Context;
using CRUDWithDapper.DTOs;
using CRUDWithDapper.Interfaces;
using CRUDWithDapper.Models;
using Dapper;
using System.Data;

namespace CRUDWithDapper.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _context;

        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var query = "SELECT * FROM student WHERE GPA > 7";

            using(var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<Student>(query);
                return students.ToList();
            }
        }
        public async Task<Student> GetStudent(int id)
        {
            var query = "SELECT * FROM student WHERE student_id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QuerySingleOrDefaultAsync<Student>(query, new { id });
                return student;
            }
        }
        public async Task<Student> CreateStudent(StudentDto studentDto)
        {
            var query = "INSERT INTO student (first_name, last_name, gpa, major, enrollment_date) " +
                "VALUES (@First_Name, @Last_Name, @GPA, @Major, @Enrollment_Date)" + 
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("first_name", studentDto.First_Name, DbType.String);
            parameters.Add("last_name", studentDto.Last_Name, DbType.String);
            parameters.Add("gpa", studentDto.GPA, DbType.Double);
            parameters.Add("major", studentDto.Major, DbType.String);
            parameters.Add("enrollment_date", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                //await connection.ExecuteAsync(query, parameters);
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createStudent = new Student
                {
                    student_id = id,
                    First_Name = studentDto.First_Name,
                    Last_Name = studentDto.Last_Name,
                    GPA = studentDto.GPA,
                    Major = studentDto.Major,
                    Enrollment_Date = DateTime.Now,
                };
                return createStudent;
            }
        }
        public async Task UpdateStudent(int id, EditStudentDto editStudentDto)
        {
            var query = "UPDATE student SET first_name = @First_Name, last_name = @Last_Name, gpa = @GPA, " +
                "major = @Major WHERE student_id = @student_id";
            var parameters = new DynamicParameters();
            parameters.Add("student_id", id, DbType.Int32);
            parameters.Add("first_name", editStudentDto.First_Name, DbType.String);
            parameters.Add("last_name", editStudentDto.Last_Name, DbType.String);
            parameters.Add("gpa", editStudentDto.GPA, DbType.Double);
            parameters.Add("major", editStudentDto.Major, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }
        public async Task DeleteStudent(int id)
        {
            var query = "DELETE FROM student WHERE student_id = @Id";
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<string> GetMajorByStudentId(int id)
        {
            var procedureName = "ShowMajorByStudentId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            using(var connection = _context.CreateConnection())
            {
                var major = await connection.QueryFirstOrDefaultAsync<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return major;
            }
        }
    }
}
