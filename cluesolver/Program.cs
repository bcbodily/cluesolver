using System;
using System.Collections.Generic;

namespace cluesolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new ClueGame(new SortedSet<string> { "mom", "dad", "james", "brynn", "autumn", "daniel" }, ClueMasterDetective.CARDS_ALL);

            // do a round
            var suggester = "dad";
            ISet<Card> cards = new SortedSet<Card> { ClueMasterDetective.CARD_SUSPECT_COL_MUSTARD, ClueMasterDetective.CARD_ROOM_BILLIARD_ROOM, ClueMasterDetective.CARD_WEAPON_CANDLESTICK };
            ISet<string> helpers = new SortedSet<string> { "mom", "james", "brynn" };
            game.AddSuggestionResults(suggester, cards, helpers);

            cards = new HashSet<Card> { ClueMasterDetective.CARD_SUSPECT_COL_MUSTARD, ClueMasterDetective.CARD_ROOM_CARRIAGE_HOUSE, ClueMasterDetective.CARD_WEAPON_HORSESHOE };
            helpers = new SortedSet<string> { "mom" };
            suggester = "dad";
            game.AddSuggestionResults(suggester, cards, helpers);

            ShowGame(game);

            ShowPlayerPossibilities(game);

        }

        private static void ShowGame(ClueGame game)
        {
            Console.WriteLine();
            Console.WriteLine("BY CARD");
            Console.WriteLine("--------------------");
            foreach (var category in game.Cards.Keys)
            {
                Console.WriteLine();
                Console.WriteLine($"{category.ToUpperInvariant()}");
                foreach (Card card in game.Cards[category])
                {
                    Console.Out.Write($"{card}:".PadRight(20));
                    Console.Write(string.Join(", ", game.GetPotentialOwners(card)));
                    Console.WriteLine();
                }
            }

        }

        private static void ShowPlayerPossibilities(ClueGame game)
        {
            Console.WriteLine();
            Console.WriteLine("BY PLAYER");
            Console.WriteLine("--------------------");
            Console.WriteLine();

            foreach (var player in game.PlayerConstraints.Keys)
            {
                Console.WriteLine($"{player}");
                foreach (var constraint in game.PlayerConstraints[player])
                {
                    Console.Write($"   {constraint.NumberOfCandidatesOwned}:".PadRight(10));
                    Console.Write(string.Join(", ", constraint.Candidates));
                    Console.WriteLine();
                }
            }
        }
    }
}
