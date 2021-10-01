namespace MiddlewareRevisited.Models
{
    public class User
    {
        public string firstName;
        public string middleName;
        public string lastName;
        public string id;
        public string username;
        public bool printAllowed;
        public School school;
        [Newtonsoft.Json.JsonProperty("schoolClass")]
        public SchoolClass schoolClass;
    }
}
