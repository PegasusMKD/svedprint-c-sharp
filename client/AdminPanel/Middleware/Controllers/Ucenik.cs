using AdminPanel.Middleware.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Controllers
{
    class Ucenik
    {
        public static List<Models.Ucenik> RetrieveStudents(Models.Admin admin, string year = "III")
        {
            List<Models.Ucenik> tmp;
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token },
                {JSONRequestParameters.Ucenik.Godina, year }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.RetrieveStudentsRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            try
            {
                tmp = JsonConvert.DeserializeObject<List<Models.Ucenik>>(response.responseText);
            }
            catch (Exception ex)
            {
                tmp = null;
                throw ex;
            }

            return tmp;
        }

        /// <summary>
        /// A Controller which updates the selected student's class
        /// </summary>
        /// <param name="students">List of students</param>
        /// <param name="admin"></param>
        /// <param name="_class">Name of the class they should be put into</param>
        public static void TransferStudentsClasses(List<Models.Ucenik> students, Models.Admin admin, string _class)
        {
            var students_for_transfer = from student in students where student.transferClass select student;
            List<Dictionary<string,string>> transfering = new List<Dictionary<string, string>>();
            foreach(Models.Ucenik student in students_for_transfer)
            {

                transfering.Add(new Dictionary<string, string>
                {
                    { "first_name",student.Ime},
                    { "middle_name",student.SrednoIme},
                    { "last_name",student.Prezime},
                    { "ctr",student.Counter},
                    {"paralelka", student.Paralelka }
                });

                student.transferClass = false;
                student.Paralelka = _class;
            }
            var json = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {JSONRequestParameters.Token ,admin.Token},
                {JSONRequestParameters.Ucenik.Paralelka, _class },
                {JSONRequestParameters.Ucenik.StudentsForUpdate, transfering }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateStudentsClasses);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

        }
    }
}
