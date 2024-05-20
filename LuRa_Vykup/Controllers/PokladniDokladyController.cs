using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuRa_Vykup.Data;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;
using LuRa_Vykup.Pages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuRa_Vykup.Controllers
{
    public class PokladniDokladyController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PokladniDokladyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <List<PokladniDoklady>> GetPokladnidokladySeznam(string userId)
        {
            var vykupPage = await _context.PokladniDoklady
                                           .Where(x => x.IdUzivatele == userId)
                                           .Include(x => x.Dodavatele)
                                           .ThenInclude(x => x.Vykupna)
                                           .Include(x => x.Ceniky)
                                           .OrderByDescending(d => d.CisloDokladu)
                                           .ToListAsync();
            return vykupPage;
        }

        public async Task<List<Vykup>> GetPokladnidokladySeznamFiltr(string userId, int IdDodavatel)
        {
            var vykupPage = await _context.Vykup
                                           .Where(x => x.IdUzivatele == userId && x.IdDodavatel == IdDodavatel && x.IdPokladniDoklad == null && x.DodaciListId == null)
                                           .Include(x => x.Dodavatele)
                                           .ThenInclude(x => x.Ceniky)
                                           .Include(x => x.DodaciListy)
                                           .Include(x => x.Vykupna)
                                           .Include(x => x.PokladniDoklady)
                                           .Include(x => x.Traces)
                                           .OrderByDescending(d => d.Id)
                                           .ToListAsync();
            return vykupPage;
        }

        public async Task<List<Vykup>> GetPokladnidokladySeznamDoklad(string userId, int idDoklad)
        {
            var vykupPage = await _context.Vykup
                                           .Where(x => x.IdUzivatele == userId && x.IdPokladniDoklad == idDoklad)
                                           .Include(x => x.Dodavatele)
                                           .ThenInclude(x => x.Ceniky)
                                           .Include(x => x.DodaciListy)
                                           .Include(x => x.Vykupna)
                                           .Include(x => x.PokladniDoklady)
                                           .Include(x => x.Traces)
                                           .OrderByDescending(d => d.Id)
                                           .ToListAsync();
            return vykupPage;
        }

        public async Task <List<(string Nazev, int Id, int CenikId)>> GetNazvyDodavatele(string userId)
        {
            var seznam = await _context.Vykup
                .Where(x => x.IdUzivatele == userId && x.DodaciListId == null && x.IdPokladniDoklad == null)
                .Include(x => x.Dodavatele)
                .ThenInclude(x => x.Ceniky)
                .Select(x => new { x.Dodavatele.Dodavatel, x.Dodavatele.Id, x.Dodavatele.CenikId })
                .Distinct()
                .ToListAsync();
                

            return seznam.Select(d => (d.Dodavatel, d.Id, d.CenikId)).ToList();
        }

        public async Task<bool> SavePokladniDoklad(List<Vykup> vykup, PokladniDoklady pokladniDoklady, string UserId)
        {
            try
            {
                await _context.PokladniDoklady.AddAsync(pokladniDoklady);
                await _context.SaveChangesAsync();
                int newId = pokladniDoklady.Id;

                vykup.ForEach(x => x.IdPokladniDoklad = newId);
                vykup.ForEach(x => x.CenaKgPokladniDoklad = HelperServices.VratCenu(x.Dodavatele.Ceniky, (int)x.Kategorie, (int)x.Druh));
                _context.Vykup.UpdateRange(vykup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveAndDeletePokladniDoklad(List<Vykup> vykup, string UserId)
        {
            try
            {
                vykup.ForEach(x => x.IdPokladniDoklad = null);
                vykup.ForEach(x => x.CenaKgPokladniDoklad = null);
                _context.Vykup.UpdateRange(vykup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SmazatDoklad(int id, List<Vykup> vykup)
        {
            try
            {
                vykup.ForEach(x => x.CenaKgPokladniDoklad = null);
                _context.Vykup.UpdateRange(vykup);
                await _context.SaveChangesAsync();
                //k uprave ve vykupu dojde automaticky - vazba nastavena na SetNull
                await _context.PokladniDoklady.Where(x => x.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
