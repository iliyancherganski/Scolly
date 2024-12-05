using Microsoft.EntityFrameworkCore;

using Scolly.Extensions;
using Scolly.Infrastructure.Data;
using Scolly.Services.Services;
using Scolly.Services.Services.Contracts;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder = WebAppIdentityUser.AddUserIdentityOnBuilder(builder);

        builder.Services.AddScoped<IAgeGroupService, AgeGroupService>();
        builder.Services.AddScoped<IChildService, ChildService>();
        builder.Services.AddScoped<ICityService, CityService>();
        builder.Services.AddScoped<ICourseRequestService, CourseRequestService>();
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<ICourseTypeService, CourseTypeService>();
        builder.Services.AddScoped<IParentService, ParentService>();
        builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
        builder.Services.AddScoped<ITeacherService, TeacherService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.SeedRoles();

        app.Run();
    }
}