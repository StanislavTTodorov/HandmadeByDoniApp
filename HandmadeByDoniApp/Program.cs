
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationDbContext(builder.Configuration);
        builder.Services.AddApplicationIdentiry(builder.Configuration);

        builder.Services.AddRecaptchaService();

        builder.Services.ConfigureApplicationCookie(cfg =>
        {
            cfg.LoginPath = "/User/Login";
            cfg.AccessDeniedPath = "/Home/Error/401";
        });

        builder.Services
               .AddControllersWithViews()
               .AddMvcOptions(options =>
               {
                   options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                   options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
               });

        builder.Services.AddApplicationServises();




        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();

        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.SeedAdministrator(DevelopmentAdminEmail);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });


        app.Run();
    }
}
