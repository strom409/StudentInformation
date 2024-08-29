using StudentInformation.Domain.ModelDto;
using StudentInformation.Domain.Models;
using StudentInformation.Repository;
using StudentInformation.Services.Interface;

namespace StudentInformation.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly NHibernate.ISession _session;
        private readonly IStudentRepository<Subject> _studentRepository;

        public SubjectService(NHibernate.ISession session, IStudentRepository<Subject> studentRepository)
        {
            _session = session;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync()
        {
            var subjects = await _studentRepository.GetAll();

            return subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Class = s.Class,
                Language = s.Language,
                Teachers = s.TeacherSubjects.Select(ts => new TeacherDto
                {
                    Id = ts.Teacher.Id,
                    Name = ts.Teacher.Name,
                    Age = ts.Teacher.Age,
                    ImagePath = ts.Teacher.ImagePath,
                    Sex = ts.Teacher.Sex
                }).ToList()
            });
        }

        public async Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                Name = subjectDto.Name,
                Class = subjectDto.Class,
                Language = subjectDto.Language
            };

            using (var transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(subject);
                await transaction.CommitAsync();
            }

            subjectDto.Id = subject.Id;
            return subjectDto;
        }




    }

}