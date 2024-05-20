using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuRa_Vykup.Data;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;
using LuRa_Vykup.Pages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuRa_Vykup.Controllers
{
    public class DLController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DLController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Seznam
        public async Task<List<(string Nazev, int Id)>> GetNazvyDodavatele(string userId)
        {
            var seznam = await _context.Vykup
                .Where(x => x.IdUzivatele == userId && x.DodaciListId == null && x.IdPokladniDoklad == null)
                .Select(x => new { x.Dodavatele.Dodavatel, x.Dodavatele.Id })
                .Distinct()
                .ToListAsync();

            return seznam.Select(d => (d.Dodavatel, d.Id)).ToList();
        }

        public async Task<List<DodaciListy>> GetDLList(string userId)
        {
            var vykupPage = await _context.DodaciListy
                                           .Where(x => x.IdUzivatele == userId)
                                           .Include(x => x.Dodavatele)
                                           .Include(x => x.Dodavatele)
                                           .ThenInclude(x => x.Ceniky)
                                           .OrderByDescending(d => d.CisloDL)
                                           .ToListAsync();
            return vykupPage;
        }

        #endregion

        #region NovyDodaciList
        public async Task <List<Vykup>> GetVykupList(string userId, int idDodavatel)
        {
            var vykupList = await _context.Vykup
                                       .Where(x => x.IdUzivatele == userId && x.DodaciListId == null && x.IdDodavatel == idDodavatel && x.IdPokladniDoklad == null)
                                       .Include(x => x.Dodavatele)
                                       .ThenInclude(x => x.Ceniky)
                                       .ToListAsync();
            return vykupList;
        }

        public async Task<bool> NewDLSave(string userId, DodaciListy dodaciListy, List<Vykup> vykupList)
        {
            try
            {
                await _context.DodaciListy.AddAsync(dodaciListy);
                await _context.SaveChangesAsync();
                var newId = dodaciListy.Id;

                vykupList.ForEach(x => x.DodaciListId = newId);
                vykupList.ForEach(x => x.CenaKg = HelperServices.VratCenu(x.Dodavatele.Ceniky, (int)x.Kategorie, (int)x.Druh));
                _context.Vykup.UpdateRange(vykupList);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        #endregion

        #region Uprava DL
        public async Task <List<Vykup>> GetPolozkyDL(string userId, int id)
        {
            var vykupList = await _context.Vykup
                                       .Where(x => x.IdUzivatele == userId && x.DodaciListId == id)
                                       .Include(x => x.Dodavatele)
                                       .ThenInclude(x => x.Ceniky)
                                       .Include(x => x.DodaciListy)
                                       .ToListAsync();
            return vykupList;
        }

        public async Task<bool> SaveAndDeleteDodaciList(List<Vykup> vykup, string UserId)
        {
            try
            {
                vykup.ForEach(x => x.DodaciListId = null);
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

        public async Task<bool> SmazatDoklad(int id, List<Vykup> list)
        {
            try
            {
                list.ForEach(x => x.CenaKg = null);
                _context.Vykup.UpdateRange(list);
                await _context.SaveChangesAsync();
                //k uprave ve vykupu dojde automaticky - vazba nastavena na SetNull
                await _context.DodaciListy.Where(x => x.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion



        public List<Vykup> GetVykupForDL(int id)
        {

            return _context.Vykup
                .Where(x => x.DodaciListId == id)
                .Include(x => x.Dodavatele)
                .Include(x => x.DodaciListy)
                .Include(x => x.Vykupna)
                .ToList();
        }

       
    }
}
