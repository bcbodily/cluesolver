using System;
using System.Collections.Generic;

namespace cluesolver
{
    public interface IClueGame
    {
        IEnumerable<Card> AllCards { get; }

        IReadOnlyDictionary<string, ISet<Card>> CardsByCategory { get; }

        string EnvelopePlayer { get; }

        IEnumerable<string> AllPlayers { get; }

        IEnumerable<Card> PlayerHand(string player);

        IEnumerable<Suggestion> Suggestions {get;}

        IEnumerable<Revelation> MakeSuggestion(Suggestion suggestion);
    }
}