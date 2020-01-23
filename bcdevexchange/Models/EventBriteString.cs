using Newtonsoft.Json;

namespace bcdevexchange.Models
{
    public class EventBriteString
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }
    }
}
