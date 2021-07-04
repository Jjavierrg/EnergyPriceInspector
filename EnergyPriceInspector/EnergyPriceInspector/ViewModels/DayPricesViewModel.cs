namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Extensions;
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using EnergyPriceInspector.Views;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class DayPricesViewModel : BaseViewModel
    {
        private ObservableCollection<PriceInformation> _dayPrices;
        private DateTime _updatedDate;
        private Command _refreshCommand;

        public DayPricesViewModel()
        {
            Title = Langs.Langs.DayPricesTitle;
            EnergyService = DependencyService.Get<IEnergyService>() ?? throw new ArgumentNullException("IEnergyService ");
            UserConfiguration = DependencyService.Get<UserConfiguration>() ?? throw new ArgumentNullException("UserConfiguration ");

            _ = LoadDataAsync().ConfigureAwait(false);
        }

        public ObservableCollection<PriceInformation> DayPrices
        {
            get => _dayPrices;
            set => SetProperty(ref _dayPrices, value);
        }

        public DateTime UpdatedDate
        {
            get => _updatedDate;
            set => SetProperty(ref _updatedDate, value);
        }

        public ICommand RefreshCommand => _refreshCommand ??= new Command(async () => await RefreshDashboardData());
        private IEnergyService EnergyService { get; }
        private UserConfiguration UserConfiguration { get; }

        private Task LoadDataAsync() => RefreshDashboardData();

        private Task RefreshDashboardData() => ExecuteWithBusyIndicatorControl(async () =>
        {
            var startOfToday = DateTime.Today;
            var endOfToday = DateTime.Today.AddDays(1).AddSeconds(-1);
            var prices = await EnergyService.GetPricesAsync(UserConfiguration?.GeoLocation?.Id, startOfToday, endOfToday);

            DayPrices = prices?.OrderBy(x => x.Date).ToObservableCollection();
            UpdatedDate = DateTime.Now;
        });
    }
}
