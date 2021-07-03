namespace EnergyPriceInspector.Models
{
    using Xamarin.Forms;

    public class UserConfiguration: BindableObject
    {
        private int _geoId;

        public int GeoId
        {
            get { return _geoId; }
            set { _geoId = value; OnPropertyChanged(); }
        }

    }
}
