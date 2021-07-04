namespace EnergyPriceInspector.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GeoLocation : IEquatable<GeoLocation>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj) => Equals(obj as GeoLocation);
        public bool Equals(GeoLocation other) => other != null && Id == other.Id && Name == other.Name;
        public override int GetHashCode() => HashCode.Combine(Id, Name);

        public static bool operator ==(GeoLocation left, GeoLocation right) => EqualityComparer<GeoLocation>.Default.Equals(left, right);
        public static bool operator !=(GeoLocation left, GeoLocation right) => !(left == right);
    }
}
