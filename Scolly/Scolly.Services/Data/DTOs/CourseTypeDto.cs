using Scolly.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Scolly.Services.Data.DTOs
{
    public class CourseTypeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<CourseDto> CourseDtos { get; set; } = new List<CourseDto>();
    }
}
