namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ShellViewModel : BaseViewModel
    {
        public ShellViewModel()
        {
            Title = Langs.Langs.AppTitle;
            StorageService = DependencyService.Get<IStorageService>() ?? throw new ArgumentNullException("IStorageService");

            _ = LoadUserConfigurationAsync().ConfigureAwait(false);
        }

        private IStorageService StorageService { get; }

        private Task LoadUserConfigurationAsync() => ExecuteWithBusyIndicatorControl(async () =>
        {
            var userConfig = await StorageService.LoadDataAsync<UserConfiguration>(Constants.Constants.CONFIGURATION_SAVE_KEY);
            userConfig ??= new UserConfiguration();

            DependencyService.RegisterSingleton(userConfig);
        });
    }
}
