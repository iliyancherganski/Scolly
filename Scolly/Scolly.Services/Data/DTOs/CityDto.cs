using System.ComponentModel.DataAnnotations;

namespace Scolly.Services.Data.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
