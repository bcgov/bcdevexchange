using bcdevexchange.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bcdevexchange.Service
{
    public class EventBriteService : IEventBriteService
    {
        private HttpClient client = new HttpClient();
        private string bearertoken = Environment.GetEnvironmentVariable("BEARER_TOKEN");

        public async Task<IEnumerable<Event>> GetAllCoursesAsync()
        {
            var eveAll = await GetAllAsync();
            var courses = eveAll.Where(e => e.FormatId == "9");
            return courses;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var eveAll = await GetAllAsync();
            var events = eveAll.Where(e => e.FormatId != "9");
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
                req.Headers.Add("Authorization", $"Bearer {bearertoken}");
                var httpResponse = await client.SendAsync(req);

                if (!httpResponse.IsSuccessStatusCode)
                {
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
            var nonSeriesEvents = filteredEvents.Where(e => e.IsSeries == false).ToList();
            var seriesEvents = filteredEvents.Where(e => e.IsSeries == true).GroupBy(x => x.SeriesId).Select(y => y.OrderBy(x => x.Start.Utc).First()).ToList();
            seriesEvents.AddRange(nonSeriesEvents);
            return seriesEvents;
        }
    }
}
