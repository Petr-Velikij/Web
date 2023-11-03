using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebTutor.Controllers;
using WebTutor.Data;

namespace WebTutor
{
    public class Program
    {
        public static string MyAllowSpecificOrigins = "Test";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PersonContext>(bd => bd.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
            builder.Services.AddDbContext<LessonContext>(bd => bd.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://25.75.246.82:44405")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ���������, ����� �� �������������� �������� ��� ��������� ������
                    ValidateIssuer = true,
                    // ������, �������������� ��������
                    ValidIssuer = AuthOptions.ISSUER,
                    // ����� �� �������������� ����������� ������
                    ValidateAudience = true,
                    // ��������� ����������� ������
                    ValidAudience = AuthOptions.AUDIENCE,
                    // ����� �� �������������� ����� �������������
                    ValidateLifetime = true,
                    // ��������� ����� ������������
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // ��������� ����� ������������
                    ValidateIssuerSigningKey = true,
                };
            });
            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            app.Map("/login/{username}", (string username) =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            });

            app.MapFallbackToFile("index.html");

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // �������� ������
        public const string AUDIENCE = "MyAuthClient"; // ����������� ������
        const string KEY = "b92%[bZ/bc%74JxKH-{e";   // ���� ��� ��������
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}