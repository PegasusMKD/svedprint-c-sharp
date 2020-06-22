using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class School
    {
        [JsonProperty]
        public string id;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string actNumber;
        [JsonProperty]
        public DateTime actDate;
        [JsonProperty]
        public string directorName;
        [JsonProperty]
        public string businessNumber;
        [JsonProperty]
        public string mainBook;
        [JsonProperty]
        public string ministry;
        [JsonProperty]
        public string country;
        [JsonProperty]
        public string city;
        [JsonProperty]
        public int lastDigitsOfYear;
        [JsonProperty]
        public List<DateTime> printDatesForDiploma;
        [JsonProperty]
        public List<DateTime> printDatesForTestimony;
    }
}