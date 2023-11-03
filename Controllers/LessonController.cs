﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebTutor.Data;
using WebTutor.Medels;

namespace WebTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Test")]
    public class LessonController : ControllerBase
    {
        private readonly LessonContext db;

        public LessonController(LessonContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public int Create(Lesson lesson)
        {
            Console.WriteLine("PostLesson");
            //db.Lessons.Add(new Lesson { DateTime = new DateTime(2023, 11, 03, 2, 9, 0, DateTimeKind.Utc), OrderId = 1, Task = "Test"});
            db.Lessons.Add(lesson);
            db.SaveChanges();
            return db.Lessons.ToList().Last().Id;
        }
        [HttpGet("{id}")]
        public Lesson? Get(int id)
        {
            Console.WriteLine("GetLesson");
            Lesson? lesson = db.Lessons.FirstOrDefault(x => x.Id == id);
            return lesson;
        }
        [HttpGet]
        public List<Lesson> GetOrder(int idOrder)
        {
            Console.WriteLine("GetLessonOrder");
            if (idOrder != 0)
            {
                List<Lesson> lessons = new();
                List<Lesson> allLessons = db.Lessons.ToList();
                foreach (Lesson lesson in allLessons)
                {
                    if (lesson.OrderId == idOrder) lessons.Add(lesson);
                }
                return lessons;
            }
            return db.Lessons.ToList();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DeleteLesson");
            Lesson lesson = db.Lessons.First(x => x.Id == id);
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return Ok();
        }
    }
}