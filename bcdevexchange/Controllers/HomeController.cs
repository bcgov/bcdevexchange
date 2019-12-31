using Microsoft.AspNetCore.Mvc;

namespace bcdevexchange.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
