using Microsoft.EntityFrameworkCore;
using WebTutor.Data;

namespace WebTutor.Medels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DatePublic { get; set; }
		public string Title { get; set; } = "Без темы";
        public string Description { get; set; } = "";
        public int Groups { get; set; } = 0; 
        public Type Type { get; set; }
        public string? Venue { get; set; }
    }

    public class PostLesson
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DatePublic { get; set; }
        public string Title { get; set; } = "Без темы";
        public string Description { get; set; } = "";
        public String Groups { get; set; } = "Не назначена";
        public Type Type { get; set; }
        public string? Venue { get; set; }

        static public async Task<PostLesson> Convent(ApplicationContext db, Lesson lesson)
        {
            Group? group = await db.Groups.FindAsync(lesson.Groups);
            if (group == null) group = new Group { Id = 0, Name = "Утеряна" };
            return new PostLesson
            {
                Id = lesson.Id,
                OrderId = lesson.OrderId,
                DateTime = lesson.DateTime,
                DatePublic = lesson.DatePublic,
                Title = lesson.Title,
                Description = lesson.Description,
                Groups = group.Name,
                Type = lesson.Type,
                Venue = lesson.Venue
            };
        }
    }
    public enum Type
    {
        None,
        Online,
        InReally,
        News
    }
}
