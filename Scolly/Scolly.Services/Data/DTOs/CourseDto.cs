using System.ComponentModel.DataAnnotations;

namespace Scolly.Services.Data.DTOs
{
    public class CourseDto
    {
        [Required]
        public int Id { get; set; }

        public int AgeGroupDtoId { get; set; }
        public AgeGroupDto AgeGroupDto { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; } = null!;


        public int CourseTypeDtoId { get; set; }
        public CourseTypeDto CourseTypeDto { get; set; } = null!;

        public List<TeacherDto> TeacherDtos { get; set; } = new List<TeacherDto>();
        public List<CourseRequestDto> CourseRequestDtos { get; set; } = new List<CourseRequestDto>();
    }
}
