using System;
using System.Collections.Generic;

namespace cluesolver
{
    public struct Suggestion
    {
        /// <summary>
        /// Creates a new <see cref="Suggestion"/> with a specified suggester and specified cards
        /// </summary>
        /// <param name="suggester"></param>
        /// <param name="cards"></param>
        public Suggestion(string suggester, IEnumerable<Card> cards)
        {
            Suggester = suggester;
            SuggestedCards = new SortedSet<Card>(cards);
        }

        /// <summary>
        /// The player making the suggestion
        /// </summary>
        /// <value></value>
        public string Suggester { get; }

        /// <summary>
        /// The cards included in the suggestion
        /// </summary>
        /// <value></value>
        public IEnumerable<Card> SuggestedCards { get; }
    }
}