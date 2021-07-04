namespace EnergyPriceInspector.Models
{
    using System;

    public class ApiResponse
    {
        public Indicator indicator { get; set; }
    }

    public class Indicator
    {
        public string name { get; set; }
        public string short_name { get; set; }
        public int id { get; set; }
        public bool composited { get; set; }
        public string step_type { get; set; }
        public bool disaggregated { get; set; }
        public Magnitud[] magnitud { get; set; }
        public Tiempo[] tiempo { get; set; }
        public Geo[] geos { get; set; }
        public DateTime values_updated_at { get; set; }
        public Value[] values { get; set; }
    }

    public class Magnitud
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Tiempo
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Geo
    {
        public int geo_id { get; set; }
        public string geo_name { get; set; }
    }

    public class Value
    {
        public float value { get; set; }
        public DateTime datetime { get; set; }
        public DateTime datetime_utc { get; set; }
        public DateTime tz_time { get; set; }
        public int geo_id { get; set; }
        public string geo_name { get; set; }
    }

}
