namespace Scolly.Services.Data.DTOs
{
    public class TeacherCourseDto
    {
        public int TeacherDtoId { get; set; }
        public TeacherDto TeacherDto { get; set; } = null!;

        public int CourseDtoId { get; set; } 
        public CourseDto CourseDto { get; set; } = null!;
    }
}
