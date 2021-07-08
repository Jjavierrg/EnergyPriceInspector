namespace EnergyPriceInspector.Behaviors
{
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class FadeInBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty DurationProperty = BindableProperty.Create("Duration", typeof(uint), typeof(FadeInBehavior), (uint)1500);

        public uint Duration
        {
            get => (uint)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.PropertyChanged -= OnPropertyChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Renderer")
                return;

            Task.Run(async () => await FadeElementWithDelay(sender as VisualElement));
        }

        private async Task FadeElementWithDelay(VisualElement element)
        {
            if (element == null)
                return;

            ViewExtensions.CancelAnimations(element);
            element.Opacity = 0;
            await element.FadeTo(1, Duration);
        }
    }
}
