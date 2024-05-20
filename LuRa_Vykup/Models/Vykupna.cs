using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuRa_Vykup.Models
{
    [Table("Vykupna")] //nazev tabulky v databazi
    public class Vykupna
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256), Required(ErrorMessage = "Zadejte prosím jméno výkupny"), Display(Name = "Výkupna"),]
        public string Nazev { get; set; }

        [MaxLength(256), Display(Name = "Firma")]
        public string? Firma { get; set; } = string.Empty;

        [MaxLength(256), Display(Name = "Ulice")]
        public string? Ulice { get; set; } = string.Empty;

        [MaxLength(50), Display(Name = "Číslo popisné")]
        public string? CisloPopisne { get; set; } = string.Empty;

        [MaxLength(50), Display(Name = "Město")]
        public string? Mesto { get; set; } = string.Empty;

        [MaxLength(50), Display(Name = "PSČ")]
        public string? Psc { get; set; } = string.Empty;

        [MaxLength(50), Display(Name = "IČO")]
        public string? Ico { get; set; } = string.Empty;

        public string? IdUzivatel { get; set; } = string.Empty;
    }
}
