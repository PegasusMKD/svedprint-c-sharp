using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class SubjectOrientation
    {
        public string id { get; set; }
        public string shortName { get; set; }
        public string fullName { get; set; }
        public List<string> subjects { get; set; }
    }
}