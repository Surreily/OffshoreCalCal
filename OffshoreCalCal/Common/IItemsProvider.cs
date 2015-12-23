using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffshoreCalCal.Common
{
    /// <summary>
    /// An interface for a provider of collection details.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IItemsProvider<T>
    {
        /// <summary>
        /// Fetches the total number of available items.
        /// </summary>
        /// <returns></returns>
        int FetchCount();

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        IList<T> FetchRange(int startIndex, int count);
    }
}
