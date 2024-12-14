using System.ComponentModel.DataAnnotations;

namespace Scolly.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Полето 'Имейл' трябва да бъде попълнено.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Парола' трябва да бъде попълнено.")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}
