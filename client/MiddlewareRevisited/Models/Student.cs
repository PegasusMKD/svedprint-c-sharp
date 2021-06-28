using Middleware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MiddlewareRevisited.Models
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class PersonalDataAttribute : System.Attribute
    {
        private string friendlyName;

        public PersonalDataAttribute(string v)
        {
            friendlyName = v;
        }
    }
    public class Student : INotifyPropertyChanged
    {

        // properties
        public string id { get => _id; set => _id = value; }
        [PersonalData("Пол")]
        public string gender { get => _gender; set { if (_gender != value) { _gender = value; NotifyPropertyChanged(); } } } 

        [PersonalData("Име")]
        public string firstName { get => _firstName; set { if (_firstName != value) { _firstName = value; NotifyPropertyChanged(); } } }

        [PersonalData("Средно име")]
        public string middleName { get => _middleName; set { if (_middleName != value) { _middleName = value; NotifyPropertyChanged(); } } }

        [PersonalData("Презиме")]
        public string lastName { get => _lastName; set { if (_lastName != value) { _lastName = value; NotifyPropertyChanged(); } } }

        [PersonalData("Број во дневник")]
        public int? number { get => _number; set { if (_number != value) { _number = value; NotifyPropertyChanged(); } } }

        [PersonalData("Татково име")]
        public string fathersName { get => _fathersName; set { if (_fathersName != value) { _fathersName = value; NotifyPropertyChanged(); } } }

        [PersonalData("Мајкино име")]
        public string mothersName { get => _mothersName; set { if (_mothersName != value) { _mothersName = value; NotifyPropertyChanged(); } } }

        [PersonalData("Датум на раѓање")]
        public DateTime? dateOfBirth { get => _dateOfBirth ?? DateTime.Now; set { if (_dateOfBirth != value) { _dateOfBirth = value; NotifyPropertyChanged(); } } }

        [PersonalData("Место на живеење")]
        public string placeOfResidence { get => _placeOfResidence; set { if (_placeOfResidence != value) { _placeOfResidence = value; NotifyPropertyChanged(); } } }

        [PersonalData("Место на раѓање")]
        public string placeOfBirth { get => _placeOfBirth; set { if (_placeOfBirth != value) { _placeOfBirth = value; NotifyPropertyChanged(); } } }

        [PersonalData("По кој пат ја учи годината")]
        public string timesStudiedYear { get => _timesStudiedYear; set { if (_timesStudiedYear != value) { _timesStudiedYear = value; NotifyPropertyChanged(); } } }

        [PersonalData("По кој пат полага")]
        public string timesTakenExam { get => _timesTakenExam; set { if (_timesTakenExam != value) { _timesTakenExam = value; NotifyPropertyChanged(); } } }

        [PersonalData("Испитен термин")]
        public string examMonth { get => _examMonth; set { if (_examMonth != value) { _examMonth = value; NotifyPropertyChanged(); } } }

        [PersonalData("Положил испит?")]
        public string passedExam { get => _passedExam; set { if (_passedExam != value) { _passedExam = value; NotifyPropertyChanged(); } } }

        [PersonalData("Положил година???")]
        public string passedYear { get => _passedYear; set { if (_passedYear != value) { _passedYear = value; NotifyPropertyChanged(); } } }
        [PersonalData("Оправдани изостаноци")]
        public int? justifiedAbsences { get => _justifiedAbsences; set { if (_justifiedAbsences != value) { _justifiedAbsences = value; NotifyPropertyChanged(); } } }

        [PersonalData("Неоправдани изостаноци")]
        public int? unjustifiedAbsences { get => _unjustifiedAbsences; set { if (_unjustifiedAbsences != value) { _unjustifiedAbsences = value; NotifyPropertyChanged(); } } }

        [PersonalData("Тип на испит??")]
        public string examType { get => _examType; set { if (_examType != value) { _examType = value; NotifyPropertyChanged(); } } }

        [PersonalData("Тип на образование")]
        public string educationType { get => _educationType; set { if (_educationType != value) { _educationType = value; NotifyPropertyChanged(); } } }

        [PersonalData("Тип на ученик??")]
        public string studentType { get => _studentType; set { if (_studentType != value) { _studentType = value; NotifyPropertyChanged(); } } }

        [PersonalData("Поведение")]
        public string behaviorType { get => _behaviorType; set { if (_behaviorType != value) { _behaviorType = value; NotifyPropertyChanged(); } } }

        [PersonalData("Успех од претходна година")]
        public string lastGradeYear { get => _lastGradeYear; set { if (_lastGradeYear != value) { _lastGradeYear = value; NotifyPropertyChanged(); } } }

        //[PersonalData("тбх ме мрзи веќе")]
        public string lastSchoolYearSuccessType { get => _lastSchoolYearSuccessType; set { if (_lastSchoolYearSuccessType != value) { _lastSchoolYearSuccessType = value; NotifyPropertyChanged(); } } }

        //[PersonalData("Место на живеење")]
        public int? lastSchoolYear { get => _lastSchoolYear; set => _lastSchoolYear = value; }
        //[PersonalData("Место на живеење")]
        public string lastSchoolName { get => _lastSchoolName; set => _lastSchoolName = value; }
        //[PersonalData("Место на живеење")]
        public string lastBusinessNumber { get => _lastBusinessNumber; set => _lastBusinessNumber = value; }
        //[PersonalData("Место на живеење")]
        public DateTime? dateWhenTestimonyWasPrinted { get => _dateWhenTestimonyWasPrinted; set => _dateWhenTestimonyWasPrinted = value; }
        //[PersonalData("Место на живеење")]
        public string citizenship { get => _citizenship; set => _citizenship = value; }
        //[PersonalData("Место на живеење")]
        public string nameOfSchool { get => _nameOfSchool; set => _nameOfSchool = value; }
        //[PersonalData("Место на живеење")]
        public bool? printedTestimony { get => _printedTestimony; set => _printedTestimony = value; }
        //[PersonalData("Место на живеење")]
        public OptionalSubjects[] optionalSubjects { get => _optionalSubjects; set => _optionalSubjects = value; }
        //[PersonalData("Место на живеење")]
        public string diplomaBusinessNumber { get => _diplomaBusinessNumber; set => _diplomaBusinessNumber = value; }
        //[PersonalData("Место на живеење")]
        public List<int> languages { get => _languages; set => _languages = value; }
        //[PersonalData("Место на живеење")]
        public List<int> grades { get => _grades; set => _grades = value; }
        //[PersonalData("Место на живеење")]
        public List<int> droppedGrades { get => _droppedGrades; set => _droppedGrades = value; }
        //[PersonalData("Место на живеење")]
        public SubjectOrientation subjectOrientation { get => _subjectOrientation; set => _subjectOrientation = value; }
        public SchoolClass schoolClass { get => _schoolClass; set => _schoolClass = value; }
        public string country { get => _country; set => _country = value; }


        private string _id;
        private string _gender;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private int? _number;
        private string _fathersName;
        private string _mothersName;
        private DateTime? _dateOfBirth;
        private string _placeOfResidence;
        private string _placeOfBirth;
        private string _timesStudiedYear;
        private string _timesTakenExam;
        private string _examMonth;
        private string _passedExam;
        private string _passedYear;
        private int? _justifiedAbsences;
        private int? _unjustifiedAbsences;
        private string _examType;
        private string _educationType;
        private string _studentType;
        private string _behaviorType;
        private string _lastGradeYear;
        private string _lastSchoolYearSuccessType;
        private int? _lastSchoolYear;
        private string _lastSchoolName;
        private string _lastBusinessNumber;
        private DateTime? _dateWhenTestimonyWasPrinted;
        private string _citizenship;
        private string _nameOfSchool;
        private bool? _printedTestimony;
        private OptionalSubjects[] _optionalSubjects;
        private string _diplomaBusinessNumber;
        private List<int> _languages;
        private List<int> _grades;
        private List<int> _droppedGrades;
        private SubjectOrientation _subjectOrientation;
        private SchoolClass _schoolClass;
        private string _country;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}