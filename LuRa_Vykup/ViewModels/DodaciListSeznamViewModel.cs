using LuRa_Vykup.Models;

namespace LuRa_Vykup.ViewModels
{
    public class DodaciListSeznamViewModel
    {
        private DodaciListy _dl;

        public DodaciListSeznamViewModel(DodaciListy dl)
        {
            _dl = dl;
        }

        public int Id => _dl.Id;

        public string Cislo => _dl.CisloDL;

        public DateOnly Datum => DateOnly.FromDateTime(_dl.DatumVystaveni ?? DateTime.Now);

        public string Dodavatel => _dl.Dodavatele.Dodavatel;

        public string Cenik => _dl.Dodavatele.Ceniky.Nazev;

        public string Mena => _dl.Dodavatele.Ceniky.Mena.ToString();
    }
}
