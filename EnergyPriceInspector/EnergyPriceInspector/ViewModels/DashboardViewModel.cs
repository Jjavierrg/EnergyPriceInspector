namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using EnergyPriceInspector.Views;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class DashboardViewModel : BaseViewModel
    {
        private DashboardData _dashboardData;
        private Command _refreshCommand;
        private Command _navigateToSettingsCommand;

        public DashboardViewModel()
        {
            Title = Langs.Langs.DashboardTitle;
            EnergyService = DependencyService.Get<IEnergyService>() ?? throw new ArgumentNullException("IEnergyService ");
            StorageService = DependencyService.Get<IStorageService>() ?? throw new ArgumentNullException("IStorageService ");
            UserConfiguration = DependencyService.Get<UserConfiguration>() ?? throw new ArgumentNullException("UserConfiguration ");
            NavigationService = DependencyService.Get<INavigationService>() ?? throw new ArgumentNullException("INavigationService ");

            _ = LoadDataAsync().ConfigureAwait(false);
        }


        public DashboardData DashboardData
        {
            get => _dashboardData;
            set => SetProperty(ref _dashboardData, value);
        }

        public ICommand RefreshCommand => _refreshCommand ??= new Command(async () => await RefreshDashboardData());
        public ICommand NavigateToSettingsCommand => _navigateToSettingsCommand ??= new Command(async () => await NavigationService.NavigateToAsync<SettingsView>());

        private IEnergyService EnergyService { get; }
        private IStorageService StorageService { get; }
        private UserConfiguration UserConfiguration { get; }
        private INavigationService NavigationService { get; }

        private Task LoadDataAsync() => ExecuteWithBusyIndicatorControl(async () =>
        {
            var previousData = await StorageService.LoadDataAsync<DashboardData>(Constants.Constants.LASTDATA_SAVE_KEY);
            previousData ??= await GetDashboardDataFromProvider();
            DashboardData = previousData;
        });

        private Task RefreshDashboardData() => ExecuteWithBusyIndicatorControl(async () => DashboardData = await GetDashboardDataFromProvider());

        private async Task<DashboardData> GetDashboardDataFromProvider()
        {
            var result = await EnergyService.GetDashboardAsync(UserConfiguration?.GeoLocation);
            await StorageService.SaveDataAsync(Constants.Constants.LASTDATA_SAVE_KEY, result);
            return result;
        }
    }
}
