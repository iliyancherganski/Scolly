/*using Microsoft.AspNetCore.Identity;
using Scolly.Infrastructure.Data.Models;

namespace Scolly.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole userRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(userRole);
                }

                if (!await roleManager.RoleExistsAsync("Teacher"))
                {
                    IdentityRole userRole = new IdentityRole("Teacher");
                    await roleManager.CreateAsync(userRole);
                }

                if (!await roleManager.RoleExistsAsync("Parent"))
                {
                    IdentityRole userRole = new IdentityRole("Parent");
                    await roleManager.CreateAsync(userRole);
                }
                // ADMIN
                User? admin = await userManager.FindByEmailAsync("admin@admin.bg");
                if (admin != null)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                int teachersCount = 5;
                int parentsCount = 5;

                // TEACHERS
                for (int i = 1; i <= teachersCount; i++)
                {
                    User? teacher = await userManager.FindByEmailAsync($"teacher{i}@gmail.com");
                    if (teacher != null)
                    {
                        await userManager.AddToRoleAsync(teacher, "Teacher");
                    }
                }

                // PARENTS
                for (int i = 1; i <= parentsCount; i++)
                {
                    User? parent = await userManager.FindByEmailAsync($"parent{i}@gmail.com");
                    if (parent != null)
                    {
                        await userManager.AddToRoleAsync(parent, "Parent");
                    }
                }


            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}*/