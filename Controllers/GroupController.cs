using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebTutor.Data;
using WebTutor.Medels;

namespace WebTutor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Test")]
    public class GroupController : ControllerBase
    {
        private readonly GroupConttext db;
        public GroupController(GroupConttext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public int Create(Group group)
        {
            Console.WriteLine("PostGroup");
            db.Groups.Add(group);
            db.SaveChanges();
            return db.Groups.ToList().Last().Id;
        }

        [Authorize]
        [HttpGet("{id}")]
        public Group? Get(int id)
        {
            Console.WriteLine("GetLesson");
            Group? group = db.Groups.FirstOrDefault(x => x.Id == id);
            return group;
        }

        [Authorize]
        [HttpGet]
        public List<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DeleteGrop");
            Group? group = db.Groups.FirstOrDefault(x => x.Id == id);
            if (group == null) return (IActionResult)Results.BadRequest();
            db.Groups.Remove(group);
            db.SaveChanges();
            return Ok();
        }
    }
}
