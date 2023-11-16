using Microsoft.EntityFrameworkCore;
using WebTutor.Medels;

namespace WebTutor.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Authorization> Authorization { get; set; }
    }
}
