namespace EnergyPriceInspector.Models
{
    public interface IConfiguration
    {
        string ApiEndpoint { get; set; }
        string ApiToken { get; set; }
    }
}