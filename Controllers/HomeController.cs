using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using WebTutorCore.Data.Models;

namespace WebApp.Controllers
{
    //[Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Area(int altitude, int height)
        {
            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        }
    }
}