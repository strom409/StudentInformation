using StudentInformation.Domain.ModelDto;

namespace StudentInformation.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetStudentsAsync(string search, string className);
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
    }
}
