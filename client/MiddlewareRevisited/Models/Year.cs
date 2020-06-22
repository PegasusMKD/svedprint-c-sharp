using Newtonsoft.Json;
using System;

namespace MiddlewareRevisited.Models
{
    public class Year
    {
        [JsonProperty]
        public string id;
        [JsonProperty]
        public DateTime dateWhenTestimonyConfirmed;
        [JsonProperty]
        public string name;
    }
}