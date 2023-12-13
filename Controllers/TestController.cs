using Microsoft.AspNetCore.Mvc;
using WebTutor.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace WebTutor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Test")]
    public class TestController : ControllerBase
    {
        private readonly String ip;
        private readonly ApplicationContext db;
        public TestController(ApplicationContext db)
        {
            ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].MapToIPv4().ToString(); ;
            this.db = db;
        }
        [Authorize]
        [HttpPost]
        [RequestSizeLimit(209715200)]
        public async Task<List<String>> Upload(IFormFileCollection files)
        {
            Console.WriteLine($"Post file {files.Count}");
            //var uploadPath = $"{Directory.GetCurrentDirectory()}/wwwroot/file";
            //Directory.CreateDirectory($"{uploadPath}/id={User?.FindFirst("id")?.Value}");
            List<String> path = new();
            long ticks = DateTime.UtcNow.Ticks;
            String uploadPath = $"{Directory.GetCurrentDirectory()}/wwwroot/file";
            String identifier = $"id={User?.FindFirst("id")?.Value}/{ticks}";
            Directory.CreateDirectory($"{uploadPath}/{identifier}");
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    Console.WriteLine($"Start load file {file.FileName}");
                    string filePath = $"{identifier}/{file.FileName}";
                    using (var fs = new FileStream($"{uploadPath}/{filePath}", FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }
                    Console.WriteLine($"End load file {file.FileName}");
                    path.Add($"https://{ip}:7045/file/{filePath}");
                }
            }
            return path;
        }
        /*
        public async Task<ActionResult> Domload()
        {
            string filename = "638379774682447358.png";
            string filepath = $"{Directory.GetCurrentDirectory()}/wwwroot/file/1/{filename}";
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType;

            _ = new FileExtensionContentTypeProvider().TryGetContentType(filepath, out contentType);

            return File(filedata, contentType, "lolkek.png", false);
        }
        */
    }
}