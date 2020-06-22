using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class SchoolClass
    {
        public string id;
        public string name;
        public Year year;
        public List<Student> students;
        public List<SubjectOrientation> subjectOrientations;
    }
}