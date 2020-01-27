using Newtonsoft.Json;

namespace bcdevexchange.Models
{
    public class Event
    {
        [JsonProperty("name")]
        public EventBriteString Name { get; set; }

        [JsonProperty("description")]
        public EventBriteString Description { get; set; }

        [JsonProperty("logo")]
        public LogoImage Logo { get; set; }

        [JsonProperty("start")]
        public EventBriteDateTime Start { get; set; }

        [JsonProperty("end")]
        public EventBriteDateTime End { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("online_event")]
        public bool OnlineEvent { get; set; }

        [JsonProperty("is_free")]
        public bool IsFree { get; set; }

        [JsonProperty("venue_id")]
        public string VenueId { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("format_id")]
        public string FormatId { get; set; }

        [JsonProperty("series_id")]
        public string SeriesId { get; set; }

        [JsonProperty("is_series")]
        public bool IsSeries { get; set; }
    }
}
   