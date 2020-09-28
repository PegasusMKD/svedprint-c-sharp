using MiddlewareRevisited.Models.PrintModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    class ReportCardBack : GenericPrinter
    {
        public Font smallFont;
        public Font normalFont;
        public Font bigFont;

        public ReportCardBack(ref Student student, ref User homeroomTeacher, string filename, string saveAs) : base(ref student,ref homeroomTeacher, filename, saveAs)
        {
            this.smallFont = new Font("arial", 20, FontStyle.Bold);
            this.normalFont = new Font("arial", 30, FontStyle.Bold);
            this.bigFont = new Font("arial", 40, FontStyle.Bold);
            resetData();
        }

        public override void resetData()
        {
            double average = 0;
            if (student.grades.Count != 0) average = student.grades.Average();
            this.data = new (int x, int y, string value, Font font, int width, int height)[] {
                (x: 740, y: 304, value: student.behaviorType, font: this.normalFont, width: 0, height: 0),
                (x: 2170, y: 304, value: student.justifiedAbsences.ToString(), font: this.normalFont, width: 0, height: 0),
                (x: 2170, y: 384, value: student.unjustifiedAbsences.ToString(), font: this.normalFont, width: 0, height: 0),
                (x: 1610, y: 624, value: homeroomTeacher.schoolClass.year.name, font: this.bigFont, width: 0, height: 0),
                (x: 420, y: 784, value: student.studentType, font: this.bigFont, width: 0, height: 0),
                (x: 1370, y: 784, value: student.subjectOrientation.fullName, font: this.bigFont, width: 0, height: 0),
                (x: 420, y: 940, value: "/////", font: this.bigFont, width: 0, height: 0),
                (x: 1840, y: 917, value: average != 0 ? $"{((Grades)Math.Round(average)).getGradeLabel()}({Math.Round(average,2)})" : "", font: this.bigFont, width: 0, height: 0),
                (x: 320, y: 1105, value: homeroomTeacher.school.city, font: this.bigFont, width: 0, height: 0),
                (x: 920, y: 1105, value: homeroomTeacher.schoolClass.year.dateWhenTestimonyConfirmed.ToShortDateString(), font: this.bigFont, width: 0, height: 0),
                (x: 1960, y: 1105, value: student.lastBusinessNumber, font: this.bigFont, width: 0, height: 0),
                (x: 320, y: 1720, value: $"{homeroomTeacher.firstName} {homeroomTeacher.middleName} {homeroomTeacher.lastName}", font: this.bigFont, width: 0, height: 0),
                (x: 1770, y: 1720, value: homeroomTeacher.school.directorName, font: this.bigFont, width: 0, height: 0),
                (x: 1180, y: 2615, value: homeroomTeacher.school.actNumber, font: this.bigFont, width: 0, height: 0),
                (x: 1860, y: 2615, value: homeroomTeacher.school.actDate.ToShortDateString(), font: this.bigFont, width: 0, height: 0),
                (x: 620, y: 2712, value: homeroomTeacher.school.ministry, font: this.bigFont, width: 0, height: 0),
            };
            
        }
    }
}
