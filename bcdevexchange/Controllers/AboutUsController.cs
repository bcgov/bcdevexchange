using Microsoft.AspNetCore.Mvc;

namespace bcdevexchange.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
