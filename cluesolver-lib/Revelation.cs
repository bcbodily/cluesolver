using System;

namespace cluesolver
{
    /// <summary>
    /// Represents a revelation of a card by a player
    /// </summary>
    readonly public struct Revelation : IEquatable<Revelation>
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
        /// <value>the card that was revealed</value>
        public Card? Card { get; }

        /// <summary>
        /// The player that made the revelation
        /// </summary>
        /// <value>the player that made the revelation</value>
        public string Player { get; }

        /// <summary>
        /// Returns whether another <see cref="Revelation"/> is equal to this value
        /// </summary>
        /// <param name="other">another <see cref="Revelation"/> to compare to this value</param>
        /// <returns>this if the other value is equal to this value; otherwise, false</returns>
        public bool Equals(Revelation other) =>
            this == other;

        /// <summary>
        /// Returns whether another <see cref="object"/> is a <see cref="Revelation"/> equal to this value
        /// </summary>
        /// <param name="obj">an object to compare to this value</param>
        /// <returns>true if the object is a <see cref="Revelation"/> equal to this value; otherwise, false</returns>
        public override bool Equals(object obj) =>
            obj is Revelation other &&
            this == other;

        /// <summary>
        /// Returns a hash code for this value
        /// </summary>
        /// <returns>a hash code value consistent for this value</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Card, Player);

        public static bool operator ==(Revelation lhs, Revelation rhs) =>
            lhs.Card.Equals(rhs.Card) &&
            lhs.Player.Equals(rhs.Player);

        public static bool operator !=(Revelation lhs, Revelation rhs) =>
            !lhs.Card.Equals(rhs.Card) ||
            !lhs.Player.Equals(rhs.Player);
    }
}