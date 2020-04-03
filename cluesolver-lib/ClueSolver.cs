using System;
using System.Collections.Generic;
using System.Linq;

namespace cluesolver
{
    public class ClueSolver
    {
        public IClueGame Game { get; }

        public ClueSolver(IClueGame game)
        {

            Game = game;
            var cards = Game.AllCards;

            CardConstraints = new Dictionary<Card, Constraint<Card, string>>();

            // setup cards
            var allCards = new Dictionary<string, ISet<Card>>();

            foreach (Card card in cards)
            {
                if (!allCards.ContainsKey(card.Category))
                {
                    allCards.Add(card.Category, new SortedSet<Card>());
                }
                allCards[card.Category].Add(card);

                CardConstraints[card] = new Constraint<Card, string>(card, game.AllPlayers);
                CardConstraints[card].Solved += CardConstraint_Solved;
            }

            Cards = new Dictionary<string, ISet<Card>>(allCards);

            // setup each players collection of constraints
            PlayerConstraints = new Dictionary<string, ISet<Constraint<string, Card>>>();
            foreach (var player in game.AllPlayers)
            {
                PlayerConstraints[player] = new HashSet<Constraint<string, Card>>();
            }

            // setup envelope constraints
            foreach (var category in Cards.Keys)
            {
                // add a constraint of having at least one of each category
                PlayerConstraints[Game.EnvelopePlayer].Add(new Constraint<string, Card>(Game.EnvelopePlayer, Cards[category], 1));
            }
        }

        /// <summary>
        /// A dictionary of the card constraints, by card
        /// </summary>
        /// <value></value>
        private IDictionary<Card, Constraint<Card, string>> CardConstraints { get; set; }

        /// <summary>
        /// A dictionary of sets of player constraints, by player
        /// </summary>
        /// <value></value>
        private IDictionary<string, ISet<Constraint<string, Card>>> PlayerConstraints { get; }

        /// <summary>
        /// A dictionary of cards, by card category
        /// </summary>
        /// <value></value>
        public IReadOnlyDictionary<string, ISet<Card>> Cards { get; }

        public IEnumerable<string> GetPotentialOwners(Card card) => CardConstraints[card].Candidates;

        public IEnumerable<Constraint<string, Card>> GetPlayerConstraints(string player) => PlayerConstraints[player];

        public void AddEntirePlayerHand(string player, IEnumerable<Card> cards)
        {
            var allCards = Game.AllCards.ToHashSet();
            var otherCards = allCards.Except(cards);
            foreach (var card in cards)
            {
                var rev = new Revelation(player, card);
                AddRevelation(rev);
            }

            RemovePossibility(otherCards, new string[] { player });
        }

        public void AddRevelation(Revelation revelation)
        {
            // if the card is known, update possibilities/constraints
            if (revelation.Card.HasValue)
            {
                var card = revelation.Card.Value;
                // var constraint = CardConstraints[card];
                var nonOwners = new SortedSet<string>(Game.AllPlayers);
                nonOwners.Remove(revelation.Player);
                RemovePossibility(new SortedSet<Card> { card }, nonOwners);
                AddPlayerConstraint(revelation.Player, new Card[] { card });
            }
        }

        public void AddSuggestionResults(Suggestion suggestion, IEnumerable<Revelation> revelations)
        {
            ISet<Card> cards = new SortedSet<Card>(suggestion.SuggestedCards);
            ISet<string> helpers = new SortedSet<string>();
            foreach (var revelation in revelations)
            {
                helpers.Add(revelation.Player);
            }

            // create the set of players that didn't help
            // start with all players, then remove those that helped (or *may* have been able to help)
            var nonHelpers = new SortedSet<string>(Game.AllPlayers);
            nonHelpers.ExceptWith(helpers);

            // if # of helpers < # of cards, then neither the suggester nor the envelope can be ruled out
            if (helpers.Count < cards.Count)
            {
                // the envelope *could* have helped, so don't eliminate it
                nonHelpers.Remove(Game.EnvelopePlayer);
                // suggester *could* be an unknown helper, so don't eliminate them
                nonHelpers.Remove(suggestion.Suggester);
            }

            // remove the possibility of any non-helper having any of the suggested cards
            RemovePossibility(cards, nonHelpers);

            // add player constraints
            foreach (var helper in helpers)
            {
                AddPlayerConstraint(helper, cards);
            }

        }

