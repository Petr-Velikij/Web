using Microsoft.EntityFrameworkCore;
using WebTutor.Medels;

namespace WebTutor.Data
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
		public virtual DbSet<Group> Groups { get; set; }
		public virtual DbSet<Lesson> Lessons { get; set; }
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Authorization> Authorization { get; set; }
	}
}