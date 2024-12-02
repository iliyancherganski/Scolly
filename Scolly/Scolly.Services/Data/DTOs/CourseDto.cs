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


        public int CourseTypeDtoId { get; set; }
        public CourseTypeDto CourseTypeDto { get; set; } = null!;

        public List<TeacherCourseDto> TeacherCourseDtos { get; set; } = new List<TeacherCourseDto>();
        public List<CourseRequestDto> CourseRequestDtos { get; set; } = new List<CourseRequestDto>();
    }
}
