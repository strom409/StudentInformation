using StudentInformation.Domain.ModelDto;

namespace StudentInformation.Services.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetSubjectsAsync();
        Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto);
    }
}
