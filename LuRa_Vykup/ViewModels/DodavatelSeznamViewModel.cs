using LuRa_Vykup.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LuRa_Vykup.ViewModels
{
    public class DodavatelSeznamViewModel
    {
        private Dodavatele _dodavatel;

        public DodavatelSeznamViewModel(Dodavatele dodavatel)
        {
            _dodavatel = dodavatel;
        }

        public int Id => _dodavatel.Id;

        public string Jmeno => _dodavatel.Dodavatel;

        public string CisloM => _dodavatel.CisloM;

        public string Cenik => _dodavatel.Ceniky.Nazev;

        public string Mena => _dodavatel.Ceniky.Mena.ToString();

    }
}
