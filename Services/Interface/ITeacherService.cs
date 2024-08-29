using StudentInformation.Domain.ModelDto;

namespace StudentInformation.Services.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto);
    }
}

