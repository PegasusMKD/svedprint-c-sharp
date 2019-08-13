using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Models
{
    public static class Util
    {
        public static void UpdateObject<TYPE>(TYPE source, TYPE dest, List<string> exclude_fields = null)
        {
            Type T = typeof(TYPE);

            if (exclude_fields == null) exclude_fields = new List<string>();
            var properties = T.GetProperties().Where(p => !exclude_fields.Contains(p.Name));

            foreach (var p in properties)
            {
                var val = p.GetValue(source, null);
                if (val != null)
                {
                    p.SetValue(dest, val, null);
                }
            }
        }
    }
    public static class JSONRequestParameters
    {
        public const string Token = "token";

        public static class Admin
        {
            public const string Username = "user";
            public const string UsernameUpdated = "username";
            public const string Password = "pass";
            public const string PasswordUpdated = "password";
            public const string PrintAllowed = "printAllowed";
            public const string Uchilishte = "uchilishte";
            public const string DelovodenBroj = "delovoden_broj";
            public const string OdobrenoSveditelstvo = "odobreno_sveditelstvo";
            public const string Ministerstvo = "ministerstvo";
            public const string GlavnaKniga = "glavna_kniga";
            public const string Akt = "akt";
            public const string AktGodina = "akt_godina";
            public const string Direktor = "direktor";
            public const string UchilishteUpdate = "name";
            public const string PrintAllowedUpdated = "action";
        }

        public static class Klasen
        {
            public const string Ime = "first_name";
            public const string SrednoIme = "middle_name";
            public const string Prezime = "last_name";
            public const string Username = "username";
            public const string PasswordUpdated = "password";
            public const string Paralelka = "paralelka";
            public const string UsernameUpdated = "new_username";
            public const string Klas = "class";

        }

        public static class Ucenik
        {
            public const string Ime = "ime";
            public const string SrednoIme = "srednoIme";
            public const string Prezime = "prezime";
            public const string Paralelka = "paralelka";
            public const string Tatko = "tatko";
            public const string Majka = "majka";
            public const string Roden = "roden";
            public const string Smer = "smer";
            public const string CelSmer = "cel_smer";
            public const string Broj = "broj";
            public const string Pol = "gender";
            public const string PrethodnaGodina = "prethodna_godina";
            public const string PrethodenUspeh = "prethoden_uspeh";
            public const string Polozhil = "polozhil";
            public const string Counter = "ctr";
            public const string PolagalOceni = "polagal";
            public const string StudentsToUpdate = "students_to_update";
            public const string StudentsToUpdateIme = "name";
            public const string StudentsToUpdateSrednoIme = "middle_name";
            public const string StudentsToUpdatePrezime = "last_name";

        }

        public static class ErrorCodes
        {
            public const string OK = "000";
            public const string FAIL = "809";
            public const string TRANSFER_INIT_OK = "005";
            public const string BACK_UP_FAIL = "302";
        }
    }
}