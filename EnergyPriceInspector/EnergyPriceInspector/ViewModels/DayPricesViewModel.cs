namespace EnergyPriceInspector.ViewModels
{
    using EnergyPriceInspector.Extensions;
    using EnergyPriceInspector.Models;
    using EnergyPriceInspector.Services;
    using Microcharts;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class DayPricesViewModel : BaseViewModel
    {
        private ObservableCollection<PriceInformation> _dayPrices;
        private Chart _chartData;
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

        public Chart ChartData
        {
            get => _chartData;
            set => SetProperty(ref _chartData, value);
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
            ChartData = GenerateChartFromApiData(DayPrices);
            UpdatedDate = DateTime.Now;
        });

        private static Chart GenerateChartFromApiData(IEnumerable<PriceInformation> data)
        {
            if (!(data?.Any() ?? false))
                return null;

            return new LineChart
            {
                LineMode = LineMode.Straight,
                LabelTextSize = 20,
                LabelOrientation = Orientation.Vertical,
                Entries = data.Select((x, i) => new ChartEntry(x.Price)
                {
                    Color = SKColor.Parse("#1ab2ff"),
                    TextColor = SKColor.Parse("#1ab2ff"),
                    Label = i % 3 == 0 ? x.Date.ToString("t") : string.Empty,
                    ValueLabel = x.Price.ToString()
                })
            };
        }
    }
}
