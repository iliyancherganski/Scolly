using Scolly.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Data.DTOs
{
    public class ChildDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето 'Собствено име' трябва да бъде попълнено.")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Фамилия' трябва да бъде попълнено.")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public int ParentDtoId { get; set; }
        public ParentDto ParentDto { get; set; } = null!;

        [MaxLength(15)]
        [Phone(ErrorMessage = "Телефонния номер трябва да бъде валиден!")]
        public string? PhoneNumber { get; set; }

        public List<CourseRequestDto> CourseRequestsDtos { get; set; } = new List<CourseRequestDto>();
    }
}
