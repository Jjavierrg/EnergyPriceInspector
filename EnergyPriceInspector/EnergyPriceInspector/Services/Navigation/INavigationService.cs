using System.Threading.Tasks;

namespace EnergyPriceInspector.Services
{
    public interface INavigationService
    {
        Task NavigateBackAsync<T>();
        Task NavigateToAsync<T>();
        void RegisterRouteComponent<T>();
    }
}