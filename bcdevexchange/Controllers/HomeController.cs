using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;
using System.Threading.Tasks;

namespace bcdevexchange.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("learning")]
        public async Task<IActionResult> GetEvents()
        {
            var events =await EventBriteResponse.GetEventBriteResponseAsync();
            return View("Learning",events);
        }

    }
}
