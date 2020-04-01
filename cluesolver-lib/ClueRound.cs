using System;
using System.Collections.Generic;

namespace cluesolver
{
    public struct ClueRound
    {
        public ISet<Card> Cards {get;}
        public ISet<String> Helpers {get;}

        public ClueRound(ISet<Card> cards, ISet<String> helpers) {
            Cards = new SortedSet<Card>(cards);
            Helpers = new SortedSet<String>(helpers);
        }
    }
}