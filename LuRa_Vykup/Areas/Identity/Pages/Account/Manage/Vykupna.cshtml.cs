// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuRa_Vykup.Data;
using LuRa_Vykup.Controllers;


namespace LuRa_Vykup.Areas.Identity.Pages.Account.Manage
{
    public class VykupModel : PageModel
    {


        private string? UserId { get; set; }
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }

        public VykupModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

        }





        [BindProperty]
        [Required]
        [Display(Name = "Název výkupny")]
        public string Nazev { get; set; }

        [BindProperty]
        [Display(Name = "Ulice")]
        public string Ulice { get; set; }

        [BindProperty]
        [Display(Name = "Číslo popisné")]
        public string CisloPopisne { get; set; }

        [BindProperty]
        [Display(Name = "Město")]
        public string Mesto { get; set; }

        [BindProperty]
        [Display(Name = "PSČ")]
        public string Psc { get; set; }

        [BindProperty]
        [Display(Name = "IČO")]
        public string Ico { get; set; }

        [BindProperty]
        [Display(Name = "Firma")]
        public string Firma { get; set; }





        private async Task LoadAsync(IdentityUser user)
        {
            Debug.WriteLine("Nacitam data");
            string userId = user.Id;
            ZalozitVykupnuController zalozitVykupnu = new ZalozitVykupnuController(_context);
            Models.Vykupna vykupna = zalozitVykupnu.VratVykupnu(user.Id);
            //Models.Vykupna vykupna = _context.Vykupny.FirstOrDefault(x => x.IdUzivatel == user.Id);
            if (vykupna != null)
            {
                Nazev = vykupna.Nazev;
                Ulice = vykupna.Ulice;
                CisloPopisne = vykupna.CisloPopisne;
                Mesto = vykupna.Mesto;
                Psc = vykupna.Psc;
                Ico = vykupna.Ico;
                Firma = vykupna.Firma;
            }

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeVykupAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var dostanVykupnu = new ZalozitVykupnuController(_context);
            var vykupnaUpdate = dostanVykupnu.VratVykupnu(user.Id);
            if (vykupnaUpdate != null)
            {
                vykupnaUpdate.Nazev = Nazev;
                vykupnaUpdate.Ulice = Ulice;
                vykupnaUpdate.CisloPopisne = CisloPopisne;
                vykupnaUpdate.Mesto = Mesto;
                vykupnaUpdate.Psc = Psc;
                vykupnaUpdate.Ico = Ico;
                vykupnaUpdate.Firma = Firma;
            }
            dostanVykupnu.UlozVykupnu(vykupnaUpdate);

            StatusMessage = "Udaje byly zmeneny.";
            return RedirectToPage();

        }

        public async Task<IActionResult> VytvorDefaultVykupnu(string userID)
        {
            var vytvorDefault = new ZalozitVykupnuController(_context);
            var vykupna = new Models.Vykupna
            {
                Nazev = "Defaultni",
                Ulice = string.Empty,
                CisloPopisne = string.Empty,
                Mesto = string.Empty,
                Psc = string.Empty,
                IdUzivatel = userID,
                Firma = string.Empty,
                Ico = string.Empty

            };
            vytvorDefault.VytvorVykupnu(vykupna);

            return RedirectToPage();
        }

        public string GetNazevVykupny(string userID)
        {
            var vratVykupy = new ZalozitVykupnuController(_context);

            var vykupna = vratVykupy.VratVykupnu(userID);
            if (vykupna != null)
            {
                return vykupna.Nazev;
            }
            else
            {
                return null;
            }
        }


    }
}
