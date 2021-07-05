namespace EnergyPriceInspector.Controls
{
    using EnergyPriceInspector.Models;
    using Microcharts;
    using Microcharts.Forms;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms;

    public class PriceChart : ChartView
    {
        public static readonly BindableProperty ChartBackgroundColorProperty = BindableProperty.Create("ChartBackgroundColor", typeof(Color), typeof(PriceChart), Color.White);
        public static readonly BindableProperty YAxisTextPaintProperty = BindableProperty.Create("YAxisTextPaint", typeof(Color), typeof(PriceChart), Color.White);
        public static readonly BindableProperty YAxisLinesPaintProperty = BindableProperty.Create("YAxisLinesPaint", typeof(Color), typeof(PriceChart), Color.White);
        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create("LabelColor", typeof(Color), typeof(PriceChart), Color.White);
        public static readonly BindableProperty SerieColorProperty = BindableProperty.Create("SerieColor", typeof(Color), typeof(PriceChart), Color.White);
        public static readonly BindableProperty PricesProperty = BindableProperty.Create("Prices", typeof(IEnumerable<PriceInformation>), typeof(PriceChart), Array.Empty<PriceInformation>(), propertyChanged: HandleChartEntriesChanged);

        public PriceChart() => Chart = new LineChart
        {
            LineMode = LineMode.Straight,
            LabelTextSize = 20,
            LabelOrientation = Orientation.Vertical,
            ShowYAxisLines = true,
            ShowYAxisText = true,
            YAxisPosition = Position.Left,
            YAxisTextPaint = new SKPaint(new SKFont { Size = 20 }) { Color = SKColor.Parse(YAxisTextPaint.ToHex()) },
            YAxisLinesPaint = new SKPaint { Color = SKColor.Parse(YAxisLinesPaint.ToHex()) },
            Entries = new[] { new ChartEntry(0) },
            LabelColor = SKColor.Parse(LabelColor.ToHex()),
            BackgroundColor = SKColor.Parse(ChartBackgroundColor.ToHex())
        };

        public IEnumerable<PriceInformation> Prices
        {
            get => (IEnumerable<PriceInformation>)GetValue(PricesProperty);
            set => SetValue(PricesProperty, value);
        }

        public Color ChartBackgroundColor
        {
            get => (Color)GetValue(ChartBackgroundColorProperty);
            set => SetValue(ChartBackgroundColorProperty, value);
        }

        public IEnumerable<ChartEntry> Entries
        {
            get => ((LineChart)Chart).Entries;
            set => ((LineChart)Chart).Entries = value;
        }

        public Color YAxisTextPaint
        {
            get => (Color)GetValue(YAxisTextPaintProperty);
            set => SetValue(YAxisTextPaintProperty, value);
        }

        public Color YAxisLinesPaint
        {
            get => (Color)GetValue(YAxisLinesPaintProperty);
            set => SetValue(YAxisLinesPaintProperty, value);
        }

        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        public Color SerieColor
        {
            get => (Color)GetValue(SerieColorProperty);
            set => SetValue(SerieColorProperty, value);
        }

        private static void HandleChartEntriesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var value = newValue as IEnumerable<PriceInformation>;
            if (!(value?.Any() ?? false))
                return;

            ((PriceChart)bindable).Entries = value.Select((x, i) => new ChartEntry(x.Price)
            {
                Color = SKColor.Parse(((PriceChart)bindable).SerieColor.ToHex()),
                TextColor = SKColor.Parse(((PriceChart)bindable).SerieColor.ToHex()),
                Label = i % 3 == 0 ? x.Date.ToString("t") : string.Empty
            });
        }
    }
}
