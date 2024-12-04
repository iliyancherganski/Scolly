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

        public List<CourseDto> CourseDtos { get; set; } = new List<CourseDto>();
        public List<SpecialtyDto> SpecialtyDtos { get; set; } = new List<SpecialtyDto>();
    }
}
