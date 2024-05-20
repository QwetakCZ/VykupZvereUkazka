using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuRa_Vykup.Pages;
using LuRa_Vykup.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Humanizer.Localisation.DateToOrdinalWords;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using NuGet.Versioning;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;

namespace LuRa_Vykup.Controllers
{
    
    public class VykupController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public VykupController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> UpdateVykupAsync(Models.Vykup newVykup)
        {
            try
            {
                _context.Vykup.Update(newVykup);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Vykup> GetVykup(string userId, int id)
        {
            return await _context.Vykup
                .Where(x => x.Id == id && x.IdUzivatele == userId)
                .Include(x => x.DodaciListy)
                .Include(x => x.PokladniDoklady)
                .FirstAsync();
        }


        public async Task<bool> DeleteVykupAsync(int id)
        {
            try
            {
                await _context.Vykup.Where(x => x.Id == id && x.DodaciListId == null).ExecuteDeleteAsync();
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Metoda na vraceni CisloM na zaklade Id z tabulky Dodavatele
        public async Task<string> GetCisloM(int id)
        {
            return await _context.Dodavatele.Where(x => x.Id == id).Select(x => x.CisloM).FirstOrDefaultAsync() ?? "0";
        }

        public int GetIdVykupna(string userId)
        {
            if (HelperServices.IdVykupna == 0)
            {
                HelperServices.IdVykupna = _context.Vykupny.Where(x => x.IdUzivatel == userId).Select(x => x.Id).FirstOrDefault();
            }
            return HelperServices.IdVykupna;
        }



        public async Task<bool> CreateVykupAsync(string userId, Models.Vykup newVykup)
        {
            try
            {
                newVykup.IdUzivatele = userId;
                newVykup.VykupnaId = GetIdVykupna(userId);
                await _context.Vykup.AddAsync(newVykup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Vykup>> GetVykupPage(string userId)
        {
            var vykupPage =await _context.Vykup
                                           .Where(x => x.IdUzivatele == userId)
                                           .OrderByDescending(d => d.DatumVykupu)
                                           .Include(x => x.Dodavatele)
                                           .Include(x => x.DodaciListy)
                                           .Include(x => x.PokladniDoklady)
                                           .ToListAsync();
            return vykupPage;
        }

        public async Task<List<Vykup>> GetVykupLast(string userId, int pocetPoslednich)
        {
            var vykupPage = await _context.Vykup

                                           .Where(x => x.IdUzivatele == userId && x.DatumVykupu.Date > DateTime.Now.Date.AddDays(-2))
                                           .OrderByDescending(d => d.Id)
                                           .Take(pocetPoslednich)
                                           .Include(x => x.Dodavatele)
                                           .ToListAsync();

            return vykupPage;
        }

        public async Task<List<(string Nazev, int Id)>> GetNazvyDodavatele(string userId)
        {
            var vykupPage = await _context.Dodavatele
                                           .Where(x => x.IdUzivatel == userId)
                                           .OrderBy(d => d.Dodavatel)
                                           .Select(x => new { x.Dodavatel, x.Id })
                                           .ToListAsync();

            return vykupPage.Select(d => (d.Dodavatel, d.Id)).ToList();
        }

        
    }

}
