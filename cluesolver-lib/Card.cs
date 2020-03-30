using System;

namespace cluesolver
{
    /// <summary>
    /// Represents a single card, with a category and title
    /// </summary>
    public struct Card : IEquatable<Card>
    {
        /// <summary>
        /// The category of the card
        /// </summary>
        /// <value>the category of this card</value>
        public string Category { get; }

        /// <summary>
        /// The title of the card
        /// </summary>
        /// <value>the title of this card</value>
        public string Title { get; }

        /// <summary>
        /// Creates a new <see cref="Card"/> value with a specified <see cref="Card.Category"/> and <see cref="Card.Title"/>
        /// </summary>
        /// <param name="category">the value to assign to <see cref="Card.Category"/></param>
        /// <param name="title">the value to assign to <see cref="Card.Title"/></param>
        public Card(string category, string title)
        {
            Category = category;
            Title = title;
        }

        public bool Equals(Card other) =>
            this == other;

        public override int GetHashCode() =>
            HashCode.Combine(Category, Title);

        public override bool Equals(object obj) =>
            obj is Card other &&
            this == other;

        public override string ToString() => Title;

        public static bool operator ==(Card lhs, Card rhs) =>
            lhs.Category.Equals(rhs.Category) &&
            lhs.Title.Equals(rhs.Title);

        public static bool operator !=(Card lhs, Card rhs) =>
            !lhs.Category.Equals(rhs.Category) ||
            !lhs.Title.Equals(rhs.Title);
    }
}
