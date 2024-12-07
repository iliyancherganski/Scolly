using Scolly.Services.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Scolly.Services.Data.DTOs
{
    public class UserDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public int CityDtoId { get; set; }
        public CityDto CityDto { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Address { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        public string Password { get; set; } = null!;

        [Required]
        public UserRoles Role { get; set; }
    }
}
