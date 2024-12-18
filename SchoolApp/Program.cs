using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Repositories;

namespace SchoolApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Student6DbContext>(options => options.UseSqlServer(connstring));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRepositories();

           // builder.Services

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
