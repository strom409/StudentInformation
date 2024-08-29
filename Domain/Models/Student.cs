namespace StudentInformation.Domain.Models
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string Class { get; set; }
        public virtual string RollNumber { get; set; }
        public virtual string AdditionalInfo { get; set; }
    }

    public class Teacher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string Sex { get; set; }
        public virtual IList<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

    public class Subject
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Class { get; set; }
        public virtual string Language { get; set; }
        public virtual IList<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

    public class TeacherSubject
    {
        public virtual int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual string Class { get; set; }
    }

}
