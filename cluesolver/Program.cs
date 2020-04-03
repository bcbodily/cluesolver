using System;
using System.Collections.Generic;
using System.Linq;

namespace cluesolver
{
    class Program
    {
        private const string MENUITEM_EXIT = "EXIT";
        private const string MENUITEM_MAKE_SUGGESTION = "Make a Suggestion";

        private const string MENUITEM_MAKE_SUGGESTION_WITH_RESPONSES = "Make a Suggestion (w/ Responses)";

        private const string MENUITEM_MAKE_REVELATION = "Make a Revelation";

        private const string MENUITEM_SHOW_HANDS = "Show Hands";

        private const string MENUITEM_SHOW_CARD_POSSIBILITIES = "Show Cards";

        private const string MENUITEM_SHOW_ENVELOPE_CARDS = "Show Envelope";

        private const string MENUITEM_SHOW_PLAYER_POSSIBILITIES = "Show Players";

        private const string MENUITEM_SHOW_SUGGESTION_HISTORY = "Show Suggestion History";

        private static string[] MENU_ITEMS = new string[] { MENUITEM_SHOW_ENVELOPE_CARDS, MENUITEM_MAKE_SUGGESTION, MENUITEM_MAKE_SUGGESTION_WITH_RESPONSES, MENUITEM_MAKE_REVELATION, MENUITEM_SHOW_SUGGESTION_HISTORY, MENUITEM_SHOW_HANDS, MENUITEM_SHOW_CARD_POSSIBILITIES, MENUITEM_SHOW_PLAYER_POSSIBILITIES, MENUITEM_EXIT };

        static void Main(string[] args)
        {
            IClueGame game = new ClueGame(new SortedSet<string> { "dad", "mom", "james", "brynn", "autumn", "daniel" }, ClueMasterDetective.CARDS_ALL);

            var solver = new ClueSolver(game);

            solver.AddEntirePlayerHand("dad", game.PlayerHand("dad"));

            var selectedItem = "";
            while (selectedItem != MENUITEM_EXIT)
            {
                var selectedNumber = ShowMenu("Main Menu", "Option", MENU_ITEMS);
                selectedItem = MENU_ITEMS[selectedNumber - 1];
                switch (selectedItem)
                {
                    case MENUITEM_SHOW_ENVELOPE_CARDS:
                        ShowPlayerPossibilities(solver, game.EnvelopePlayer);
                        break;
                    case MENUITEM_SHOW_HANDS:
                        ShowHands(game);
                        break;
                    case MENUITEM_SHOW_CARD_POSSIBILITIES:
                        ShowCardPossibilities(solver);
                        break;
                    case MENUITEM_SHOW_PLAYER_POSSIBILITIES:
                        ShowPlayersPossibilities(game, solver);
                        break;
                    case MENUITEM_SHOW_SUGGESTION_HISTORY:
                        ShowSuggestionHistory(game);
                        break;
                    case MENUITEM_MAKE_REVELATION:
                        var player = GetPlayer(game);
                        if (player != null)
                        {
                            var card = GetCard(game);
                            if (card.HasValue)
                            {
                                var revelation = new Revelation(player, card.Value);
                                solver.AddRevelation(revelation);
                            }
                        }
                        break;
                    case MENUITEM_MAKE_SUGGESTION:
                        var suggester = GetPlayer(game);
                        if (suggester != null)
                        {
                            var cards = GetOneOfEachCard(game).ToList();
                            if (cards.Count > 0)
                            {
                                var suggestion = new Suggestion(suggester, cards);
                                var revelations = game.MakeSuggestion(suggestion);
                                solver.AddSuggestionResults(suggestion, revelations);
                            }
                        }
                        break;
                    case MENUITEM_MAKE_SUGGESTION_WITH_RESPONSES:
                        var swrSuggester = GetPlayer(game);
                        if (swrSuggester != null)
                        {
                            var cards = GetOneOfEachCard(game).ToList();
                            if (cards.Count > 0)
                            {
                                var suggestion = new Suggestion(swrSuggester, cards);

                                // get revelations
                                var helpers = GetPlayerList(game);
                                var revelations = new List<Revelation>();
                                foreach (var helper in helpers)
                                {
                                    revelations.Add(new Revelation(helper, null));
                                }

                                solver.AddSuggestionResults(suggestion, revelations);
                            }
                        }
                        break;
                }
            }

            Console.WriteLine($"Input was {selectedItem}");

        }

        private static void ShowHands(IClueGame game)
        {
            Console.WriteLine();
            Console.WriteLine("PLAYER HANDS");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            foreach (var player in game.AllPlayers)
            {
                Console.WriteLine($"{player}");
                Console.WriteLine(string.Join(", ", game.PlayerHand(player)));
                Console.WriteLine();
            }
        }

