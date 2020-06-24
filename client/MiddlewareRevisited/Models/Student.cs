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
        public string optionalSubjects;
        public string diplomaBusinessNumber;
        public List<int> languages;
        public List<int> grades;
        public List<int> droppedGrades;
        public SubjectOrientation subjectOrientation;

        public Dictionary<string, string> GetPolinja()
        {
            Dictionary<string, string> polinja = new Dictionary<string, string>();
            polinja.Add(RequestParameters.ime, firstName);
            polinja.Add(RequestParameters.srednoIme, middleName);
            polinja.Add(RequestParameters.prezime, lastName);
            polinja.Add(RequestParameters.smer, subjectOrientation.shortName);
            polinja.Add(RequestParameters.tatko, fathersName);
            polinja.Add(RequestParameters.majka, mothersName);
            polinja.Add(RequestParameters.broj, number.ToString());
            polinja.Add(RequestParameters.gender, gender);
            polinja.Add(RequestParameters.roden, dateOfBirth.ToShortDateString());
            polinja.Add(RequestParameters.mesto_na_ragjanje, placeOfBirth);
            polinja.Add(RequestParameters.mesto_na_zhiveenje, placeOfResidence);
            polinja.Add(RequestParameters.pat_polaga, timesStudiedYear);
            polinja.Add(RequestParameters.polozhil, passedYear);
            polinja.Add(RequestParameters.povedenie, behaviorType);
            polinja.Add(RequestParameters.opravdani, justifiedAbsences.ToString());
            polinja.Add(RequestParameters.neopravdani, unjustifiedAbsences.ToString());
            polinja.Add(RequestParameters.proektni, optionalSubjects);
            //polinja.Add(RequestParameters.pedagoshki_merki, _pedagoski_merki);
            polinja.Add(RequestParameters.prethodna_godina, lastGradeYear);
            polinja.Add(RequestParameters.prethodno_uchilishte, lastSchoolName);
            polinja.Add(RequestParameters.prethoden_uspeh, lastSchoolYearSuccessType);
            return polinja;
        }
    }
}