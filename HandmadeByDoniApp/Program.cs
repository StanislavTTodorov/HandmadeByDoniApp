
using HandmadeByDoniApp.Services.Data;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Services.Data.Service;
using HandmadeByDoniApp.Web.Infrastructure.Extensions;
using HandmadeByDoniApp.Web.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationDbContext(builder.Configuration);
        builder.Services.AddApplicationIdentiry(builder.Configuration);

        builder.Services.AddRecaptchaService();
        //****//
        // Custom localization for changing the default names of the scheduler control e.g.Appoinment
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        //builder.Services.AddSingleton(typeof(IStringLocalizer), typeof(CustomLocalizationService));
        //builder.Services.AddSingleton(typeof(CustomLocalizationService));

        // builder.Services.AddSingleton<IStringLocalizer>(provider => new CustomLocalizationService("Resources.App", "Resources"));
        //****//
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

        var localizationSettings = builder.Configuration.GetSection("Localization");
        //****//
        // Регистриране на CustomLocalizationService
        builder.Services.AddSingleton<IStringLocalizer>(provider =>
            new CustomLocalizationService(
                localizationSettings["BaseName"],
                localizationSettings["ResourceDir"]
            ));

        // Останалата конфигурация за локализация
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { "en-US", "bg-BG" };
            options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
            options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
            options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
        });
        //****//


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
        ////bg 
        //var supportedCultures = app.Configuration.GetSection("Cultures")
        //  .GetChildren().ToDictionary(x => x.Key, x => x.Value).Keys.ToArray();


        //var localizationOptions = new RequestLocalizationOptions()
        //    .AddSupportedCultures(supportedCultures)
        //    .AddSupportedUICultures(supportedCultures)
        //    .SetDefaultCulture(supportedCultures[0]);
        //app.UseRequestLocalization(localizationOptions);
        //var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
        //app.UseRequestLocalization(localizationOptions);
        //****//
        app.Use(async (context, next) =>
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            await next.Invoke();
        });
        var supportedCulturess = new[] { "en-US", "bg-BG" };
        var localizationOptionss = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCulturess[0])
            .AddSupportedCultures(supportedCulturess)
            .AddSupportedUICultures(supportedCulturess);

        app.UseRequestLocalization(localizationOptionss);
        //****//

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.EnableOnlineUsersCheck();

        if (app.Environment.IsDevelopment())
        {
            app.SeedAdministrator(DevelopmentAdminEmail);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });


        app.Run();
    }
}
