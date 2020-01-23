using Newtonsoft.Json;
using System;

namespace bcdevexchange.Models
{
    public class EventBriteDateTime
    {
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("local")]
        public DateTime Local { get; set; }

        [JsonProperty("utc")]
        public DateTime Utc { get; set; }
    }
}
