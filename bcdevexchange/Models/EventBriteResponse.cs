using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bcdevexchange.Models
{
    public class EventBriteResponse
    {
        [JsonProperty("pagination")]
        public Pagination PageInfo { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        public static async Task<EventBriteResponse> GetEventBriteResponseAsync()
        {
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("7XC6QWZD2N54OO7PDHIC");
            var req = new HttpRequestMessage(HttpMethod.Get, "https://www.eventbriteapi.com/v3/organizations/228490647317/events/");
            req.Headers.Add("Authorization", "Bearer 7XC6QWZD2N54OO7PDHIC");
            var httpResponse = await client.SendAsync(req);


            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var eventBriteResponse = JsonConvert.DeserializeObject<EventBriteResponse>(content);

            return eventBriteResponse;
        }
    }
}
