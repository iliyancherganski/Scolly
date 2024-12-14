using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace Scolly.ViewModels.User
{
    public class RegisterFormViewModel
    {
        public RegisterFormViewModel()
        {
        }

        public RegisterFormViewModel(bool isTeacher)
        {
            if (isTeacher)
            {
                SpecialtyIds = new List<int>();
            }
        }

        [Required(ErrorMessage = "Полето 'Собствено име' трябва да бъде попълнено.")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълго.", MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Бащино име' трябва да бъде попълнено.")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълго.", MinimumLength = 2)]
        public string MiddleName { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Фамилия' трябва да бъде попълнено.")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Полето 'Адрес' трябва да бъде попълнено.")]
        [StringLength(150, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълъг.", MinimumLength = 2)]
        public string Address { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Имейл' трябва да бъде попълнено.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Парола' трябва да бъде попълнено.")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Потвърждаване на парола' трябва да бъде попълнено.")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ.")]
        public string ConfirmPassword { get; set; } = null!;

        public List<int>? SpecialtyIds { get; set; } = null;

    }
}
