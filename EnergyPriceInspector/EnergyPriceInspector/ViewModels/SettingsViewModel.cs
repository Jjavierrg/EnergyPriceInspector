namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            Title = Langs.Langs.SettingsTitle;

            StorageService = DependencyService.Get<IStorageService>() ?? throw new ArgumentNullException("IStorageService");
            UserConfiguration = DependencyService.Get<UserConfiguration>() ?? throw new ArgumentNullException("UserConfiguration");
            EnergyService = DependencyService.Get<IEnergyService>() ?? throw new ArgumentNullException("IEnergyService");

            UserConfiguration.PropertyChanged += SaveDataAsync;

            _ = LoadDataAsync().ConfigureAwait(false);
        }

        public UserConfiguration UserConfiguration { get; }
        public IEnumerable<GeoLocation> GeoLocations { get; private set; }
        public GeoLocation SelectedGeolocation
        {
            get => UserConfiguration.GeoLocation;
            set { UserConfiguration.GeoLocation = value; OnPropertyChanged(); }
        }


        private IStorageService StorageService { get; }
        private IEnergyService EnergyService { get; }

        private Task LoadDataAsync() => ExecuteWithBusyIndicatorControl(async () => GeoLocations = await EnergyService.GetGeolocationsAsync());
        private async void SaveDataAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e) => await StorageService.SaveDataAsync(Constants.Constants.CONFIGURATION_SAVE_KEY, UserConfiguration);
    }
}
