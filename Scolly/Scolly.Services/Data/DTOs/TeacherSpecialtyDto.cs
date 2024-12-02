using Scolly.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Services.Data.DTOs
{
    public class TeacherSpecialtyDto
    {
        public int TeacherDtoId { get; set; }
        public TeacherDto TeacherDto { get; set; } = null!;

        public int SpecialtyDtoId { get; set; }
        public SpecialtyDto SpecialtyDto { get; set; } = null!;
    }
}
