namespace EnergyPriceInspector
{
    using EnergyPriceInspector.Services;
    using EnergyPriceInspector.Views;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterDependencies();
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

        private void RegisterDependencies()
        {
            DependencyService.Register<INavigationService, NavigationService>();
        }

        private void RegisterRoutes()
        {
            var navigationService = DependencyService.Get<INavigationService>();
            navigationService.RegisterRouteComponent<DashboardView>();
            navigationService.RegisterRouteComponent<AboutView>();
        }
    }
}
