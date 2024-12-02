using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Infrastructure.Data.Models
{
    public class TeacherSpecialty
    {
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        [ForeignKey(nameof(Specialty))]
        public int SpecialtyId { get; set;}
        public Specialty Specialty { get; set; } = null!;

    }
}
