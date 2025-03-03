using CRUDWithDapper.DTOs;
using CRUDWithDapper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithDapper.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}", Name = "StudentById")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
        {
            var createStudent = await _studentRepository.CreateStudent(studentDto);
            return CreatedAtRoute("StudentById", new { id = createStudent.student_id }, createStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] EditStudentDto editStudentDto)
        {
            var student = await _studentRepository.GetStudent(id);
            if(student == null)
            {
                return NotFound();
            }
            await _studentRepository.UpdateStudent(id, editStudentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetStudent(id);
            if(student is null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(id);
            return NoContent();
        }

        [HttpGet("ByStudentId/{id}")]
        public async Task<IActionResult> GetMajorByStudentId(int id)
        {
            var major = await _studentRepository.GetMajorByStudentId(id);
            if (major == null)
            {
                return NotFound();
            }
            return Ok(major);
        }
    }
}
