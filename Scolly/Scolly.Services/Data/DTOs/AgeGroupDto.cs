using Scolly.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Scolly.Services.Data.DTOs
{
    public class AgeGroupDto
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Полето 'Име' трябва да бъде попълнено.")]
		[MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
