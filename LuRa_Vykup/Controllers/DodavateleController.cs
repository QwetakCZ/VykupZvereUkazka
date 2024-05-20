using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuRa_Vykup.Pages;
using LuRa_Vykup.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Humanizer.Localisation.DateToOrdinalWords;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;

namespace LuRa_Vykup.Controllers
{
    
    public class DodavateleController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public DodavateleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Dodavatele> GetDodavatele(string userId, int id)
        {
            return await _context.Dodavatele.Where(x => x.Id == id && x.IdUzivatel == userId).FirstOrDefaultAsync();
        }


        public async Task<bool> UpdateDodavateleAsync(Models.Dodavatele newDodavatel)
        {
            try
            {
                _context.Dodavatele.Update(newDodavatel);
                await _context.SaveChangesAsync();
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteDodavateleAsync(int id)
        {
            try
            {
                var vykup = await _context.Vykup.Where(x => x.IdDodavatel == id).CountAsync();
                if (vykup > 0)
                {
                    return false;
                }
                await _context.Dodavatele.Where(x => x.Id == id).ExecuteDeleteAsync();
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetIdVykupna(string userId)
        {
            if (HelperServices.IdVykupna == 0)
            {
                HelperServices.IdVykupna = _context.Vykupny.Where(x => x.IdUzivatel == userId).Select(x => x.Id).FirstOrDefault();
            }
            return HelperServices.IdVykupna;
        }

        public async Task<bool> CreateDodavateleAsync(string userId, Models.Dodavatele newDodavatel)
        {
            try
            {
                newDodavatel.IdVykupna = GetIdVykupna(userId);
                newDodavatel.IdUzivatel = userId;
                _context.Dodavatele.Add(newDodavatel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task <List<Models.Dodavatele>> GetDodavateleList(string userId)
        {
            var dodavatele = await _context.Dodavatele
                                           .Where(x => x.IdUzivatel == userId)
                                           .OrderBy(d => d.Dodavatel)
                                           .Include(x => x.Ceniky)
                                           .ToListAsync();

            return dodavatele;
        }

        public async Task VytvorDefaultVykupnu(string userID)
        {

            var vykupna = new Models.Vykupna
            {
                Nazev = "Defaultni",
                Ulice = string.Empty,
                CisloPopisne = string.Empty,
                Mesto = string.Empty,
                Psc = string.Empty,
                IdUzivatel = userID

            };

            _context.Vykupny.Add(vykupna);
            await _context.SaveChangesAsync();
        }
    }

}
