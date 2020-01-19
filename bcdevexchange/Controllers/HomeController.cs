using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using bcdevexchange.Service;

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
            IEventBriteService eventBriteObj = new EventBriteService();
            var events = await eventBriteObj.GetAllEventsAsync();
            var courses = await eventBriteObj.GetAllCoursesAsync();
            model.Add("events", events);
            model.Add("courses", courses);
            return View("Learning", model);
        }

    }
}
