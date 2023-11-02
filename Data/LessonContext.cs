using Microsoft.EntityFrameworkCore;

namespace WebTutor.Data
{
    public class LessonContext : DbContext
    {
        public LessonContext(DbContextOptions<LessonContext> options) : base(options) { }
        public virtual DbSet<LessonContext> Lessons { get; set; }
    }
}
