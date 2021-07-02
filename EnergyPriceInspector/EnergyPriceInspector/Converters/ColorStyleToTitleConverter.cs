namespace EnergyPriceInspector.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Xamarin.Forms;
    using static EnergyPriceInspector.Controls.DashboardWidget;

    public class ColorStyleToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ColorStyles)value switch
            {
                ColorStyles.HighValue => Langs.Langs.Max,
                ColorStyles.LowValue => Langs.Langs.Min,
                ColorStyles.Info => Langs.Langs.Now,
                _ => string.Empty,
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
