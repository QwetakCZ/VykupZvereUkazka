using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuRa_Vykup.Models
{
    public enum DruhZvirete : int
    {
        Divočák = 1,
        Srnčí = 2,
        Jelen = 3,
        Daněk = 4,
        Sika = 5,
        Muflon = 6
    }

    public enum Kategorie:int
    {
        [Description("Kat I")]
        I = 1,
        [Description("Kat II")]
        II = 2,
        [Description("Kat III")]
        III = 3,
    }

    [Table("Vykup")] //nazev tabulky v databazi
    public class Vykup
    {

        [Key]
        public int Id { get; set; }
        
        [Required, Display(Name = "ID Dodavatele")]
        public int IdDodavatel { get; set; }

        [MaxLength(50), Required, Display(Name = "Plomba")]
        public string Plomba { get; set; }

        [Required, Display(Name = "Druh")]
        [EnumDataType(typeof(DruhZvirete))]
        public DruhZvirete Druh { get; set; }

        [Required, Display(Name = "Vaha"), Column(TypeName = "decimal(18,2)")]
        public decimal Vaha { get; set; }

        [Required, Display(Name = "Datum Výkupu")]
        public DateTime DatumVykupu { get; set; }

        [MaxLength(50), Required, Display(Name = "Číslo M")]
        public string CisloM { get; set; }

        [Required, Display(Name = "Kategorie")]
        [EnumDataType(typeof(Kategorie))]
        public Kategorie Kategorie { get; set; }

        public int VykupnaId { get; set; }

        [Display(Name = "ID Dodacího listu")]
        public int? DodaciListId { get; set; }

        [ForeignKey("IdDodavatel")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Dodavatele Dodavatele { get; set; } //deklarovani vztahu mezi tabulkami Dodatele a Vykup

        [ForeignKey("DodaciListId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public DodaciListy? DodaciListy { get; set; } //deklarovani vztahu mezi tabulkami DodaciListy a Vykup

        [ForeignKey("VykupnaId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Vykupna? Vykupna { get; set; } //deklarovani vztahu mezi tabulkami Vykupna a Vykup

        public string IdUzivatele { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CenaKg { get; set; }

        public int? IdPokladniDoklad { get; set; }

        [ForeignKey("IdPokladniDoklad")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public PokladniDoklady? PokladniDoklady { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CenaKgPokladniDoklad { get; set; }

        public int? IdTraces { get; set; }
        
        [ForeignKey("IdTraces")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public Traces? Traces { get; set; }



    }
}
