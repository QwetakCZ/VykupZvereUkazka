using LuRa_Vykup.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuRa_Vykup.Services;
using LuRa_Vykup.Models;
using LuRa_Vykup.Pages;
using static Azure.Core.HttpHeader;
using System.ComponentModel;

namespace LuRa_Vykup.Controllers
{
    public class ZalozitVykupnuController : Controller
    {
       private readonly ApplicationDbContext _context;

        public ZalozitVykupnuController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public Models.Vykupna VratVykupnu(string user)
        {
            Models.Vykupna vykupna = _context.Vykupny.FirstOrDefault(x => x.IdUzivatel == user);
            return vykupna;
        }

        public async Task<Models.Vykupna> UlozVykupnu(Models.Vykupna vykupna)
        {
            _context.Update(vykupna);
            await _context.SaveChangesAsync();
            return vykupna;
        }

        public async Task<Models.Vykupna> VytvorVykupnu(Models.Vykupna vykupna)
        {
            _context.Add(vykupna);
            await _context.SaveChangesAsync();
            return vykupna;
        }

       
    }
}
