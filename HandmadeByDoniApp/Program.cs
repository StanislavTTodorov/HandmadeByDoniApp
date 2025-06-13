
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
using System.Text.RegularExpressions;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationDbContext(builder.Configuration);
        builder.Services.AddApplicationIdentiry(builder.Configuration);

        builder.Services.AddAuthentication()
              .AddGoogle(options =>
              {
                  IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
                  options.ClientId = googleAuthNSection["ClientId"] ?? string.Empty;
                  options.ClientSecret = googleAuthNSection["ClientSecret"] ?? string.Empty;
              });


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

        var supportedCultures = new[] { "en-US", "bg-BG" };

        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList(),
            SupportedUICultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList(),
        };

        app.UseRequestLocalization(localizationOptions);


        //****//

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.EnableOnlineUsersCheck();
        app.Use(async (context, next) =>
        {
            var cultureQuery = context.Request.Query["culture"];
            var cul = context.Request.Cookies;
            string? culture = null;
            if (cul[".AspNetCore.Culture"] != null)
            {
                var cultureSplit = cul[".AspNetCore.Culture"].Split("=").ToArray()[1];
                culture = cultureSplit.Split("|").ToArray()[0];
            }
            if (string.IsNullOrWhiteSpace(culture) || (culture != "bg-BG" && culture != "en-US"))
            { 
                culture = "en-US"; 
            }
            if (!string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);             
            }
            await next.Invoke();
        });

        if (app.Environment.IsDevelopment())
        {
            app.SeedAdministrator(DevelopmentAdminEmail);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });


        app.Run();
    }
}
