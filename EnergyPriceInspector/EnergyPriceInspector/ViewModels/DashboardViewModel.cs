using EnergyPriceInspector.Services;
using EnergyPriceInspector.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private Command _navigateCommand;
        private Command _refreshCommand;

        public DashboardViewModel()
        {
            Title = Langs.Langs.DashboardTitle;
            NavigationService = DependencyService.Get<INavigationService>();
            EnergyService = DependencyService.Get<IEnergyService>();
            StorageService = DependencyService.Get<IStorageService>();

            _ = LoadData().ConfigureAwait(false);
        }

        public ICommand NavigateCommand => _navigateCommand ??= new Command(async () => await NavigateCommandExecute(), () => !IsBusy);
        public ICommand RefreshCommand => _refreshCommand ??= new Command(async () => await LoadData());

        private INavigationService NavigationService { get; }
        private IEnergyService EnergyService { get; }
        private IStorageService StorageService { get; }

        protected override void OnBusyChanged()
        {
            _navigateCommand?.ChangeCanExecute();
            _refreshCommand?.ChangeCanExecute();
        }

        private async Task LoadData()
        {
            IsBusy = true;
            try
            {
                var result = await EnergyService.GetPricesAsync();
                await StorageService.SaveDataAsync("LAST_DATA", result);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task NavigateCommandExecute() => NavigationService.NavigateToAsync<AboutView>();
    }
}
