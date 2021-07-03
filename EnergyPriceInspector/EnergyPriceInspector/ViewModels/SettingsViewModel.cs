namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            Title = Langs.Langs.SettingsTitle;

            StorageService = DependencyService.Get<IStorageService>() ?? throw new ArgumentNullException("IStorageService");
            UserConfiguration = DependencyService.Get<UserConfiguration>() ?? throw new ArgumentNullException("UserConfiguration");
            UserConfiguration.PropertyChanged += SaveDataAsync;

            LoadData();
        }

        public UserConfiguration UserConfiguration { get; }
        public Geo[] GeoLocations { get; private set; }
        public Geo SelectedGeolocation
        {
            get => GeoLocations.FirstOrDefault(x => x.geo_id == UserConfiguration?.GeoId);
            set { UserConfiguration.GeoId = value?.geo_id ?? 0; OnPropertyChanged(); }
        }


        private IStorageService StorageService { get; }


        private void LoadData()
        {
            GeoLocations = new[] {
                new Geo { geo_id = 8741, geo_name = Langs.Langs.Peninsula },
                new Geo { geo_id = 8742, geo_name = Langs.Langs.Canarias },
                new Geo { geo_id = 8743, geo_name = Langs.Langs.Baleares },
                new Geo { geo_id = 8744, geo_name = Langs.Langs.Ceuta },
                new Geo { geo_id = 8745, geo_name = Langs.Langs.Melilla }
            };
        }
        private async void SaveDataAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e) => await StorageService.SaveDataAsync(Constants.Constants.CONFIGURATION_SAVE_KEY, UserConfiguration);
    }
}
