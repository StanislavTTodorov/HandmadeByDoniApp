

using HandmadeByDoniApp.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Infrastructure.Extensions
{
    public static class WebApplicationBulderExtensions
    {

        /// <summary>
        ///  This method registers all services with their interfaces and implementations of given assembly.
        ///  The assembly is taken from the type of random service implementation provided. 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid Service type provided!");
            }
            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();
            foreach (Type implamentationType in serviceTypes)
            {
                Type? interfaceType = implamentationType.GetInterface($"I{implamentationType.Name}");

                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No Interface is provided for the service whih name: {implamentationType.Name}");
                }
                services.AddScoped(interfaceType, implamentationType);
            }
        }
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app,string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminiRoleName))
                {
                    return;
                 
                }

                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminiRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser adminUser = await userManager.FindByEmailAsync(email);

               await userManager.AddToRoleAsync(adminUser, AdminiRoleName);
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
