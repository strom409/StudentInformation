using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformation.Domain.ModelDto;
using StudentInformation.Services.Interface;

namespace StudentInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teachers = await _teacherService.GetTeachersAsync();
            return Ok(teachers);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> PostTeacher([FromBody] TeacherDto teacherDto)
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacherDto);
            return CreatedAtAction(nameof(GetTeachers), new { id = createdTeacher.Id }, createdTeacher);
        }
    }
}
