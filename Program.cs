using WebTutorCore.Data.Models;
using WebTutorCore.Data;
using Microsoft.EntityFrameworkCore;

namespace WebTutorCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            if (!app.Environment.IsDevelopment()) app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");
            Test();
            app.Run();
        }
        private static async void Test()
        {
            using (WebTutorCore.Data.AppContext db = new WebTutorCore.Data.AppContext())
            {
                var users = await db.Users.ToListAsync();
                Console.WriteLine("������ ��������:");
                foreach (User u in users) Console.WriteLine($"{u.Id}.{u.Name} {u.Surname} T:{u.Telephone} P:{u.Password}");

                Lesson? lesson = await db.Lessons.FindAsync(1);
                if (lesson != null)
                {
                    lesson.OrderId++;
                    await db.SaveChangesAsync();
                }
                var lessons = await db.Lessons.ToListAsync();
                Console.WriteLine("������ ��������:");
                foreach (Lesson l in lessons) Console.WriteLine($"{l.Id}. {l.Day}.{l.Month}.{l.Year} order:{l.OrderId}");
            }
        }
    }
}