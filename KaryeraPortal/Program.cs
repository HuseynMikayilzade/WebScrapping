using KaryeraPortal.Services;

namespace KaryeraPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<HelloJobScraperService>();
            builder.Services.AddScoped<JobSearchScraperService>();
            builder.Services.AddScoped<BusyScraperService>();
            builder.Services.AddScoped<SmartJobScraperService>();
            builder.Services.AddScoped<JobsGlorriScraperService>();
            var app = builder.Build();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}