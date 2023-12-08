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
        private readonly LessonContext db;

        public LessonController(LessonContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public async Task<int> Create(Lesson lesson)
        {
            Console.WriteLine("PostLesson");
            string? idOrder = User?.FindFirst("id")?.Value;
            if (idOrder == null) return 0;
            lesson.OrderId = int.Parse(idOrder);
            lesson.DatePublic = DateTime.UtcNow;
            await db.Lessons.AddAsync(lesson);
            await db.SaveChangesAsync();
            return db.Lessons.ToList().Last().Id;
        }
        [HttpGet("{id}")]
        public Lesson? Get(int id)
        {
            Console.WriteLine($"GetLesson");
            Lesson? lesson = db.Lessons.FirstOrDefault(x => x.Id == id);
            return lesson;
        }
        [HttpGet]
        public async Task<List<Lesson>> GetOrder()
        {
            int idOrder = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            Console.WriteLine($"GetLesson Order:{User?.Identity?.Name} ID:{User?.Claims.First(x => x.Type == "id").Value}");
            if (idOrder != 0)
            {
                List<Lesson> lessons = new();
                List<Lesson> allLessons = await db.Lessons.ToListAsync();
                foreach (Lesson lesson in allLessons)
                {
                    if (lesson.OrderId == idOrder) lessons.Add(lesson);
                }
                return lessons;
            }
            return await db.Lessons.ToListAsync();
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