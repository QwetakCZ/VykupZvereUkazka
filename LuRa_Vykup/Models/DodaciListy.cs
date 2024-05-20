using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuRa_Vykup.Models
{
    [Table("DodaciListy")] //nazev tabulky v databazi
    public class DodaciListy
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Datum vystavení DL")]
        public DateTime? DatumVystaveni { get; set; }

        [MaxLength(50), Required, Display(Name = "Číslo dokladu")]
        public string CisloDL { get; set; }

        public string IdUzivatele { get; set; }

        public int IdDodavatel { get; set; }

        [ForeignKey("IdDodavatel")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Dodavatele Dodavatele { get; set; }


    }
}
