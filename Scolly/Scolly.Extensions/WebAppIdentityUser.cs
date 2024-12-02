using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;

namespace Scolly.Extensions
{
    public static class WebAppIdentityUser
    {
        public static WebApplicationBuilder AddUserIdentityOnBuilder(WebApplicationBuilder builder)
        {
           /* builder.Services.AddIdentityCore<User>
                (options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();*/
            builder.Services.AddIdentity<User, IdentityRole>
                (options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return builder;
        }
    }
}
