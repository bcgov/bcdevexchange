using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace bcdevexchange.Models
{
    public class Event
    {
        [JsonProperty("name")]
        public EventBriteString Name { get; set; }

        [JsonProperty("description")]
        public EventBriteString Description { get; set; }

        [JsonProperty("start")]
        public EventBriteDateTime Start { get; set; }

        [JsonProperty("end")]
        public EventBriteDateTime End { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("online_event")]
        public static bool OnlineEvent { get; set; }

        [JsonProperty("is_free")]
        public static bool IsFree { get; set; }

        [JsonProperty("venue_id")]
        public static string VenueId { get; set; }

        [JsonProperty("category_id")]
        public static string CategoryId { get; set; }

        [JsonProperty("subcategory_id")]
        public static string SubcategoryId { get; set; }

        public static async Task<List<Event>> GetAllEventsAsync()
        {
            List<Event> events = new List<Event>();
            bool hasMoreItems = false;
            string continuationToken = string.Empty;

            do
            {
                var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("7XC6QWZD2N54OO7PDHIC");
                string baseUrl = "https://www.eventbriteapi.com/v3/organizations/228490647317/events/";
                if (hasMoreItems == true)
                {
                    baseUrl = baseUrl + $"?continuation={continuationToken}"; 
                }
                var req = new HttpRequestMessage(HttpMethod.Get, baseUrl);
                req.Headers.Add("Authorization", "Bearer 7XC6QWZD2N54OO7PDHIC");
                var httpResponse = await client.SendAsync(req);


                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve events");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var eventBriteResponse = JsonConvert.DeserializeObject<EventBriteResponse>(content);
                hasMoreItems = eventBriteResponse.PageInfo.HasMoreItems;
                continuationToken = eventBriteResponse.PageInfo.Continuation;

                events.AddRange(eventBriteResponse.Events);
            }
            while (hasMoreItems);

            return events;
        }
    }
}
   