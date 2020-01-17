using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            Dictionary<string, object> model = new Dictionary<string, object> { };
            var events =await Event.GetAllEventsAsync();
            model.Add("events", events);
            return View("Learning", model);
        }

    }
}
