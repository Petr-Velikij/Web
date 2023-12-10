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
        private readonly ApplicationContext db;

        public PersonsController(ApplicationContext db)
        {
            this.db = db;
        }
        public class FullData
        {
            public Authorization auth { get; set; } = new();
            public Person person { get; set; } = new();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("registration")]
        public async Task<IResult> Create(FullData reg)
        {
			Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => x.Email == reg.auth.Email);
            if (person == null)
            {
                db.Authorization.Add(reg.auth);
                await db.SaveChangesAsync();
                reg.person.IdAut = reg.auth.Id;
                db.Persons.Add(reg.person);
                await db.SaveChangesAsync();
                Console.WriteLine($"CreatePerson Name:{reg.person.Name} Status:200");
                return Results.Ok();
            }
            Console.WriteLine($"Not create Email:{reg.auth.Email} Status:401");
            return Results.BadRequest();
        }
		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			Authorization? person = await db.Authorization.FirstOrDefaultAsync(x => x.Id == id);
			if (person == null) return (IActionResult)Results.BadRequest();
			db.Authorization.Remove(person);
			await db.SaveChangesAsync();
			Console.WriteLine($"DelitePerson {id}");
			return Ok();
		}

		[Authorize]
        [HttpPatch("password")]
        public async Task<IResult> Patch(string oldPassword, string newPassword)
        {
			string? idStr = User?.FindFirst("id")?.Value;
			if (idStr == null) Results.BadRequest();
			int id = int.Parse(idStr);

            Authorization? auth = await db.Authorization.FirstOrDefaultAsync(x => x.Id == id);
            if (auth == null) return Results.BadRequest();
            if (auth.Password != oldPassword) return Results.BadRequest();
			auth.Password = newPassword;
			db.SaveChanges();
            return Results.Ok();
        }
		[Authorize]
		[HttpPatch]
		public async Task<IResult> Patch(Person newPerson)
		{
			string? idStr = User?.FindFirst("id")?.Value;
			if (idStr == null) Results.BadRequest();
			int id = int.Parse(idStr);

			Person? person = await db.Persons.FirstOrDefaultAsync(x => x.Id == id);
			if (person == null) return Results.BadRequest();
			newPerson.Id = id;
            person = newPerson;
			db.SaveChanges();
			return Results.Ok();
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
