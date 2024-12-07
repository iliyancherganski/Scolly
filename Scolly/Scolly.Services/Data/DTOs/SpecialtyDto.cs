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

        [Required(ErrorMessage = "Полето 'Име' трябва да бъде попълнено.")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
