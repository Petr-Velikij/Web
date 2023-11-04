using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
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

        [HttpPost]
        public IResult Create(Authorization aut)
        {
            Console.WriteLine("CreatrPerson");
            Authorization? person = db.Authorization.FirstOrDefault(x => x.Email == aut.Email);
            if (person == null)
            {
                db.Authorization.Add(aut);
                db.SaveChanges();
                Login(aut);
            }
            return Results.Unauthorized();
        }
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
        [HttpGet]
        public List<Authorization> Get()
        {
            Console.WriteLine("GetAllPerson");
            return db.Authorization.ToList();
        }
        [HttpPut]
        public IResult Login(Authorization aut)
        {
            Console.WriteLine("Login");
            var identity = GetIdentity(aut);
            if (identity == null) return Results.Unauthorized();
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
                username = identity.Name
            };
            return Results.Json(response);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DelitePerson");
            Authorization _person = db.Authorization.First(x => x.Id == id);
            db.Authorization.Remove(_person);
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
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, aut.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
