using Middleware;
using MiddlewareRevisited.Models.PrintModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareRevisited.Helpers;

namespace MiddlewareRevisited.Models.PrintModels
{
    public class ReportCardFront : GenericPrinter
    {

        public Font smallFont;
        public Font normalFont;
        public Font bigFont;

        private int nextSubjectDifference = 79;

       public ReportCardFront(ref Student student, ref User homeroomTeacher,string filename, string saveAs) : base(ref student, ref homeroomTeacher, filename, saveAs)
        {
            this.smallFont = new Font("arial", 20, FontStyle.Bold);
            this.normalFont = new Font("arial", 30, FontStyle.Bold);
            this.bigFont = new Font("arial", 40, FontStyle.Bold);
            resetData();
        }

        private void generateSubjectsData()
        {
            List<(int, int, string, Font, int, int)> tmpData = this.data.ToList();
            tmpData.Add((210, 1638, "ЗАДОЛЖИТЕЛНИ ПРЕДМЕТИ:", this.normalFont, 0, 0));
            int sumSubjects = 1722;
            for (int idx=0; idx<student.grades.Count; idx++)
            {
                if(idx == homeroomTeacher.schoolClass.year.numberOfMustSubjects - 1)
                {
                    tmpData.Add((210, sumSubjects, "ИЗБОРНИ ПРЕДМЕТИ:", this.normalFont, 0, 0));
                    sumSubjects += this.nextSubjectDifference;
                }
                tmpData.Add((210, sumSubjects, student.subjectOrientation.subjects[idx], this.normalFont, 0, 0));
                tmpData.Add((1554, sumSubjects, ((Grades)student.grades[idx]).getGradeLabel(), this.normalFont, 0, 0));
                tmpData.Add((2180, sumSubjects, student.grades[idx].ToString(), this.normalFont, 0, 0));
                sumSubjects += this.nextSubjectDifference;
            }
            generateOptionalSubjectsData(sumSubjects, tmpData);
        }

        private void generateOptionalSubjectsData(int sumSubjects, List<(int, int, string, Font, int, int)> tmpData)
        {
            if(student.optionalSubjects != null)
            {
                tmpData.Add((210, 1638, "ПРОЕКТНИ АКТИВНОСТИ ОД ОБЛАСТИТЕ:", this.normalFont, 0, 0));
                for (int idx = 0; idx < student.optionalSubjects.Length; idx++)
                {
                    if (!student.optionalSubjects[idx].print) continue;
                    sumSubjects += this.nextSubjectDifference;
                    tmpData.Add((210, sumSubjects, student.optionalSubjects[idx].name, this.normalFont, 0, 0));
                    tmpData.Add((1554, sumSubjects, student.optionalSubjects[idx].passed ? "реализирал" : "не реализирал", this.normalFont, 0, 0));
                }
            }
            this.data = tmpData.ToArray();
        }

        public override void resetData()
        {
            this.data = new (int x, int y, string value, Font font, int width, int height)[] {
                (x: 490, y: 455, value: homeroomTeacher.school.name, font: this.bigFont, width: 0, height: 0),
                (x: 580, y: 632, value: homeroomTeacher.school.city, font: this.normalFont, width: 0, height: 0),
                (x: 1750, y: 628, value: homeroomTeacher.school.mainBook, font: this.normalFont, width: 0, height: 0),
                (x: 2050, y: 628, value: DateTime.Now.Year.ToString(), font: this.normalFont, width: 0, height: 0), // TODO: Add parsing
                (x: 1160, y: 830, value: homeroomTeacher.schoolClass.year.name, font: this.bigFont, width: 0, height: 0),
                (x: 1085, y: 974, value: $"{student.firstName} {student.middleName} {student.lastName}", font: this.bigFont, width: 0, height: 0),
                (x: 730, y: 1103, value: student.fathersName != null ? student.fathersName : student.mothersName, font: this.normalFont, width: 0, height: 0),
                (x: 1820, y: 1103, value: student.dateOfBirth.ToShortDateString(), font: this.normalFont, width: 0, height: 0),
                (x: 670, y: 1205, value: student.placeOfBirth, font: this.normalFont, width: 0, height: 0),
                (x: 1760, y: 1205, value: student.placeOfBirth, font: this.normalFont, width: 0, height: 0),
                (x: 545, y: 1296, value: student.country, font: this.normalFont, width: 0, height: 0),
                (x: 1760, y: 1272, value: student.citizenship, font: this.smallFont, width: 500, height: 200),
                (x: 470, y: 1387, value: student.lastSchoolYear.ToString(), font: this.normalFont, width: 0, height: 0),
                (x: 625, y: 1387, value: DateTime.Now.Year.ToString(), font: this.normalFont, width: 0, height: 0), // TODO: Add parsing
                (x: 1140, y: 1387, value: student.timesStudiedYear, font: this.normalFont, width: 0, height: 0),
                (x: 1930, y: 1387, value: homeroomTeacher.schoolClass.year.name, font: this.normalFont, width: 0, height: 0)
            };

            generateSubjectsData();
        }
    }
}
