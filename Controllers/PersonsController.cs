using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        public class Registr
        {
            public Authorization auth { get; set; } = new();
            public Person person { get; set; } = new();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("registration")]
        public async Task<IResult> Create(Registr reg)
        {
            
            Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => x.Email == reg.auth.Email);
            if (person == null)
            {
                db.Authorization.Add(reg.auth);
                await db.SaveChangesAsync();
                reg.person.IdAut = reg.auth.Id;
                db.Persons.Add(reg.person);
                await db.SaveChangesAsync();
                Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] CreatePerson Status:200");
                return Results.Ok();
            }
            Console.WriteLine($"[{DateTime.UtcNow.ToString("HH:mm:ss")}] Not create Email:{reg.auth.Email}, Password:{reg.auth.Password} Status:401");
            return Results.Unauthorized();
        }

        [Authorize]
        [HttpPatch]
        public async Task<IResult> Patch(Authorization Authorization)
        {
            Console.WriteLine("PatchPerson");
            Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => x.Id == Authorization.Id);
            if (person == null) return Results.BadRequest();
            person.Email = Authorization.Email;
            person.Password = Authorization.Password;
            db.SaveChanges();
            return Results.Json(db.Authorization.First(x => x.Id == Authorization.Id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<Authorization>> GetAll()
        {
            Console.WriteLine("GetAllPerson");
            return await db.Authorization.ToListAsync();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(Authorization aut)
        {
            var identity = await GetIdentity(aut);
            if (identity == null)
            {
                Console.WriteLine($"Login Email:{aut.Email}, Password:{aut.Password} Status:401");
                return Results.Unauthorized();
            }
            Person? DataPersom = await db.Persons.FirstOrDefaultAsync(id => id.IdAut.ToString() == identity.FindFirst("id").Value);
            if (DataPersom == null) DataPersom = new Person {Name = "Без имени", Surname = "Без фамилии" };
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        //notBefore: DateTime.UtcNow,
                        claims: identity.Claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                name = DataPersom?.Name,
                surname = DataPersom?.Surname,
                role = identity?.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value
            };
            Console.WriteLine($"Login Email:{aut.Email}, Password:{aut.Password} Status:200");
            Console.WriteLine($"JWT:{encodedJwt}");
            return Results.Json(response);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine("DelitePerson");
            Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null) return (IActionResult)Results.BadRequest();
            db.Authorization.Remove(person);
            await db.SaveChangesAsync();
            return Ok();
        }
        private async Task<ClaimsIdentity?> GetIdentity(Authorization aut)
        {
            Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => (x.Email == aut.Email && x.Password == aut.Password));
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim("id", person.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
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
            Console.WriteLine($"CheckToken {User.Claims.First(e => e.Type == ClaimsIdentity.DefaultNameClaimType).Value}");
            return Ok();
        }
    }
}
