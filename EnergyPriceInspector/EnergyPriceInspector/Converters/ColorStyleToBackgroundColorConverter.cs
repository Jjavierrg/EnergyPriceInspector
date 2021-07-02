namespace EnergyPriceInspector.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Xamarin.Forms;
    using static EnergyPriceInspector.Controls.DashboardWidget;

    public class ColorStyleToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x=> x.Source?.OriginalString?.StartsWith("Styles/Colors.xaml", StringComparison.OrdinalIgnoreCase) ?? false);
            if (colorDictionary == null)
                return Color.Default;

            return (ColorStyles)value switch
            {
                ColorStyles.HighValue => (Color)colorDictionary["DangerColor"],
                ColorStyles.LowValue => (Color)colorDictionary["SuccessColor"],
                ColorStyles.Info => (Color)colorDictionary["InfoColor"],
                _ => Color.Default,
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
