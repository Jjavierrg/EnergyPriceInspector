using EnergyPriceInspector.Services;
using EnergyPriceInspector.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private Command navigateCommand;
        private Command refreshCommand;

        public DashboardViewModel()
        {
            Title = Langs.Langs.DashboardTitle;
            NavigationService = DependencyService.Get<INavigationService>();
            EnergyService = DependencyService.Get<IEnergyService>();

            _ = Task.Run(() => LoadData());
        }

        public ICommand NavigateCommand => navigateCommand ??= new Command(async () => await NavigateCommandExecute(), () => !IsBusy);
        public ICommand RefreshCommand => refreshCommand ??= new Command(async () => await LoadData());

        private INavigationService NavigationService { get; }
        private IEnergyService EnergyService { get; }

        protected override void OnBusyChanged()
        {
            navigateCommand?.ChangeCanExecute();
            refreshCommand?.ChangeCanExecute();
        }

        private async Task LoadData()
        {
            IsBusy = true;
            try
            {
                await EnergyService.GetPrices();
                await Task.Delay(5000);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task NavigateCommandExecute() => NavigationService.NavigateToAsync<AboutView>();
    }
}
