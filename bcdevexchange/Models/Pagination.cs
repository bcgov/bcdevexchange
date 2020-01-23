using Newtonsoft.Json;

namespace bcdevexchange.Models
{
    public class Pagination
    {
        [JsonProperty ("object_count")]
        public int ObjectCount { get; set; }

        [JsonProperty("page_number")]
        public int PageNumber { get; set; }
        
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty("page_count")]
        public int PageCount { get; set; }

        [JsonProperty("continuation")]
        public string Continuation { get; set; }
        
        [JsonProperty("has_more_items")]
        public bool HasMoreItems { get; set; }
    }
}
