using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace HandmadeByDoniApp.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddApplicationServises(this IServiceCollection services)
        {

            services.AddServices(typeof(IProductService));
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]
            //    {
            //        new CultureInfo("en-US"),
            //        new CultureInfo("bg-BG"),
            //    };
            //    options.DefaultRequestCulture = new RequestCulture("en-US");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentException();

            services.AddDbContext<HandmadeByDoniAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            return services;

        }
        public static IServiceCollection AddApplicationIdentiry(this IServiceCollection services, IConfiguration config)
        {

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = config.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireDigit = config.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequireNonAlphanumeric = config.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequireLowercase = config.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase = config.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequiredLength = config.GetValue<int>("Identity:Password:RequiredLength");
            })
              .AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<HandmadeByDoniAppDbContext>();
            return services;
        }
    }
}
