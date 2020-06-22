using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models
{
    public class User
    {
        public string firstName;
        public string middleName;
        public string lastName;
        public string id;
        public string username;
        public string password;
        public string token;
        public bool printAllowed;
        public School school;
        public SchoolClass schoolClass;
    }
}
