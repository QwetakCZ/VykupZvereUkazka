using LuRa_Vykup.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuRa_Vykup.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

        public virtual DbSet<Dodavatele> Dodavatele { get; set; } //Tabulka v db - dodavatele 

        public virtual DbSet<Vykup> Vykup { get; set; } //Tabulka v db - vykup

        public virtual DbSet<DodaciListy> DodaciListy { get; set; } //Tabulka v db - dodaci listy

        public virtual DbSet<Vykupna> Vykupny { get; set; } //Tabulka v db - Vykupna
        public virtual DbSet<Ceniky> Ceniky { get; set; } 
        public virtual DbSet<PokladniDoklady> PokladniDoklady { get; set; }
        public virtual DbSet<Traces> Traces { get; set; }


    }
}
