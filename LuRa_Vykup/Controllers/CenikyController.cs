using LuRa_Vykup.Data;
using LuRa_Vykup.Models;
using LuRa_Vykup.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace LuRa_Vykup.Controllers
{
    public class CenikyController
    {

        private readonly ApplicationDbContext _context;
        public CenikyController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Vraci nazvy ceniku pro vyber v selectu
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<(string Nazev, int Id)>> GetCenikyNazvy(string userId)
        {
            var ceniky = await _context.Ceniky
                                           .Where(x => x.UserId == userId)
                                           .OrderBy(d => d.Nazev)
                                           .Select(x => new { x.Nazev, x.Id })
                                           .ToListAsync();

            return ceniky.Select(d => (d.Nazev, d.Id)).ToList();
        }

        public async Task CreateDefaultCeniky(string userId)
        {
           await _context.Ceniky.AddAsync(new Models.Ceniky
           {
               Nazev = "Základní ceník",
               UserId = userId,
               JelenI = 0,
               JelenII = 0,
               JelenIII = 0,
               DanekI = 0,
               DanekII = 0,
               DanekIII = 0,
               SikaI = 0,
               SikaII = 0,
               SikaIII = 0,
               SrnciI = 0,
               SrnciII = 0,
               SrnciIII = 0,
               DivocakI = 0,
               DivocakII = 0,
               DivocakIII = 0,
               MuflonI = 0,
               MuflonII = 0,
               MuflonIII = 0,
           });

            await _context.SaveChangesAsync();
        }

        public async Task<List<Ceniky>> GetCenikyList(string userId)
        {
            var cenikPage = await _context.Ceniky
                                           .Where(x => x.UserId == userId)
                                           .OrderBy(d => d.Nazev)
                                           .ToListAsync();
            return cenikPage;
        }

        public async Task<bool> CreateCenikAsync(string userId, Models.Ceniky newCenik)
        {
            try
            {
                newCenik.UserId = userId;
                _context.Ceniky.Add(newCenik);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Ceniky> GetCenik(int id, string userId)
        {
            var _cenik = await _context.Ceniky
                                           .Where(x => x.Id == id && x.UserId == userId)
                                            .Select(x => new Ceniky
                                            {
                                                Nazev = x.Nazev,
                                                SrnciI = x.SrnciI,
                                                SrnciII = x.SrnciII,
                                                SrnciIII = x.SrnciIII,
                                                DanekI = x.DanekI,
                                                DanekII = x.DanekII,
                                                DanekIII = x.DanekIII,
                                                JelenI = x.JelenI,
                                                JelenII = x.JelenII,
                                                JelenIII = x.JelenIII,
                                                DivocakI = x.DivocakI,
                                                DivocakII = x.DivocakII,
                                                DivocakIII = x.DivocakIII,
                                                SikaI = x.SikaI,
                                                SikaII = x.SikaII,
                                                SikaIII = x.SikaIII,
                                                MuflonI = x.MuflonI,
                                                MuflonII = x.MuflonII,
                                                MuflonIII = x.MuflonIII,
                                                Mena = x.Mena
                                            })
                                           .FirstAsync();
            return _cenik;
        }

        public async Task<bool> UpdateCenikAsync(int id, string userId, Models.Ceniky newCenik)
        {
            try
            {
                newCenik.UserId = userId;
                newCenik.Id = id;
                _context.Ceniky.Update(newCenik);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> IsCenikInUse (int id, string userId)
        {
            var dodavatele = _context.Dodavatele
                                           .Where(x => x.CenikId == id && x.IdUzivatel == userId)
                                           .FirstOrDefault();

            if (dodavatele != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCenikAsync(int id, string userId)
        {
            try
            {
                await _context.Ceniky.Where(x => x.Id == id && x.UserId == userId).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<List<(string Nazev, int Id)>> GetCenikyAsync(string userId)
        {
            //var ceniky = _context.Ceniky
            //                               .Where(x => x.UserId == userId)
            //                               .OrderBy(d => d.Nazev)
            //                               .ToList();
            //return ceniky;

            var ceniky = await _context.Ceniky
                                          .Where(x => x.UserId == userId)
                                          .OrderBy(d => d.Nazev)
                                          .Select(x => new { x.Nazev, x.Id })
                                          .ToListAsync();

            return ceniky.Select(d => (d.Nazev, d.Id)).ToList();
        }

    }
}
