using System;
using System.Collections.Generic;

namespace cluesolver
{
    public class SuggestionBuilder
    {
        private string suggester { get; set; }
        private ISet<Card> cards { get; set; } = new HashSet<Card>();

        public Suggestion Build() => new Suggestion(suggester, cards as IEnumerable<Card>);

        public SuggestionBuilder AddCard(Card card)
        {
            cards.Add(card);
            return this;
        }

        public SuggestionBuilder Suggester(string suggester)
        {
            this.suggester = suggester;
            return this;
        }

    }
}