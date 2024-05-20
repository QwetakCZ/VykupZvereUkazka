using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuRa_Vykup.Data;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;
using LuRa_Vykup.Pages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuRa_Vykup.Controllers
{
    public class TracesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public TracesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Traces>> GetTracesSeznam(string userId)
        {
            var tracesPage = await _context.Traces
                                           .Where(x => x.IdUzivatele == userId)
                                           .OrderByDescending(d => d.CisloTraces)
                                           .ToListAsync();
            return tracesPage;
        }

        public async Task<List<Vykup>> GetTracesSeznamFiltr(string userId)
        {
            var vykupPage = await _context.Vykup
                                            .Where(x => x.IdUzivatele == userId && x.IdTraces == null)
                                            .Include(x => x.Dodavatele)
                                                .ThenInclude(x => x.Ceniky)
                                            .Include(x => x.Traces)
                                            .Where(x => x.Dodavatele.Ceniky.Mena == Mena.EUR) // Filtrování podle hodnoty Mena v Ceniky
                                            .OrderByDescending(d => d.Id)
                                            .ToListAsync();
            return vykupPage;
        }

        public async Task<List<Vykup>> GetPolozkyTracers(string userId, int id)
        {
            var vykupList = await _context.Vykup
                                       .Where(x => x.IdUzivatele == userId && x.IdTraces == id)
                                       .Include(x => x.Dodavatele)
                                       .ThenInclude(x => x.Ceniky)
                                       .Include(x => x.Traces)
                                       .ToListAsync();
            return vykupList;
        }

        public async Task<bool> SaveAndDeleteTracesList(List<Vykup> vykup, string UserId)
        {
            try
            {
                vykup.ForEach(x => x.IdTraces = null);
                _context.Vykup.UpdateRange(vykup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SmazatDoklad(int id)
        {
            try
            {
                await _context.Traces.Where(x => x.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> SaveTraces(List<Vykup> vykup, Traces traces, string UserId)
        {
            try
            {
                await _context.Traces.AddAsync(traces);
                await _context.SaveChangesAsync();
                int newId = traces.Id;

                vykup.ForEach(x => x.IdTraces = newId);
                _context.Vykup.UpdateRange(vykup);
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
