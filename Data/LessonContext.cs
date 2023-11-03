using Microsoft.EntityFrameworkCore;
using WebTutor.Medels;

namespace WebTutor.Data
{
    public class LessonContext : DbContext
    {
        public LessonContext(DbContextOptions<LessonContext> options) : base(options) {}
        public virtual DbSet<Lesson> Lessons { get; set; }
    }
}
