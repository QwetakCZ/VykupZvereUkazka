using LuRa_Vykup.Models;

namespace LuRa_Vykup.ViewModels
{
    public class PokladniDokladySeznamViewModel
    {
        private PokladniDoklady _pd;

        public PokladniDokladySeznamViewModel(PokladniDoklady pd)
        {
            _pd = pd;
        }

        public int Id => _pd.Id;
        public string CisloDokladu => _pd.CisloDokladu;
        public DateOnly Datum => DateOnly.FromDateTime(_pd.Datum);
        public string Dodavatel => _pd.Dodavatele.Dodavatel;
        public string Cenik => _pd.Ceniky.Nazev;
        public string Mena => _pd.Ceniky.Mena.ToString();

        public string VykupnaNazev => _pd.Dodavatele.Vykupna.Nazev;
        public string VykupnaFirma => _pd.Dodavatele.Vykupna.Firma;
        public string VykupnaUlice => $"{_pd.Dodavatele.Vykupna.Ulice} {_pd.Dodavatele.Vykupna.CisloPopisne}, {_pd.Dodavatele.Vykupna.Mesto}";
        public string VykupnaIco => _pd.Dodavatele.Vykupna.Ico;


    }
}
