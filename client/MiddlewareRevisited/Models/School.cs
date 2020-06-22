using System;
using System.Collections.Generic;

namespace MiddlewareRevisited.Models
{
    public class School
    {
        public string id;
        public string name;
        public string actNumber;
        public DateTime actDate;
        public string directorName;
        public string businessNumber;
        public string mainBook;
        public string ministry;
        public string country;
        public string city;
        public int lastDigitsOfYear;
        public List<DateTime> printDatesForDiploma;
        public List<DateTime> printDatesForTestimony;
    }
}