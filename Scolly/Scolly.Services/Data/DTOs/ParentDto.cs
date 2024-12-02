using Scolly.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scolly.Services.Data.DTOs
{
    public class ParentDto
    {
        [Required]
        public int Id { get; set; }

        public string UserDtoId { get; set; } = null!;
        public UserDto UserDto { get; set; } = null!;

        public List<ChildDto> ChildDtos { get; set; } = new List<ChildDto>();
    }
}
