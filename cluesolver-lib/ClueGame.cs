using System;
using System.Collections.Generic;

namespace cluesolver
{
    public class ClueGame
    {
        private static string ENVELOPE => "*envelope*";

        public ClueGame(ISet<string> players, ISet<Card> cards)
        {
            Players = new SortedSet<string>(players);
            Players.Add(ENVELOPE);

            CardConstraints = new Dictionary<Card, Constraint<string>>();

            // setup cards
            var allCards = new Dictionary<string, ISet<Card>>();

            foreach (Card card in cards)
            {
                if (!allCards.ContainsKey(card.Category))
                {
                    allCards.Add(card.Category, new SortedSet<Card>());
                }
                allCards[card.Category].Add(card);

                CardConstraints[card] = new Constraint<string>(Players);
            }

            Cards = new Dictionary<string, ISet<Card>>(allCards);

            // setup each players collection of constraints
            PlayerConstraints = new Dictionary<string, ISet<Constraint<Card>>>();
            foreach (var player in Players)
            {
                PlayerConstraints[player] = new HashSet<Constraint<Card>>();
            }

            // setup envelope constraints
            foreach (var category in Cards.Keys)
            {
                // add a constraint of having at least one of each category
                PlayerConstraints[ENVELOPE].Add(new Constraint<Card>(Cards[category], 1));
            }
        }

        /// <summary>
        /// A dictionary of the card constraints, by card
        /// </summary>
        /// <value></value>
        private IDictionary<Card, Constraint<string>> CardConstraints { get; set; }

        /// <summary>
        /// A dictionary of sets of player constraints, by player
        /// </summary>
        /// <value></value>
        public IDictionary<string, ISet<Constraint<Card>>> PlayerConstraints { get; }

        /// <summary>
        /// The players in the game
        /// </summary>
        /// <value></value>
        public ISet<string> Players { get; }

        /// <summary>
        /// A dictionary of cards, by card category
        /// </summary>
        /// <value></value>
        public IReadOnlyDictionary<string, ISet<Card>> Cards { get; }

        public ISet<string> GetPotentialOwners(Card card) => CardConstraints[card].Candidates;


        public void AddSuggestionResults(string suggester, ISet<Card> cards, ISet<string> helpers)
        {
            // create the set of players that didn't help
            // start with all players, then remove those that helped (or *may* have been able to help)
            var nonHelpers = new SortedSet<string>(Players);
            foreach (string helper in helpers)
            {
                nonHelpers.Remove(helper);
            }

            // if # of helpers < # of cards, then neither the helper nor the envelope can be ruled out
            if (helpers.Count < cards.Count)
            {
                // the envelope *could* have helped, so don't eliminate it
                nonHelpers.Remove(ENVELOPE);
                // suggester *could* be an unknown helper, so don't eliminate them
                nonHelpers.Remove(suggester);
            }

            // if they didn't help, they can't be candidates for any of the cards suggested, so
            // remove each non-helper as a potential card owner
            foreach (string nonHelper in nonHelpers)
            {
                foreach (Card card in cards)
                {
                    EliminateCardCandidate(card, nonHelper);
                }
            }

            // add player constraints
            foreach (var helper in helpers)
            {
                AddPlayerConstraint(helper, cards);
                // PlayerConstraints[helper].Add(new Constraint<Card>(cards));
            }

            // UpdatePlayerConstraints();
        }

        private void EliminateCardCandidate(Card card, string candidate)
        {
            CardConstraints[card].Candidates.Remove(candidate);
            foreach (var playerConstraint in PlayerConstraints[candidate])
            {
                playerConstraint.Candidates.Remove(card);
            }
        }

        public void AddPlayerConstraint(string player, ISet<Card> cards, int count = 1)
        {
            var constraintCards = new SortedSet<Card>(cards);
            // remove any cards that can't be owned by the player
            foreach (var card in cards)
            {
                if (!GetPotentialOwners(card).Contains(player))
                {
                    constraintCards.Remove(card);
                }
            }

            // create a constraint with just the possible cards
            var constraint = new Constraint<Card>(constraintCards, count);

            // if it's not solved, add it as an unsolved constraint
            if (!constraint.IsSolved)
            {
                PlayerConstraints[player].Add(constraint);
            }
            // otherwise, eliminate other owner candidates
            else
            {
                foreach (var card in constraint.Candidates)
                {
                    CardConstraints[card].Candidates.IntersectWith(new SortedSet<string> { player });
                }
            }
        }
    }

}