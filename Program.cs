using Microsoft.EntityFrameworkCore;
using WebTutor.Data;
using WebTutor.Services;
using WebTutor.Services.Intrerfaces;

namespace WebTutor
{
    public class Program
    {
        public static string MyAllowSpecificOrigins = "Test";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IPostServices, PersonServices>();
            builder.Services.AddDbContext<PersonContext>(bd => bd.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
            //builder.Services.AddSingleton<PersonContext>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://25.75.246.82:44405")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            builder.Services.AddCors(options => options.AddPolicy("Test", builder =>
            { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}