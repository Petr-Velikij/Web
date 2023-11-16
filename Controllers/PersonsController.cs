using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost("registration")]
        public IResult Create(Authorization aut)
        {
            
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Email == aut.Email);
            if (person == null)
            {
                db.Authorization.Add(aut);
                db.SaveChanges();
                Login(aut);
                Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] CreatePerson Status:200");
            }
            Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] Not create Email:{aut.Email}, Password:{aut.Password} Status:402");
            return Results.Unauthorized();
        }

        [Authorize]
        [HttpPatch]
        public Authorization Patch(Authorization Authorization)
        {
            Console.WriteLine("PatchPerson");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Id == Authorization.Id);
            if (person == null) return new Authorization();
            person.Email = Authorization.Email;
            person.Password = Authorization.Password;
            db.SaveChanges();
            return db.Authorization.First(x => x.Id == Authorization.Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<Authorization> GetAll()
        {
            Console.WriteLine("GetAllPerson");
            return db.Authorization.ToList();
        }

        [HttpPost("login")]
        public IResult Login(Authorization aut)
        {
            var identity = GetIdentity(aut);
            if (identity == null)
            {
                Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] Login Email:{aut.Email}, Password:{aut.Password} Status:401");
                return Results.Unauthorized();
            }
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        //notBefore: DateTime.UtcNow,
                        claims: identity.Claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(30)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] Login Email:{aut.Email}, Password:{aut.Password} Status:200");
            Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] JWT:{encodedJwt}");
            return Results.Json(response);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DelitePerson");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Id == id);
            if (person == null) return (IActionResult)Results.BadRequest();
            db.Authorization.Remove(person);
            db.SaveChanges();
            return Ok();
        }
        private ClaimsIdentity? GetIdentity(Authorization aut)
        {
            Authorization? person = db.Authorization.FirstOrDefault(x => (x.Email == aut.Email && x.Password == aut.Password));
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, aut.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, aut.Role)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
        [Authorize]
        [HttpGet("check")]
        public IActionResult CheckToken()
        {
            Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] CheckToken {User.Claims.First(e => e.Type == ClaimsIdentity.DefaultNameClaimType).Value}");
            return Ok();
        }
    }
}
