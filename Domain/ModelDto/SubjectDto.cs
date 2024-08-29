namespace StudentInformation.Domain.ModelDto
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Language { get; set; }
        public List<TeacherDto> Teachers { get; set; }
    }
}
