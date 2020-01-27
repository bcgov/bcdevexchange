using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace bcdevexchange.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache memoryCache;
        private IEventBriteService eventBriteService;

        public HomeController(IMemoryCache cache, IEventBriteService service)
        {
            this.memoryCache = cache;
            this.eventBriteService = service;
        }

        public async Task<IList<Event>> GetFromCache(string key)
        {
            if (!memoryCache.TryGetValue(key, out List<Event> events))
            {
                IEnumerable<Event> result;

                if (key == Constants.CoursesKey)
                {
                    result = await this.eventBriteService.GetAllCoursesAsync();
                }
                else if (key == Constants.EventsKey)
                {
                    result = await this.eventBriteService.GetAllEventsAsync();
                }
                else
                {
                    throw new ArgumentException($"invalid key {key}");
                }

                events = result.ToList();


                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(Constants.ExpirationTimeInMinutes));

                // Save data in cache.
                memoryCache.Set(key, events, cacheEntryOptions);
            }

            return events;
        }
    
        public IActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
        [HttpGet("ExchangeLab")]
        public IActionResult ExchangeLab()
        {
            return View("ExchangeLab");
        }

        [HttpGet("AboutUs")]
        public IActionResult AboutUs()
        {
            return View("AboutUs");
=======
        [HttpGet("learning")]
        public async Task<IActionResult> GetEvents()
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            var events = await this.GetFromCache(Constants.EventsKey);
            var courses = await this.GetFromCache(Constants.CoursesKey);
            model.Add("events", events);
            model.Add("courses", courses);
            return View("Learning", model);
>>>>>>> e179a92355d18ef935419faeb530f301aafc3e81
        }

    }
}
