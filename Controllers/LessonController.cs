using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTutor.Data;
using WebTutor.Medels;

namespace WebTutor.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("Test")]
    public class LessonController : ControllerBase
    {
        private readonly ApplicationContext db;

        public LessonController(ApplicationContext db)
        {
            this.db = db;
        }
		[HttpPost]
        public async Task<IResult> Create(Lesson lesson)
        {
            Console.WriteLine("PostLesson");
            string? idOrder = User?.FindFirst("id")?.Value;
            if (idOrder == null) return Results.Unauthorized();
            lesson.OrderId = int.Parse(idOrder);
            DateTime time = DateTime.Now;
            lesson.DatePublic = new DateTime(time.Ticks - (time.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Utc);

            time = lesson.DateTime;
            lesson.DateTime = new DateTime(time.Ticks, DateTimeKind.Utc);
            await db.Lessons.AddAsync(lesson);
            await db.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpGet("{id}")]
        public async Task<PostLesson?> Get(int id)
        {
            Console.WriteLine($"GetLesson");
            return await PostLesson.Convent(db, await db.Lessons.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpGet]
        public async Task<IResult> GetOrder()
        {
            int idOrder = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            Console.WriteLine($"GetLesson Order:{User?.Identity?.Name} ID:{User?.Claims.First(x => x.Type == "id").Value}");
            if (idOrder != 0)
            {
                List<PostLesson> lessons = new();
                List<Lesson> allLessons = await db.Lessons.ToListAsync();
                foreach (Lesson lesson in allLessons)
                {
                    if (lesson.OrderId == idOrder) lessons.Add(await PostLesson.Convent(db, lesson));
                }
                return Results.Ok(lessons);
            }
            return Results.Ok(await db.Lessons.ToListAsync());
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DeleteLesson");
            Lesson? lesson = db.Lessons.FirstOrDefault(x => x.Id == id);
            if (lesson == null) return (IActionResult)Results.BadRequest();
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return Ok();
        }
    }
}