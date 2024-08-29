using NHibernate.Linq;
using StudentInformation.Domain.ModelDto;
using StudentInformation.Domain.Models;
using StudentInformation.Services.Interface;

namespace StudentInformation.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly NHibernate.ISession _session;

        public TeacherService(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _session.Query<Teacher>().ToListAsync();
            return teachers.Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                Age = t.Age,
                ImagePath = t.ImagePath,
                Sex = t.Sex
            });
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _session.GetAsync<Teacher>(id);
            if (teacher == null)
                return null;

            return new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Age = teacher.Age,
                ImagePath = teacher.ImagePath,
                Sex = teacher.Sex
            };
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            var teacher = new Teacher
            {
                Name = teacherDto.Name,
                Age = teacherDto.Age,
                ImagePath = teacherDto.ImagePath,
                Sex = teacherDto.Sex
            };

            using (var transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(teacher);
                await transaction.CommitAsync();
            }

            teacherDto.Id = teacher.Id;
            return teacherDto;
        }
    }

}
