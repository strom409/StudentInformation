using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformation.Domain.ModelDto;
using StudentInformation.Services.Interface;

namespace StudentInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var subjects = await _subjectService.GetSubjectsAsync();
            return Ok(subjects);
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> PostSubject([FromBody] SubjectDto subjectDto)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(subjectDto);
            return CreatedAtAction(nameof(GetSubjects), new { id = createdSubject.Id }, createdSubject);
        }
    }
}
