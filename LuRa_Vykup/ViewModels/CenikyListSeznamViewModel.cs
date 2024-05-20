using LuRa_Vykup.Models;

namespace LuRa_Vykup.ViewModels
{
    public class CenikyListSeznamViewModel
    {
        private Ceniky _ceniky;

        public CenikyListSeznamViewModel(Ceniky ceniky)
        {
            _ceniky = ceniky;
        }

        public int Id => _ceniky.Id;

        public string Nazev => _ceniky.Nazev;

        public string MenaString => _ceniky.Mena.ToString();

    }
}
