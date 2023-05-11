using ConsoleTableExt;
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
        public string FormatInputCollection(params object[] collection)
        {
            if (collection[0].GetType().IsArray)
            {
                collection = ((IEnumerable)collection[0]).Cast<object>().Select(item => item).ToArray();
            }

            return string.Join(", ", collection.Select(item => item));
        }

        public void FormatStackDTO(List<StackDTO>? stacks)
        {
            var tableData = new List<List<Object>>();

            foreach (var stack in stacks)
            {
                tableData.Add(new List<object> { stack.StackName });
            }

            if (tableData.Count > 0)
            {
                ConsoleTableBuilder
                    .From(tableData)
                    .WithTitle("Stacks")
                    .ExportAndWriteLine();

                return;
            }

            return;
        }

        internal void FormatFlashcardDTO(List<FlashcardDTO> cards)
        {
            var tableData = new List<List<Object>>();

            foreach (var card in cards)
            {
                tableData.Add(new List<object> { card.CardId, card.CardFront, card.CardBack });
            }

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("Flashcards")
                .WithColumn("Card Id", "Front", "Back")
                .ExportAndWriteLine();
        }
    }
}
