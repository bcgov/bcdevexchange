using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace bcdevexchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IMemoryCache memoryCache;
        private readonly IEventBriteService eventBriteService;

        public HomeController(IMemoryCache cache, IEventBriteService service, ILogger<HomeController> logger)
        {
            this.memoryCache = cache;
            this.eventBriteService = service;
            this.logger = logger;
        }

        private async Task<IList<Event>> GetFromCache(string key)
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
                    logger.LogInformation($"The key is invalid {key}");
                    throw new ArgumentException($"invalid key {key}");
                }

                events = result.ToList();
                logger.LogInformation($"The number of {key} received: {events.Count}");

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

        [HttpGet("learning")]
        public async Task<IActionResult> GetEvents()
        {
            Dictionary<string, IList<Event>> model = new Dictionary<string, IList<Event>> { };
            var events = await this.GetFromCache(Constants.EventsKey);
            var courses = await this.GetFromCache(Constants.CoursesKey);
            model.Add("events", events);
            model.Add("courses", courses);
            return View("Learning", model);
        }

        [AllowAnonymous]
        public IActionResult Error(int? code)
        {
            var path = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            if (code == null && path != null)
            {
                logger.LogError($"Exception thrown {path.Error} at {path.Path}");
                code = 500;
            }
            else
            {
                logger.LogError($"General error with code {code}");
            }
            return View(code);
        }
    }
}
