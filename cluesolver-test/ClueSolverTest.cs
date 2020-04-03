using System;
using System.Collections.Generic;
using Xunit;


namespace cluesolver
{
    public class ClueSolverTest
    {

        private ISet<Card> GetCards(string category, string[] names)
        {
            var cards = new SortedSet<Card>();
            foreach (string name in names)
            {
                cards.Add(new Card(category: category, title: name));
            }

            return cards;
        }
        private ISet<Card> GetRoomCards()
        {
            var category = "room";
            var names = new string[] { "conservatory", "billards room" };
            return GetCards(category, names);

        }
        private ISet<Card> GetWeaponCards()
        {
            var category = "weapon";
            var names = new string[] { "rope", "knife" };
            return GetCards(category, names);
        }

        private void ShowPossibleOwners(ClueSolver game)
        {
            var allCards = new SortedSet<Card>();
            foreach (ISet<Card> cards in game.Cards.Values)
            {
                allCards.UnionWith(cards);
            }
            foreach (Card card in allCards)
            {
                Console.Out.WriteLine(card);
                foreach (string name in game.GetPotentialOwners(card))
                {
                    Console.Out.WriteLine($"\t{name}");
                }
            }
        }
    }
}