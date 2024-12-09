using Scolly.Services.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Scolly.ViewModels.Child
{
    public class ChildFormViewModel
    {
        public ChildFormViewModel()
        {
        }

        public ChildFormViewModel(ChildDto dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            ParentId = dto.ParentDtoId;
            PhoneNumber = dto.PhoneNumber;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Полето 'Собствено име' трябва да бъде попълнено.")]
        [MaxLength(50)]
        [MinLength(1)]
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Фамилия' трябва да бъде попълнено.")]
        [MaxLength(50)]
        [MinLength(1)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required]
        public int ParentId { get; set; } = 0;

        [AllowNull]
        [Phone]
        [Display(Name = "Телефон на детето")]
        public string? PhoneNumber { get; set; }
    }
}
