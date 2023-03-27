using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Formatter
    {
        /// <summary>
        /// Formats a collection into a sequentiel string of collection items.
        /// </summary>
        /// <param name="collection">The collection that needs to be formatted</param>
        /// <returns>A formatted string version of collection. E.g. [1, 2, 3] becomes '1, 2, 3'</returns>
        public static string FormatInputCollection(params object[] collection)
        {
            if (collection[0].GetType().IsArray)
            {
                collection = ((IEnumerable)collection[0]).Cast<object>().Select(item => item).ToArray();
            }

            return string.Join(", ", collection.Select(item => item));
        }
    }
}
