using Blazored.LocalStorage;
using LuRa_Vykup.Services.Interface;
using Microsoft.Extensions.Options;

namespace LuRa_Vykup.Services
{
    public class LocalStorage : ILocalStorage
    {
        private ILocalStorageService _ls;

        public LocalStorage(ILocalStorageService ls)
        {
            _ls = ls;
        }

        public async Task<T> Load<T>(string key)
        {
            try
            {
                if (!(await _ls.ContainKeyAsync(key)))
                    return default;

                return await _ls.GetItemAsync<T>(key);
            }
            catch (Exception exc)
            {
                return default;
            }
        }

        public async Task Save<T>(string key, T data)
        {
            await _ls.SetItemAsync<T>(key, data);
        }



    }
        

}
