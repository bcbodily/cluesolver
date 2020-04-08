using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cluesolver
{
    public class TextGameSolverView
    {
        public TextGameSolverView(IClueGame game, ClueSolver solver)
        {
            Game = game;
            Solver = solver;

            // create main menu
            MainMenu = new MenuBuilder<string, string>()
                .Title("CLUE Solver")
                .AddEntry("Show")
                .AddEntry("Inform")
                .AddEntry("Entry with a really long title")
                .AddSecondaryEntry("X", "Exit")
                .Build();

            ShowMenu = new MenuBuilder<string, string>()
                .Title("Show Menu")
                .AddEntry("Envelope")
                .AddEntry("All Players")
                .AddEntry("Cards")
                .AddEntry("Hands")
                .AddSecondaryEntry("X", "Cancel")
                .Build();

            MainMenu.ItemSelected += MainMenu_ItemSelected;
            ShowMenu.ItemSelected += ShowMenu_ItemSelected;

            MainMenu.Show(Console.Out, Console.In);
        }

        private IClueGame Game { get; }
        private ClueSolver Solver { get; }

        private Menu<string, string> MainMenu { get; }

        private Menu<string, string> ShowMenu { get; }


        public void MainMenu_ItemSelected(object sender, ItemSelectedEventArgs<string> e)
        {
            Console.WriteLine($"Selected: {e.Item.ToString()}");
            switch (e.Item.ToLower())
            {
                case "show":
                    ShowMenu.Show(Console.Out, Console.In);
                    break;
                case "exit":
                    break;
            }
            if (e.Item.ToLower() != "exit")
            {
                MainMenu.Show(Console.Out, Console.In);
            }
        }

        public void ShowMenu_ItemSelected(object sender, ItemSelectedEventArgs<string> eventArgs)
        {
            switch (eventArgs.Item.ToLower())
            {
                case "all players":
                    ShowPlayersPossibilities(Game, Solver);
                    break;
                case "cards":
                    ShowCardPossibilities(Solver);
                    break;
                case "hands":
                    ShowHands(Game);
                    break;
                case "envelope":
                    ShowPlayerPossibilities(Solver, Game.EnvelopePlayer);
                    break;
            }
        }

        private void ShowCardPossibilities(ClueSolver solver)
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

        private void ShowHands(IClueGame game)
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

        private void ShowPlayerPossibilities(ClueSolver solver, string player)
        {
            Console.WriteLine($"{player}");
            foreach (var constraint in solver.GetPlayerConstraints(player))
            {
                Console.Write($"   {constraint.NumberOfOwnedCandidates}:".PadRight(10));
                Console.Write(string.Join(", ", constraint.Candidates));
                Console.WriteLine();
            }
        }

        private void ShowPlayersPossibilities(IClueGame game, ClueSolver solver)
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
    }
}