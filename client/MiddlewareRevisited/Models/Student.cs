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
        public int number;
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
    }
}