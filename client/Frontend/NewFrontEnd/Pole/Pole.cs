namespace Frontend.NewFrontEnd.Pole
{
    public class Pole
    {
        public string Ime;
        public string RequestParametar;
        public string[] DefaultVrednost;
        public string Odgovor;
        public bool isCB = false;

        public Pole(string ime, string requestparameter, string[] defaultvrednost, string odgovor = "")
        {
            Ime = ime;
            RequestParametar = requestparameter;
            DefaultVrednost = defaultvrednost;
            if (odgovor != "") Odgovor = odgovor;
            if (defaultvrednost.Length > 1) isCB = true;
        }

        public string GetOdgovor()
        {

            if (Odgovor == "" || Odgovor == null) return DefaultVrednost[0];
            else return Odgovor;
        }

    }
}
