using Scolly.Infrastructure.Data.Enums;
using Scolly.Services.DTOs.Enums;

namespace Scolly.Services.Data.DTOs
{
    public class CourseRequestDto
    {
        public int Id { get; set; }

        public int ChildDtoId { get; set; }
        public ChildDto ChildDto { get; set; } = null!;

        public int CourseDtoId { get; set; } 
        public CourseDto CourseDto { get; set; } = null!;

        public RequestStatusDto Status { get; set; }
    }
}
