using Microsoft.AspNetCore.Mvc;

namespace KaryeraPortal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
