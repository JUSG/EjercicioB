using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eShopOnContainers.Core.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in source)
            {
                collection.Add(item);
            }

            return collection;
        }

        /// <summary>
        /// Clear the collection and add a range
        /// </summary>
        /// <typeparam name="T">Collection Type</typeparam>
        /// <param name="list">target list</param>
        /// <param name="range">Range to add</param>
        public static void ClearAndAddRange<T>(this ICollection<T> list, ICollection<T> range)
        {
            if (list != null
                && range != null)
            {
                list.Clear();

                foreach (var item in range)
                {
                    list.Add(item);
                }
            }
        }
    }
}