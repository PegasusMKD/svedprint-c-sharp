using Middleware;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiddlewareRevisited.Models
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class PersonalDataAttribute : System.Attribute
    {
        
    }
    public class Student
    {
        public string id { get; set; }
        public string gender { get; set; }
        [PersonalData]
        public string firstName { get; set; }
        [PersonalData]
        public string middleName { get; set; }
        [PersonalData]
        public string lastName { get; set; }
        [PersonalData]
        public Nullable<int> number { get; set; }
        [PersonalData]
        public string fathersName { get; set; }
        [PersonalData]
        public string mothersName { get; set; }
        [PersonalData]
        public DateTime dateOfBirth { get; set; }
        public string placeOfResidence { get; set; }
        public string placeOfBirth { get; set; }
        public string timesStudiedYear { get; set; }
        public string timesTakenExam { get; set; }
        public string examMonth { get; set; }
        public string passedExam { get; set; }
        public string passedYear { get; set; }
        public int justifiedAbsences { get; set; }
        public int unjustifiedAbsences { get; set; }
        public string examType { get; set; }
        public string educationType { get; set; }
        public string studentType { get; set; }
        public string behaviorType { get; set; }
        public string lastGradeYear { get; set; }
        public string lastSchoolYearSuccessType { get; set; }
        public int lastSchoolYear { get; set; }
        public string lastSchoolName { get; set; }
        public string lastBusinessNumber { get; set; }
        public DateTime dateWhenTestimonyWasPrinted { get; set; }
        public string citizenship { get; set; }
        public string nameOfSchool { get; set; }
        public bool printedTestimony { get; set; }
        public string optionalSubjects { get; set; }
        public string diplomaBusinessNumber { get; set; }
        public List<int> languages { get; set; }
        public List<int> grades { get; set; }
        public List<int> droppedGrades { get; set; }
        public SubjectOrientation subjectOrientation { get; set; }

        public Dictionary<string, string> GetPolinja()
        {
            Dictionary<string, string> polinja = new Dictionary<string, string>();
            polinja.Add("firstName", firstName);
            polinja.Add("middleName", middleName);
            polinja.Add("lastName", lastName);
            polinja.Add("subjectOrientationName", subjectOrientation.shortName);
            polinja.Add("fathersName", fathersName);
            polinja.Add("mothersName", mothersName);
            polinja.Add("number", number.ToString());
            polinja.Add("gender", gender);
            polinja.Add("dateOfBirth", dateOfBirth.ToShortDateString());
            polinja.Add("placeOfBirth", placeOfBirth);
            polinja.Add("placeOfResidence", placeOfResidence);
            polinja.Add("timesStudiedYear", timesStudiedYear);
            polinja.Add("passedYear", passedYear);
            polinja.Add("behaviorType", behaviorType);
            polinja.Add("justifiedAbsences", justifiedAbsences.ToString());
            polinja.Add("unjustifiedAbsences", unjustifiedAbsences.ToString());
            polinja.Add("optionalSubjects", optionalSubjects);
            //polinja.Add(RequestParameters.pedagoshki_merki, _pedagoski_merki);
            polinja.Add("lastGradeYear", lastGradeYear);
            polinja.Add("lastSchoolName", lastSchoolName);
            polinja.Add("lastSchoolYearSuccessType", lastSchoolYearSuccessType);
            return polinja;
        }
    }
}