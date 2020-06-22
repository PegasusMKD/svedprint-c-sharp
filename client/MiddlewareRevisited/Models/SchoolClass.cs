using Newtonsoft.Json;
using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class SchoolClass
    {
        [JsonProperty]
        public string id;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public Year year;
        [JsonProperty]
        public List<Student> students;
        [JsonProperty]
        public List<SubjectOrientation> subjectOrientations;

    }
}