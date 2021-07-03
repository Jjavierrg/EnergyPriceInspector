using EnergyPriceInspector.Models;
using EnergyPriceInspector.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public ShellViewModel()
        {
            Title = Langs.Langs.AppTitle;
            StorageService = DependencyService.Get<IStorageService>() ?? throw new ArgumentNullException(nameof(IStorageService));

            _ = LoadUserConfigurationAsync().ConfigureAwait(false);
        }
        private IStorageService StorageService { get; }

        private async Task LoadUserConfigurationAsync()
        {
            IsBusy = true;
            var userConfig = await StorageService.LoadDataAsync<UserConfiguration>(Constants.Constants.CONFIGURATION_SAVE_KEY);
            if (userConfig == null)
            {
                userConfig = new UserConfiguration
                {
                    GeoId = 1
                };
            }

            DependencyService.RegisterSingleton(userConfig);
            IsBusy = false;
        }
    }
}
