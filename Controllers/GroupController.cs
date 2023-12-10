using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTutor.Data;
using WebTutor.Medels;

namespace WebTutor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Test")]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationContext db;
        public GroupController(ApplicationContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<int> Create(Group group)
        {
            Console.WriteLine("PostGroup");
            await db.Groups.AddAsync(group);
			await db.SaveChangesAsync();
            return db.Groups.ToList().Last().Id;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<Group?> GetName(int id)
        {
            Console.WriteLine("GetLesson");
            Group? group = await db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            return group;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Group>> GetAll()
        {
            return await db.Groups.ToListAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            Console.WriteLine("DeleteGrop");
            Group? group = await db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (group == null) return Results.BadRequest();
            db.Groups.Remove(group);
			await db.SaveChangesAsync();
            return Results.Ok();
        }
    }
}
