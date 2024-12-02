using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Infrastructure.Data.Models
{
    public class TeacherCourse
    {
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