        private static Card? GetCard(IClueGame game, string category = "")
        {
            var selectedNumber = 0;
            if (category == "")
            {
                var categories = game.CardsByCategory.Keys;
                selectedNumber = ShowMenu("Select Category", "Pick", game.CardsByCategory.Keys.ToList(), true);
                if (selectedNumber != 0)
                {
                    category = game.CardsByCategory.Keys.ToList()[selectedNumber - 1];
                }
                else
                {
                    return null;
                }
            }

            IList<string> cardNames = new List<string>();
            foreach (var card in game.CardsByCategory[category].ToList())
            {
                cardNames.Add(card.ToString());
            }
            selectedNumber = ShowMenu($"Select Card ({category})", "Pick", cardNames, true);
            if (selectedNumber != 0)
                return game.CardsByCategory[category].ToList()[selectedNumber - 1];
            // else
            return null;
        }

        private static IEnumerable<Card> GetOneOfEachCard(IClueGame game)
        {
            ISet<Card> cards = new HashSet<Card>();
            var categories = game.CardsByCategory.Keys;
            foreach (var category in categories)
            {
                var card = GetCard(game, category);
                if (card.HasValue)
                {
                    cards.Add(card.Value);
                }
                else
                {
                    return new Card[] { };
                }
            }
            return cards;
        }

        private static string GetPlayer(IClueGame game)
        {
            var selectedNumber = ShowMenu("Select Player", "Pick", game.AllPlayers.ToList(), true);
            if (selectedNumber != 0)
                return game.AllPlayers.ToList()[selectedNumber - 1];

            // else
            return null;
        }

        private static IEnumerable<string> GetPlayerList(IClueGame game)
        {
            ISet<string> players = new SortedSet<string>();
            IList<string> availPlayers = game.AllPlayers.ToList();
            var selectedNumber = -1;
            do
            {
                selectedNumber = ShowMenu("Select Player", "Pick", availPlayers, true);
                if (selectedNumber != 0)
                {
                    players.Add(availPlayers[selectedNumber - 1]);
                    availPlayers.Remove(availPlayers[selectedNumber - 1]);
                }
            } while (selectedNumber != 0);

            return players;
        }
        private static int ShowMenu(string title, string prompt, IEnumerable<string> items, bool showCancel = false)
        {
            var menuItems = items.ToList();
            var count = menuItems.Count;
            var itemNum = -1;

            while (itemNum < (showCancel ? 0 : 1) || itemNum > count)
            {
                title = $"| {title} |";
                var underline = "";
                foreach (var character in title)
                {
                    underline += "-";
                }
                Console.WriteLine(underline);
                Console.WriteLine(title.ToUpper());
                Console.WriteLine(underline);
                var num = 1;
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"  {num++}) {item}");
                }
                if (showCancel)
                {
                    Console.WriteLine();
                    Console.WriteLine($"  0) Cancel");
                }
                Console.WriteLine();
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();
                if (menuItems.Contains(input))
                {
                    itemNum = menuItems.IndexOf(input) + 1;
                }
                else
                {
                    try
                    {
                        itemNum = int.Parse(input);
                    }
                    catch (FormatException)
                    {
                        // just ignore
                        itemNum = 0;
                    }
                }
            }
            return itemNum;
        }

        private static void ShowCardPossibilities(ClueSolver solver)
        {
            Console.WriteLine();
            Console.WriteLine("BY CARD");
            Console.WriteLine("--------------------");
            foreach (var category in solver.Game.CardsByCategory.Keys)
            {
                Console.WriteLine();
                Console.WriteLine($"{category.ToUpperInvariant()}");
                foreach (Card card in solver.Game.CardsByCategory[category])
                {
                    Console.Out.Write($"{card}:".PadRight(20));
                    Console.Write(string.Join(", ", solver.GetPotentialOwners(card)));
                    Console.WriteLine();
                }
            }

        }

        private static void ShowPlayerPossibilities(ClueSolver solver, string player)
        {
            Console.WriteLine($"{player}");
            foreach (var constraint in solver.GetPlayerConstraints(player))
            {
                Console.Write($"   {constraint.NumberOfOwnedCandidates}:".PadRight(10));
                Console.Write(string.Join(", ", constraint.Candidates));
                Console.WriteLine();
            }
        }

        private static void ShowPlayersPossibilities(IClueGame game, ClueSolver solver)
        {
            Console.WriteLine();
            Console.WriteLine("BY PLAYER");
            Console.WriteLine("--------------------");
            Console.WriteLine();

            foreach (var player in game.AllPlayers)
            {
                ShowPlayerPossibilities(solver, player);
            }
        }

        private static void ShowPlayers(IClueGame game)
        {
            // get sorted player list
            var sortedPlayers = new List<string>(new SortedSet<string>(game.AllPlayers));
            var num = 0;
            foreach (var player in sortedPlayers)
            {
                Console.WriteLine($"{++num}) {player}");
            }
        }

        private static void ShowSuggestionHistory(IClueGame game)
        {
            var longestNameLength = 0;
            foreach (var player in game.AllPlayers)
            {
                longestNameLength = player.Length > longestNameLength ? player.Length : longestNameLength;
            }

            Console.WriteLine("Suggestion History");
            Console.WriteLine("------------------");
            foreach (var suggestion in game.Suggestions)
            {
                Console.WriteLine($" {suggestion.Suggester.PadRight(longestNameLength)}: {string.Join(", ", suggestion.SuggestedCards)}");
            }
            Console.WriteLine();
        }

    }
}
