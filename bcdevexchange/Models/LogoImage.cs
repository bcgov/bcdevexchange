using Newtonsoft.Json;

namespace bcdevexchange.Models
{
    public class LogoImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
