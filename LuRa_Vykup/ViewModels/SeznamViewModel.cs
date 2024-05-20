using System.Drawing;

namespace LuRa_Vykup.ViewModels
{
    public class SeznamViewModel
    {
        public int Id { get; set; }
        public string Plomba { get; set; }
        public string Druh { get; set; }
        public string Vaha { get; set; }
        public string Kategorie { get; set; }
        public DateOnly DatumVykupu { get; set; }
        public string Dodavatel { get; set; }
        public string? DodaciListCislo { get; set; }
        public int? DodaciListId { get; set; }
        public string? Doklad { get; set; }
    }
}
