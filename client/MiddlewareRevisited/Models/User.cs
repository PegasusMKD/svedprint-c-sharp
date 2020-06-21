using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models
{
    class User
    {
        [JsonProperty]
        public string firstName;
        [JsonProperty]
        public string middleName;
        [JsonProperty]
        public string lastName;
        [JsonProperty]
        public string id;
        [JsonProperty]
        public string username;
        [JsonProperty]
        public string password;
        [JsonProperty]
        public string token;
        [JsonProperty]
        public bool printAllowed;
        [JsonProperty]
        public School school;
        [JsonProperty]
        public SchoolClass schoolClass;
    }
}
