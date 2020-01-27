using Microsoft.AspNetCore.Mvc;

namespace bcdevexchange.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("DevOpsPlatform")]
        public IActionResult DevOpsPlatform()
        {
            return View("DevOpsPlatform");
        }

        [HttpGet("ExchangeLab")]
        public IActionResult ExchangeLab()
        {
            return View("ExchangeLab");
        }

        [HttpGet("AboutUs")]
        public IActionResult AboutUs()
        {
            return View("AboutUs");
        }

    }
}
