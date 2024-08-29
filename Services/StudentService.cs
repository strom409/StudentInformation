using NHibernate.Linq;
using StudentInformation.Domain.ModelDto;
using StudentInformation.Domain.Models;
using StudentInformation.Services.Interface;

namespace StudentInformation.Services
{
    public class StudentService : IStudentService
    {
        private readonly NHibernate.ISession _session;

        public StudentService(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync(string search, string className)
        {
            var query = _session.Query<Student>();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(className))
            {
                query = query.Where(s => s.Class == className);
            }

            var students = await query.ToListAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                ImagePath = s.ImagePath,
                Class = s.Class,
                RollNumber = s.RollNumber,
                AdditionalInfo = s.AdditionalInfo
            });
        }


        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                ImagePath = studentDto.ImagePath,
                Class = studentDto.Class,
                RollNumber = studentDto.RollNumber,
                AdditionalInfo = studentDto.AdditionalInfo
            };

            using (var transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(student);
                await transaction.CommitAsync();
            }

            studentDto.Id = student.Id;
            return studentDto;
        }


    }

}
