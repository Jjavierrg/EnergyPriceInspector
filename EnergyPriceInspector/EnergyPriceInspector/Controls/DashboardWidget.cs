namespace EnergyPriceInspector.Controls
{

    using Xamarin.Forms;

    public class DashboardWidget : ContentView
    {
        public enum ColorStyles
        {
            Info,
            HighValue,
            LowValue
        }

        public static readonly BindableProperty PriceProperty = BindableProperty.Create("Price", typeof(double?), typeof(DashboardWidget), null);
        public static readonly BindableProperty ColorStyleProperty = BindableProperty.Create("ColorStyle", typeof(ColorStyles), typeof(DashboardWidget), ColorStyles.Info);

        public double? Price
        {
            get => (double?)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public ColorStyles ColorStyle
        {
            get => (ColorStyles)GetValue(ColorStyleProperty);
            set => SetValue(ColorStyleProperty, value);
        }
    }
}