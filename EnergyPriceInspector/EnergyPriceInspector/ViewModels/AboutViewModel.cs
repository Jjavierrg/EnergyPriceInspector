using EnergyPriceInspector.Services;
using EnergyPriceInspector.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private Command busyToogleCommand;

        public AboutViewModel()
        {
            Title = Langs.Langs.AboutTitle;
        }

        public ICommand BusyToogleCommand => busyToogleCommand ??= new Command(() => IsBusy = !IsBusy);
    }
}
