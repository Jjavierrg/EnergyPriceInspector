namespace EnergyPriceInspector.Services
{
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class PropertyStorageService : IStorageService
    {
        public Task SaveDataAsync<T>(string key, T data)
        {
            if (string.IsNullOrEmpty(key))
                return Task.CompletedTask;

            var jsonData = data == null ? "" : JsonConvert.SerializeObject(data);
            Application.Current.Properties[key] = jsonData;

            return Application.Current.SavePropertiesAsync();
        }

        public Task<T> LoadDataAsync<T>(string key)
        {
            var result = Application.Current.Properties.TryGetValue(key, out object value) ? JsonConvert.DeserializeObject<T>(value.ToString()) : default;
            return Task.FromResult(result);
        }
    }
}
