using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
