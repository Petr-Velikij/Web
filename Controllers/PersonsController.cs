using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebTutor.Data;
using WebTutor.Medels;

namespace WebTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Test")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonContext db;

        public PersonsController(PersonContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public Authorization Create(Authorization aut)
        {
            Console.WriteLine("HttpPost");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Email == aut.Email);
            if (person == null)
            {
                db.Authorization.Add(aut);
                db.SaveChanges();
                return db.Authorization.ToList().Last();
            }
            return new Authorization();
        }
        [HttpPatch]
        public Authorization Update(Authorization Authorization)
        {
            Console.WriteLine("HttpPatch");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Id == Authorization.Id);
            if (person == null) return new Authorization();
            person.Email = Authorization.Email;
            person.Password = Authorization.Password;
            db.SaveChanges();
            return db.Authorization.First(x => x.Id == Authorization.Id);
        }
        [HttpGet("{id}")]
        public Authorization Get(int id)
        {
            Console.WriteLine("HttpGet");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Id == id);
            return person;
        }
        [HttpPut]
        public bool Check(Authorization aut)
        {
            Console.WriteLine("HttpCheck");
            Authorization? person = db.Authorization.FirstOrDefault(x => (x.Email == aut.Email && x.Password == aut.Password));
            if (person == null) return false;
            return true;
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("HttpDelete");
            Authorization _person = db.Authorization.First(x => x.Id == id);
            db.Authorization.Remove(_person);
            db.SaveChanges();
            return Ok();
        }
    }
}
