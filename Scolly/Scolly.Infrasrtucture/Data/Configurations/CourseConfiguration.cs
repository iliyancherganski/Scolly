using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scolly.Infrastructure.Data.Models;

namespace Scolly.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // AgeGroudIds:
            // 1 - 5 клас
            // 2 - 6 клас
            // 3 - 7 клас
            // 4 - 5-7 клас

            // CourseTypeIds:
            // 1 - Математика
            // 2 - Български език и литература
            // 3 - Програмиране със C#
            // 4 - Английски език
            // 5 - Немски език
            // 6 - Френски език

            // Teachers:
            // 1 - Математика
            // 2 - Български език и литература
            // 3 - Програмиране със C#
            // 4 - Английски език
            // 5 - Немски език
            // 6 - Френски език

            List<Course> list = new List<Course>()
            {
                new Course
                {
                    Id = 1,
                    AgeGroupId = 1,
                    CourseTypeId = 2,
                    StartDate = new DateTime(2023, 10, 2),
                    EndDate = new DateTime(2024, 1, 15),
                    Price = 200,
                    Description = "Курс по български език и литература за ученици от 5 клас, насочен към подобряване на граматиката и аналитичните умения."
                },

                new Course
                {
                    Id = 2,
                    AgeGroupId = 1,
                    CourseTypeId = 2,
                    StartDate = new DateTime(2024, 1, 2),
                    EndDate = new DateTime(2024, 4, 15),
                    Price = 250,
                    Description = "Задълбочен курс по български език и литература за 5 клас, с акцент върху подготовката за национални изпити."
                },

                new Course
                {
                    Id = 3,
                    AgeGroupId = 4,
                    CourseTypeId = 5,
                    StartDate = new DateTime(2024, 1, 2),
                    EndDate = new DateTime(2024, 6, 15),
                    Price = 630,
                    Description = "Интензивен курс по немски език за ученици от 5 до 7 клас, включващ говорене, слушане и писане."
                },

                new Course
                {
                    Id = 4,
                    AgeGroupId = 1,
                    CourseTypeId = 6,
                    StartDate = new DateTime(2024, 10, 11),
                    EndDate = new DateTime(2025, 5, 25),
                    Price = 590,
                    Description = "Курс по френски език за начинаещи ученици от 5 клас, съсредоточен върху основните езикови умения."
                },

                new Course
                {
                    Id = 5,
                    AgeGroupId = 3,
                    CourseTypeId = 3,
                    StartDate = new DateTime(2023, 11, 10),
                    EndDate = new DateTime(2024, 6, 29),
                    Price = 990,
                    Description = "Практически курс по програмиране със C# за ученици от 7 клас, включващ основи на програмирането и реални проекти."
                },
            };

            builder.HasData(list);
        }
    }
}
