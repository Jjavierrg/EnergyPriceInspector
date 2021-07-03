namespace EnergyPriceInspector.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class EnumerableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) => new ObservableCollection<T>(enumerable);
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (!(enumerable?.Any() ?? false))
                return;

            if (action == null)
                return;

            foreach (var item in enumerable)
                action(item);
        }
    }
}
