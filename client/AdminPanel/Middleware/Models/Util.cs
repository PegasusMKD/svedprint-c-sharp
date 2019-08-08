using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Models
{
    class JSONRequestParameters
    {
        public static readonly string Token = "token";

        public class Admin
        {
            public static readonly string Username = "user";
            public static readonly string UsernameUpdated = "username";
            public static readonly string Password = "pass";
            public static readonly string PasswordUpdated = "password";
            public static readonly string PrintAllowed = "printAllowed";
            public static readonly string Uchilishte = "uchilishte";
            public static readonly string DelovodenBroj = "delovoden_broj";
            public static readonly string OdobrenoSveditelstvo = "odobreno_sveditelstvo";
            public static readonly string Ministerstvo = "ministerstvo";
            public static readonly string GlavnaKniga = "glavna_kniga";
            public static readonly string Akt = "akt";
            public static readonly string AktGodina = "akt_godina";
            public static readonly string Direktor = "direktor";
            public static readonly string UchilishteUpdate = "name";
            public static readonly string PrintAllowedUpdated = "action";
        }

        public class Klasen
        {
            public static readonly string Ime = "first_name";
            public static readonly string SrednoIme = "middle_name";
            public static readonly string Prezime = "last_name";
            public static readonly string Username = "username";
            public static readonly string PasswordUpdated = "password";
            public static readonly string Paralelka = "paralelka";
            public static readonly string UsernameUpdated = "new_username";
            public static readonly string Klas = "class";

        }

        public class Ucenik
        {
            public static readonly string Ime = "ime";
            public static readonly string SrednoIme = "srednoIme";
            public static readonly string Prezime = "prezime";
            public static readonly string Paralelka = "paralelka";
            public static readonly string Tatko = "tatko";
            public static readonly string Majka = "majka";
            public static readonly string Roden = "roden";
            public static readonly string Smer = "smer";
            public static readonly string CelSmer = "cel_smer";
            public static readonly string Broj = "broj";
            public static readonly string Pol = "gender";
            public static readonly string PrethodnaGodina = "prethodna_godina";
            public static readonly string PrethodenUspeh = "prethoden_uspeh";
            public static readonly string Polozhil = "polozhil";
            public static readonly string Counter = "ctr";
            public static readonly string PolagalOceni = "polagal";
            public static readonly string StudentsToUpdate = "students_to_update";
            public static readonly string StudentsToUpdateIme = "name";
            public static readonly string StudentsToUpdateSrednoIme = "middle_name";
            public static readonly string StudentsToUpdatePrezime = "last_name";
            
        }

        public class ErrorCodes
        {
            public static readonly string OK = "000";
            public static readonly string FAIL = "809";
            public static readonly string TRANSFER_INIT_OK = "005";
            public static readonly string BACK_UP_FAIL = "302";
        }
    }
}
