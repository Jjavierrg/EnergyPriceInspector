using System.Threading.Tasks;

namespace EnergyPriceInspector.Services
{
    public interface IStorageService
    {
        Task<T> LoadDataAsync<T>(string key);
        Task SaveDataAsync<T>(string key, T data);
    }
}