using EnergyPriceInspector.Services;
using EnergyPriceInspector.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private Command navigateCommand;
        private readonly INavigationService navigationService;

        public AboutViewModel()
        {
            Title = Langs.Langs.AboutTitle;
            navigationService = DependencyService.Get<INavigationService>();
        }

        public ICommand NavigateCommand => navigateCommand ??= new Command(async () => await NavigateCommandExecute());

        private Task NavigateCommandExecute() => navigationService.NavigateToAsync<AboutView>();
    }
}
