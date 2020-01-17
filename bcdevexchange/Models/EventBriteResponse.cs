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

    }
}
