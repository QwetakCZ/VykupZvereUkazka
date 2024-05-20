using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LuRa_Vykup.Models
{
    public enum Mena : int
    {
        CZK = 1,
        EUR = 2,
    }


    [Table("Ceniky")]
    public class Ceniky
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Název nesmí být prázdný")]
        public string Nazev { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SrnciI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SrnciII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SrnciIII { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal DanekI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DanekII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DanekIII { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal JelenI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal JelenII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal JelenIII { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DivocakI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DivocakII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DivocakIII { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SikaI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SikaII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SikaIII { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MuflonI { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MuflonII { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MuflonIII { get; set; }

        public Mena Mena { get; set; } = Mena.CZK;


    }
}
