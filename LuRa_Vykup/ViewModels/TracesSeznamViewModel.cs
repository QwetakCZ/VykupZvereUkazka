using LuRa_Vykup.Models;

namespace LuRa_Vykup.ViewModels
    
{
    public class TracesSeznamViewModel
    {
        private Traces _tr;

        public TracesSeznamViewModel(Traces tr)
        {
            _tr = tr;
        }

        public int Id => _tr.Id;
        public string CisloTraces => _tr.CisloTraces;
        public DateOnly Datum => DateOnly.FromDateTime(_tr.Datum);

        }

}
