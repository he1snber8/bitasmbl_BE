using Microsoft.AspNetCore.Mvc;

namespace Project_Backend_2024.Controllers.AdminControllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
