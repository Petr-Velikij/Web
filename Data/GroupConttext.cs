using Microsoft.EntityFrameworkCore;
using WebTutor.Medels;

namespace WebTutor.Data
{
    public class GroupConttext : DbContext
    {
        public GroupConttext(DbContextOptions<GroupConttext> options) : base(options) { }
        public virtual DbSet<Group> Groups { get; set; }
    }
}