using LuRa_Vykup.Models;

namespace LuRa_Vykup.ExportModels
{
    public class ExportPokladniDoklady
    {
        private Vykup _vykup;

        public ExportPokladniDoklady(Vykup vykup)
        {
            _vykup = vykup;
        }

        public string Plomba => _vykup.Plomba;

        public decimal Vaha => _vykup.Vaha;

        public string Druh => _vykup.Druh.ToString();

        public string Kategorie => _vykup.Kategorie.ToString();

        public decimal Cena => _vykup.CenaKgPokladniDoklad ?? 0;

        public decimal CenaCelkem => Math.Round(Cena * Vaha, 2);




    }
}
