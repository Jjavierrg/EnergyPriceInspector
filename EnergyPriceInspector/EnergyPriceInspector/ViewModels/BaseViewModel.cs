namespace EnergyPriceInspector.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class BaseViewModel : BindableObject
    {
        private bool _isBusy = false;
        private string _title = string.Empty;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, "IsBusy", OnBusyChanged);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected virtual void OnBusyChanged() { }

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
                return false;

            property = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected async Task ExecuteWithBusyIndicatorControl(Func<Task> func)
        {
            try
            {
                IsBusy = true;
                await func();
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task<TOut> ExecuteWithBusyIndicatorControl<TOut>(Func<Task<TOut>> func)
        {
            try
            {
                IsBusy = true;
                return await func();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
