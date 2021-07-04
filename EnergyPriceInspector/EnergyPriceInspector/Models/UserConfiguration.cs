namespace EnergyPriceInspector.Models
{
    using Xamarin.Forms;

    public class UserConfiguration: BindableObject
    {
        public static GeoLocation DEFAULT_GEOLOCATION => new GeoLocation { Id = 8741, Name = Langs.Langs.Peninsula };
        private GeoLocation _geoLocation;
        public UserConfiguration()
        {
            GeoLocation = DEFAULT_GEOLOCATION;
        }

        public GeoLocation GeoLocation
        {
            get { return _geoLocation; }
            set { _geoLocation = value; OnPropertyChanged(); }
        }
    }
}
