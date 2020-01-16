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
        EventBriteString Description { get; set; }

        [JsonProperty("start")]
        EventBriteDateTime Start { get; set; }

        [JsonProperty("end")]
        EventBriteDateTime End { get; set; }

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
    }
}
    