        /// <summary>
        /// Removes the possibility of a particular card belonging to a particular player
        /// </summary>
        /// <param name="cards">the cards that the players cannot own</param>
        /// <param name="players">the players that cannot own the cards</param>
        public void RemovePossibility(IEnumerable<Card> cards, IEnumerable<string> players)
        {
            // remove the players from the card's list
            foreach (var player in players)
            {
                foreach (var card in cards)
                {
                    CardConstraints[card].RemoveCandidate(player);

                    // remove the card from any of the player's constraints
                    foreach (var playerConstraint in PlayerConstraints[player])
                    {
                        playerConstraint.RemoveCandidate(card);
                    }
                }
            }
        }

        private void AddPlayerConstraint(string player, IEnumerable<Card> cards, int count = 1)
        {
            // card to be in the constraint (before being filtered)
            ISet<Card> constraintCards = new SortedSet<Card>(cards);

            // remove any cards that can't be owned by the player
            foreach (var card in cards)
            {
                ISet<string> possibleOwners = new SortedSet<string>(GetPotentialOwners(card));
                if (!possibleOwners.Contains(player))
                {
                    constraintCards.Remove(card);
                }
            }

            // create a constraint with just the possible cards
            var constraint = new Constraint<string, Card>(player, constraintCards, count);

            // if it's not solved, add it as an unsolved constraint
            if (!constraint.IsSolved)
            {
                PlayerConstraints[player].Add(constraint);
                constraint.Solved += PlayerConstraint_Solved;
            }
            // otherwise, eliminate other owner candidates
            else
            {   // still add it
                PlayerConstraints[player].Add(constraint);
                HandleSolvedPlayerConstraint(constraint);
            }
        }

        private void CardConstraint_Solved(object sender, EventArgs e)
        {
            var constraint = sender as Constraint<Card, string>;

            // Console.WriteLine();
            // Console.WriteLine();
            // Console.WriteLine("HANDLING CARD");
            // Console.WriteLine($"{constraint.Owner} must be {string.Join(", ", constraint.Candidates)}");
            // Console.WriteLine();
            // Console.WriteLine();
            HandleSolvedCardConstraint(constraint);
            constraint.Solved -= CardConstraint_Solved;
        }

        private void PlayerConstraint_Solved(object sender, EventArgs e)
        {
            var constraint = sender as Constraint<string, Card>;

            // Console.WriteLine();
            // Console.WriteLine();
            // Console.WriteLine("HANDLING PLAYER");
            // Console.WriteLine($"{constraint.Owner} must have {string.Join(", ", constraint.Candidates)}");
            // Console.WriteLine();
            // Console.WriteLine();
            HandleSolvedPlayerConstraint(constraint);
            constraint.Solved -= PlayerConstraint_Solved;
        }

        private void HandleSolvedCardConstraint(Constraint<Card, string> constraint)
        {
            // card that has a single owner
            // remove card from all players that are not the owner
            if (constraint.IsSolved)
            {
                var owners = new SortedSet<string>(constraint.Candidates);
                foreach (var player in Game.AllPlayers)
                {
                    if (!owners.Contains(player))
                    {
                        foreach (var playerConstraint in PlayerConstraints[player])
                        {
                            playerConstraint.RemoveCandidate(constraint.Owner);
                        }
                    }

                    // if the envelope is the owner, remove the other cards in the category
                    if (owners.Contains(Game.EnvelopePlayer))
                    {
                        // eliminate other cards from category
                        var category = constraint.Owner.Category;
                        var cardsInCat = new SortedSet<Card>(Game.CardsByCategory[category]);
                        cardsInCat.Remove(constraint.Owner);
                        RemovePossibility(cardsInCat, new string[] { Game.EnvelopePlayer });
                    }
                }
            }
        }

        private void HandleSolvedPlayerConstraint(Constraint<string, Card> constraint)
        {
            // make sure it really is solved
            if (constraint.IsSolved)
            {
                ISet<string> nonOwners = new SortedSet<string>(Game.AllPlayers);
                nonOwners.Remove(constraint.Owner);
                // remove the possibility of any non-owner for each candidate card
                RemovePossibility(constraint.Candidates, nonOwners);
            }
        }
    }

}