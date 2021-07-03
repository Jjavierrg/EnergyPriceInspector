namespace EnergyPriceInspector.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Configuration: IConfiguration
    {
        public string ApiEndpoint { get; set; }
        public string ApiToken{ get; set; }
    }
}
