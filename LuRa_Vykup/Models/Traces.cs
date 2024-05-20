using System.ComponentModel.DataAnnotations;

namespace LuRa_Vykup.Models
{
    public class Traces
    {
        [Key]
        public int Id { get; set; }

        public string CisloTraces { get; set; }

        public string IdUzivatele { get; set; }

        public DateTime Datum { get; set; }

    }
}
