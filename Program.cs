using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;

namespace Spring2024_Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // 1) fetch infor about connection string
            var conString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2) Add context class to the set of services and define the option to use sql server on that connection string
            // been fetched in previous step
            builder.Services.AddDbContext<BooksDBContext>(options => options.UseSqlServer(conString));


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