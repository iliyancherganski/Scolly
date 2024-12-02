using Scolly.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Services.Data.DTOs
{
    public class TeacherDto
    {
        [Required]
        public int Id { get; set; }

        public string UserDtoId { get; set; } = null!;
        public UserDto UserDto { get; set; } = null!;

        public List<TeacherCourseDto> TeacherCourseDtos { get; set; } = new List<TeacherCourseDto>();
        public List<TeacherSpecialtyDto> TeacherSpecialtyDtos { get; set; } = new List<TeacherSpecialtyDto>();
    }
}
