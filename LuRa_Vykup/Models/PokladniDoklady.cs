using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuRa_Vykup.Models
{
    public class PokladniDoklady
    {
        [Key]
        public int Id { get; set; }

        public string CisloDokladu { get; set; }

        public DateTime Datum { get; set; }

        public string IdUzivatele { get; set; }
        
        public int IdDodavatel { get; set; }

        [ForeignKey("IdDodavatel")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Dodavatele Dodavatele { get; set; }

        public int IdCenik { get; set; }

        [ForeignKey("IdCenik")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Ceniky Ceniky { get; set; }

    }
}
