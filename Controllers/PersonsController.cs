using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebTutor.Medels;
using WebTutor.Services.Intrerfaces;

namespace WebTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Test")]
    public class PersonsController : ControllerBase
    {
        private readonly IPostServices postServices;

        public PersonsController(IPostServices postServices)
        {
            this.postServices = postServices;
        }
        //[HttpPost]
        public Authorization Create(Authorization Authorization)
        {
            Console.WriteLine("HttpPost");
            return postServices.Create(Authorization);
        }
        [HttpPatch]
        public Authorization Update(Authorization Authorization)
        {
            Console.WriteLine("HttpPatch");
            return postServices.Update(Authorization);
        }
        [HttpGet("{id}")]
        public Authorization Get(int id)
        {
            Console.WriteLine("HttpGet");
            return postServices.Get(id);
        }
        [HttpPost]
        public bool Check(Authorization aut)
        {
            Console.WriteLine("HttpCheck");
            return postServices.Check(aut);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("HttpDelete");
            postServices.Delete(id);
            return Ok();
        }
    }
}
