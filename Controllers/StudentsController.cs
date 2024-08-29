using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformation.Domain.ModelDto;
using StudentInformation.Services.Interface;

namespace StudentInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents(string search, string className)
        {
            var students = await _studentService.GetStudentsAsync(search, className);
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> PostStudent([FromBody] StudentDto studentDto)
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudents), new { id = createdStudent.Id }, createdStudent);
        }


    }
}
