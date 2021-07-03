namespace EnergyPriceInspector
{
    using EnergyPriceInspector.ApiClient;
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using EnergyPriceInspector.Views;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Reflection;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var configuration = LoadConfiguration();
            RegisterDependencies(configuration);
            RegisterRoutes();

            MainPage = new ShellView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private IConfiguration LoadConfiguration()
        {
            var appSettingsFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EnergyPriceInspector.appsettings.json");
            if (appSettingsFileStream == null)
                return new Configuration();

            using var reader = new StreamReader(appSettingsFileStream);
            var jsonContent = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Configuration>(jsonContent);
        }

        private void RegisterDependencies(IConfiguration configuration)
        {
            DependencyService.RegisterSingleton(configuration);
            DependencyService.Register<ApiRetryHandler>();
            DependencyService.Register<ApiTokenHandler>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IApiClientFactory, ApiClientFactory>();
            DependencyService.Register<IEnergyService, EnergyService>();
        }

        private void RegisterRoutes()
        {
            var navigationService = DependencyService.Get<INavigationService>();
            navigationService.RegisterRouteComponent<DashboardView>();
            navigationService.RegisterRouteComponent<AboutView>();
        }
    }
}
