namespace EnergyPriceInspector.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class BaseViewModel : BindableObject
    {
        private bool _isBusy = false;
        private string _title = string.Empty;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
                return false;

            property = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
