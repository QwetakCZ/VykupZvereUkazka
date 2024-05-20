namespace LuRa_Vykup.Services.Interface
{
    public interface ILocalStorage
    {
        Task Save<T>(string key, T data);
        Task<T> Load<T>(string key);
    }
}
