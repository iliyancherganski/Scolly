using Scolly.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Data.DTOs
{
    public class SpecialtyDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<TeacherSpecialtyDto> TeacherSpecialtyDtos { get; set; } = new List<TeacherSpecialtyDto>();
    }
}
