using Newtonsoft.Json;
using System.Collections.Generic;

namespace bcdevexchange.Models
{
    public class EventBriteResponse
    {
        [JsonProperty("pagination")]
        public Pagination PageInfo { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

    }
}
