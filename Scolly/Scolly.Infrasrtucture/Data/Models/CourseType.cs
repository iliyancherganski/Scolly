using System.ComponentModel.DataAnnotations;

namespace Scolly.Infrastructure.Data.Models
{
    public class CourseType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<Course> Courses { get; set; } = new List<Course>();

    }
}
