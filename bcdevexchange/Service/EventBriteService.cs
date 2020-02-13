using bcdevexchange.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace bcdevexchange.Service
{
    public class EventBriteService : IEventBriteService
    {
        private readonly HttpClient client;
        private readonly string bearerToken;
        private readonly ILogger logger;
        public EventBriteService(ILogger<EventBriteService> logger) 
        {
            this.logger = logger;
            var executablePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var envfile = Path.Combine(executablePath, Constants.EnvPath);
            
            if (File.Exists(envfile))
            {
                DotNetEnv.Env.Load(envfile);
                Console.WriteLine($"Loading from environment file {envfile}");
            }
            client = new HttpClient();
            bearerToken = Environment.GetEnvironmentVariable("BEARER_TOKEN");
        }
        public async Task<IEnumerable<Event>> GetAllCoursesAsync()
        {
            var eveAll = await GetAllAsync();
            var courses = eveAll.Where(e => e.FormatId == Constants.CourseFormatId);
            logger.LogInformation($"Received Courses: {courses.Count()}");
            return courses;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var eveAll = await GetAllAsync();
            var events = eveAll.Where(e => e.FormatId != Constants.CourseFormatId);
            logger.LogInformation($"Received Events: {events.Count()}");
            return events;
        }

        private async Task<IEnumerable<Event>> GetAllAsync()
        {
            List<Event> events = new List<Event>();
            bool hasMoreItems = false;
            string continuationToken = string.Empty;

            do
            { 
                string baseUrl = "https://www.eventbriteapi.com/v3/organizations/228490647317/events/";

                if (hasMoreItems == true)
                {
                    baseUrl = baseUrl + $"?continuation={continuationToken}";
                }

                var req = new HttpRequestMessage(HttpMethod.Get, baseUrl);
                req.Headers.Add("Authorization", $"Bearer {bearerToken}");
                var httpResponse = await client.SendAsync(req);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    logger.LogError($"Cannot retrieve events: ${httpResponse.StatusCode}, ${httpResponse.ReasonPhrase}");
                    throw new Exception($"Cannot retrieve events: ${httpResponse.StatusCode}, ${httpResponse.ReasonPhrase}");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var eventBriteResponse = JsonConvert.DeserializeObject<EventBriteResponse>(content);

                hasMoreItems = eventBriteResponse.PageInfo.HasMoreItems;
                continuationToken = eventBriteResponse.PageInfo.Continuation;

                events.AddRange(eventBriteResponse.Events);
            }
            while (hasMoreItems);

            var filteredEvents = events.Where(e => e.Start.Utc >= DateTime.UtcNow.Date);
            var liveEvents = filteredEvents.Where(e => e.Status == Constants.LiveEventStatus);
            var nonSeriesEvents = liveEvents.Where(e => e.IsSeries == false).ToList();
            var seriesEvents = filteredEvents.Where(e => e.IsSeries == true).GroupBy(x => x.SeriesId).Select(y => y.OrderBy(x => x.Start.Utc).First()).ToList();
            seriesEvents.AddRange(nonSeriesEvents);
            return seriesEvents;
        }
    }
}
