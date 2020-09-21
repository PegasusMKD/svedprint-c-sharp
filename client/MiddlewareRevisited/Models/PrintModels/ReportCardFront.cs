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
    public class ReportCardFront : ICustomImage
    {
        public Image combined { get; set; }
        public Image mainPage { get; set; }
        public Image background { get; set; }

        public Student student;
        public User homeroomTeacher;

        public Font smallFont;
        public Font normalFont;
        public Font bigFont;

        private int nextSubjectDifference = 79;

        public string saveAs; // TODO: Remove later

        public (int x, int y, string value, Font font)[] data { get; set; }

       public ReportCardFront(ref Student student, ref User homeroomTeacher, string filename, string saveAs)
        {
            this.student = student;
            this.homeroomTeacher = homeroomTeacher;
            this.background = Image.FromFile(filename);
            this.combined = Helpers.GetEmptyImage(2481, 3508);
            this.smallFont = new Font("arial", 20, FontStyle.Bold);
            this.normalFont = new Font("arial", 30, FontStyle.Bold);
            this.bigFont = new Font("arial", 40, FontStyle.Bold);
            this.saveAs = saveAs;
            resetData();
        }

        private void generateSubjectsData()
        {
            List<(int, int, string, Font)> tmpData = this.data.ToList();
            tmpData.Add((210, 1638, "ЗАДОЛЖИТЕЛНИ ПРЕДМЕТИ:", this.normalFont));
            int sumSubjects = 1722;
            for (int idx=0; idx<student.grades.Count; idx++)
            {
                if(idx == homeroomTeacher.schoolClass.year.numberOfMustSubjects - 1)
                {
                    tmpData.Add((210, sumSubjects, "ИЗБОРНИ ПРЕДМЕТИ:", this.normalFont));
                    sumSubjects += this.nextSubjectDifference;
                }
                tmpData.Add((210, sumSubjects, student.subjectOrientation.subjects[idx], this.normalFont));
                tmpData.Add((1554, sumSubjects, ((Grades)student.grades[idx]).getGradeLabel(), this.normalFont));
                tmpData.Add((2180, sumSubjects, student.grades[idx].ToString(), this.normalFont));
                sumSubjects += this.nextSubjectDifference;
            }
            generateOptionalSubjectsData(sumSubjects, tmpData);
        }

        private void generateOptionalSubjectsData(int sumSubjects, List<(int, int, string, Font)> tmpData)
        {
            if(student.optionalSubjects != null)
            {
                tmpData.Add((210, 1638, "ПРОЕКТНИ АКТИВНОСТИ ОД ОБЛАСТИТЕ:", this.normalFont));
                for (int idx = 0; idx < student.optionalSubjects.Length; idx++)
                {
                    if (!student.optionalSubjects[idx].print) continue;
                    sumSubjects += this.nextSubjectDifference;
                    tmpData.Add((210, sumSubjects, student.optionalSubjects[idx].name, this.normalFont));
                    tmpData.Add((1554, sumSubjects, student.optionalSubjects[idx].passed ? "реализирал" : "не реализирал", this.normalFont));
                }
            }
            this.data = tmpData.ToArray();
        }

        public virtual void resetData()
        {
            this.data = new (int x, int y, string value, Font font)[] {
                (x: 490, y: 455, value: homeroomTeacher.school.name, font: this.bigFont),
                (x: 580, y: 632, value: homeroomTeacher.school.city, font: this.normalFont),
                (x: 1750, y: 628, value: homeroomTeacher.school.mainBook, font: this.normalFont),
                (x: 2050, y: 628, value: DateTime.Now.Year.ToString(), font: this.normalFont), // TODO: Add parsing
                (x: 1160, y: 830, value: homeroomTeacher.schoolClass.year.name, font: this.bigFont),
                (x: 1085, y: 974, value: $"{student.firstName} {student.middleName} {student.lastName}", font: this.bigFont),
                (x: 730, y: 1103, value: student.fathersName != null ? student.fathersName : student.mothersName, font: this.normalFont),
                (x: 1820, y: 1103, value: student.dateOfBirth.ToShortDateString(), font: this.normalFont),
                (x: 670, y: 1205, value: student.placeOfBirth, font: this.normalFont),
                (x: 1760, y: 1205, value: student.placeOfBirth, font: this.normalFont),
                (x: 545, y: 1296, value: student.country, font: this.normalFont),
                (x: 1760, y: 1272, value: student.citizenship, font: this.smallFont),
                (x: 470, y: 1387, value: student.lastSchoolYear.ToString(), font: this.normalFont),
                (x: 625, y: 1387, value: DateTime.Now.Year.ToString(), font: this.normalFont), // TODO: Add parsing
                (x: 1140, y: 1387, value: student.timesStudiedYear, font: this.normalFont),
                (x: 1930, y: 1387, value: homeroomTeacher.schoolClass.year.name, font: this.normalFont)
            };

            generateSubjectsData();
        }
        
        public Image generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false)
        {
            this.mainPage = Helpers.GetEmptyImage(this.background.Width, this.background.Height);
            using (var graphics = Graphics.FromImage(this.mainPage))
            {
                if(appending) graphics.Clear(Color.Transparent);

                foreach ((int x, int y, string value, Font font ) val in this.data)
                {
                    graphics.DrawString(val.value, val.font, Brushes.Black, val.x + horizontalMargin, val.y + verticalMargin);
                }
            }
            return this.mainPage;
        }


        public Image generatePreview(int horizontalMargin, int verticalMargin)
        {
            generateMainPage(horizontalMargin, verticalMargin, true);
            using (var canvas = Graphics.FromImage(this.combined))
            {
                canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(this.background, new Rectangle(0, 0, this.combined.Width, this.combined.Height),
                    new Rectangle(0, 0, this.background.Width, this.background.Height), GraphicsUnit.Pixel);
                canvas.DrawImage(this.mainPage, new Rectangle(0, 0, this.combined.Width, this.combined.Height),
                    new Rectangle(0, 0, this.mainPage.Width, this.mainPage.Height), GraphicsUnit.Pixel);
                canvas.Save();
            }

            this.combined.Save(saveAs, ImageFormat.Jpeg);
            return this.combined;
        }
    }
}
