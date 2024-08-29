using FluentNHibernate.Mapping;
using StudentInformation.Domain.Models;

namespace StudentInformation.Mapping
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Age).Not.Nullable();
            Map(x => x.ImagePath).Column("Image");
            Map(x => x.Class);
            Map(x => x.RollNumber).Not.Nullable();
            Map(x => x.AdditionalInfo).Not.Nullable();
            Table("Students");
        }
    }

    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Age).Not.Nullable();
            Map(x => x.ImagePath);
            Map(x => x.Sex).Not.Nullable();
            Table("Teachers");
        }
    }

    public class SubjectMap : ClassMap<Subject>
    {
        public SubjectMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Class).Not.Nullable();
            Map(x => x.Language);
            // HasMany(x => x.TeacherSubjects).Cascade.All();
            Table("Subjects");
        }
    }

    //public class TeacherSubjectMap : ClassMap<TeacherSubject>
    //{
    //    public TeacherSubjectMap()
    //    {
    //        CompositeId()
    //            .KeyReference(x => x.Teacher, "TeacherId")
    //            .KeyReference(x => x.Subject, "SubjectId");

    //        Map(x => x.Class).Not.Nullable();
    //        Table("TeacherSubjects");
    //    }
    //}

}

