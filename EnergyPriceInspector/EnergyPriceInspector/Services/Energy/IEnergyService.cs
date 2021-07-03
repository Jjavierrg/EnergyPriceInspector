using System.Threading.Tasks;

namespace EnergyPriceInspector.Services
{
    public interface IEnergyService
    {
        Task GetPrices();
    }
}