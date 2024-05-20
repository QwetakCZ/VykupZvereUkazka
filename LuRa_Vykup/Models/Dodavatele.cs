using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuRa_Vykup.Models
{
    [Table("Dodavatele")] //nazev tabulky v databazi
    public class Dodavatele
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50),Required(ErrorMessage ="Zadejte prosím jméno dodavatele"), Display(Name = "Dodavatel"),]
        public string Dodavatel { get; set; }

        [MaxLength(50), Required(ErrorMessage ="Zadejte prosím číslo prohlížitele"), Display(Name = "CisloM")]
        public string CisloM { get; set; }

        [MaxLength(50), Display(Name = "Telefon")]
        public string? Telefon { get; set; } =  string.Empty;

        [MaxLength(250), Display(Name = "Email")]
        public string? Email { get; set; } = string.Empty;  

        public string? IdUzivatel { get; set; }
        public int? IdVykupna { get; set; }

        [Required(ErrorMessage = "Vyberte prosím ceník")]
        public int CenikId { get; set; }

        [ForeignKey("CenikId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Ceniky? Ceniky { get; set; }

        [ForeignKey("IdVykupna")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Vykupna? Vykupna { get; set; }

    }
}
