using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;



namespace LuRa_Vykup.Services
{
    public class LoginServices
    {
        private AuthenticationState AuthenticationStateTask { get; set; }

        public LoginServices(Task<AuthenticationState> auth)
        {
            this.AuthenticationStateTask = auth.Result;
        }

        /// <summary>
        /// Vraci ID uzivatele a kontroluje zda je uzivatel prihlasen
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public string GetUserId(Action<bool> fc)
        {
            string id = AuthenticationStateTask.User.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            fc.Invoke(id == null);
            return id;
        }

        /// <summary>
        /// Vraci jmeno prihlaseneho uzivatele
        /// </summary>
        /// <returns></returns>
        public string? GetUserName()
        {
            return AuthenticationStateTask.User.Identity.Name;
        }
    }
}
