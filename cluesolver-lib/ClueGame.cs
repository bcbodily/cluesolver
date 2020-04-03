using System;
using System.Collections.Generic;

namespace cluesolver
{
    public class ClueGame : IClueGame
    {
        private static string ENVELOPE_PLAYER_NAME => "*envelope*";

        public ClueGame(IEnumerable<string> players, IEnumerable<Card> cards)
        {
            var initPlayers = new HashSet<string>(players);
            initPlayers.Add(ENVELOPE_PLAYER_NAME);
            AllPlayers = new SortedSet<string>(initPlayers);
            AllCards = new SortedSet<Card>(cards);

            // categorize cards
            IDictionary<string, ISet<Card>> cardsByCat = new Dictionary<string, ISet<Card>>();
            foreach (Card card in AllCards)
            {
                if (!cardsByCat.ContainsKey(card.Category))
                {
                    cardsByCat.Add(card.Category, new SortedSet<Card>());
                }
                cardsByCat[card.Category].Add(card);
            }
            CardsByCategory = new Dictionary<String, ISet<Card>>(cardsByCat);

            // create hands
            Hands = new Dictionary<string, ISet<Card>>();
            foreach (var player in AllPlayers)
            {
                Hands[player] = new HashSet<Card>();
            }

            // add one card from each cat to envelope
            foreach (var category in CardsByCategory.Keys)
            {
                IList<Card> cardsToPickFrom = new List<Card>(CardsByCategory[category]);
                Hands[EnvelopePlayer].Add(cardsToPickFrom[random.Next(0, cardsToPickFrom.Count)]);
            }

            // make hands
            ISet<Card> deckCards = new HashSet<Card>(AllCards);
            // remove cards in the envelope
            deckCards.ExceptWith(Hands[EnvelopePlayer]);

            IList<Card> deck = new List<Card>(deckCards);

            ISet<string> playerPlayers = new SortedSet<string>(AllPlayers);
            playerPlayers.Remove(EnvelopePlayer);
            while (deck.Count > 0)
            {
                foreach (var player in playerPlayers)
                {
                    if (deck.Count > 0)
                    {
                        var card = deck[random.Next(0, deck.Count)];
                        Hands[player].Add(card);
                        deck.Remove(card);
                    }
                }
            }
        }

        private Random random = new Random(0);

        private IList<Suggestion> SuggestionList {get;} = new List<Suggestion>();

        private IDictionary<string, ISet<Card>> Hands { get; }

        public IEnumerable<Card> AllCards { get; }

        public IReadOnlyDictionary<string, ISet<Card>> CardsByCategory { get; }

        public string EnvelopePlayer => ENVELOPE_PLAYER_NAME;

        public IEnumerable<string> AllPlayers { get; }

        public IEnumerable<Suggestion> Suggestions => SuggestionList;

        public IEnumerable<Revelation> MakeSuggestion(Suggestion suggestion)
        {
            SuggestionList.Add(suggestion);
            var revelations = new HashSet<Revelation>();
            var playersToCheck = new SortedSet<string>(AllPlayers);
            playersToCheck.Remove(suggestion.Suggester);
            playersToCheck.Remove(EnvelopePlayer);
            foreach (var player in playersToCheck)
            {
                ISet<Card> cards = new HashSet<Card>(PlayerHand(player));
                cards.IntersectWith(suggestion.SuggestedCards);
                if (cards.Count > 0)
                {
                    revelations.Add(new Revelation(player, null));
                }
            }

            return revelations;
        }

        public IEnumerable<Card> PlayerHand(string player) => Hands[player];
    }
}