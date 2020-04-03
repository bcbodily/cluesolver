using System;
using System.Collections.Generic;

namespace cluesolver
{
    public struct Revelation
    {
        /// <summary>
        /// Creates a new <see cref="Revelation"/> with a specified player and card
        /// </summary>
        /// <param name="player">the player that made the revelation</param>
        /// <param name="card">the card that was revealed</param>
        public Revelation(string player, Card? card)
        {
            Player = player;
            Card = card;
        }

        /// <summary>
        /// The card that was revealed
        /// </summary>
        /// <value></value>
        public Card? Card { get; }

        /// <summary>
        /// The player that made the revelation
        /// </summary>
        /// <value></value>
        public string Player { get; }

    }
}