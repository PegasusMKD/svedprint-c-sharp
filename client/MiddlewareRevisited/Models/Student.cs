using Middleware;
using System;
using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class Student
    {
        public string id;
        public string gender;
        public string firstName;
        public string middleName;
        public string lastName;
        public Nullable<int> number; // workaround
        public string fathersName;
        public string mothersName;
        public DateTime dateOfBirth;
        public string placeOfResidence;
        public string placeOfBirth;
        public string timesStudiedYear;
        public string timesTakenExam;
        public string examMonth;
        public string passedExam;
        public string passedYear;
        public int justifiedAbsences;
        public int unjustifiedAbsences;
        public string examType;
        public string educationType;
        public string studentType;
        public string behaviorType;
        public string lastGradeYear;
        public string lastSchoolYearSuccessType;
        public int lastSchoolYear;
        public string lastSchoolName;
        public string lastBusinessNumber;
        public DateTime dateWhenTestimonyWasPrinted;
        public string citizenship;
        public string nameOfSchool;
        public bool printedTestimony;
        public OptionalSubjects[] optionalSubjects;
        public string diplomaBusinessNumber;
        public List<int> languages;
        public List<int> grades;
        public List<int> droppedGrades;
        public string country;
        public SubjectOrientation subjectOrientation;

        public Dictionary<string, string> GetPolinja()
        {
            Dictionary<string, string> polinja = new Dictionary<string, string>();
            polinja.Add("firstName", firstName);
            polinja.Add("middleName", middleName);
            polinja.Add("lastName", lastName);
            polinja.Add("subjectOrientationName", subjectOrientation.shortName);
            polinja.Add("fathersName", fathersName);
            polinja.Add("mothersName", mothersName);
            polinja.Add("number" , number.ToString());
            polinja.Add("gender" , gender);
            polinja.Add("dateOfBirth" , dateOfBirth.ToShortDateString());
            polinja.Add("placeOfBirth" , placeOfBirth);
            polinja.Add("placeOfResidence" , placeOfResidence);
            polinja.Add("timesStudiedYear" , timesStudiedYear);
            polinja.Add("passedYear" , passedYear);
            polinja.Add("behaviorType" , behaviorType);
            polinja.Add("justifiedAbsences", justifiedAbsences.ToString());
            polinja.Add("unjustifiedAbsences", unjustifiedAbsences.ToString());
            //polinja.Add("optionalSubjects", optionalSubjects);
            //polinja.Add(RequestParameters.pedagoshki_merki, _pedagoski_merki);
            polinja.Add("lastGradeYear", lastGradeYear);
            polinja.Add("lastSchoolName" , lastSchoolName);
            polinja.Add("lastSchoolYearSuccessType", lastSchoolYearSuccessType);
            polinja.Add("country", country);
            return polinja;
        }
    }
}