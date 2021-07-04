namespace EnergyPriceInspector.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

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
        public static async Task<IEnumerable<TOut>> SelectAsync<TIn, TOut>(this IEnumerable<TIn> enumerable, Func<TIn, Task<TOut>> selectFunc)
        {
            if (!(enumerable?.Any() ?? false))
                return Array.Empty<TOut>();

            if (selectFunc == null)
                return Array.Empty<TOut>();

            var resultList = new List<TOut>();
            foreach (var item in enumerable)
            {
                var result = await selectFunc(item);
                resultList.Add(result);
            }

            return resultList;
        }
    }
}
