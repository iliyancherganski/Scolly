using System.ComponentModel.DataAnnotations;

namespace Scolly.ViewModels.User
{
    public class EditAccountViewModel
    {
        public int Id { get; set; }
        public bool IsTeacher { get; set; }

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

        public List<int> SpecialtyIds { get; set; } = new List<int>();
    }
}
