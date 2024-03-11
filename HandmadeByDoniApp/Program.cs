
using HandmadeByDoniApp.Web.Infrastructure.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationDbContext(builder.Configuration);
        builder.Services.AddApplicationIdentiry(builder.Configuration);

        builder.Services
               .AddControllersWithViews();

        builder.Services.AddApplicationServises();




        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
