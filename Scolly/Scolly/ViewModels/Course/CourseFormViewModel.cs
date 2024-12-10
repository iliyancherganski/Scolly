using Scolly.Services.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Scolly.ViewModels.Course
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public int AgeGroupId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Полето 'Описание' трябва да бъде поппълнено.")]
        [MaxLength(400)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0, 100000, ErrorMessage = "Цената трябва да бъде число между 0 и 100000.")]
        public decimal Price { get; set; }

        [Required]
        public int CourseTypeId { get; set; }


        [Required]
        public List<int> TeacherIds { get; set; } = new List<int>();    
    }
}
