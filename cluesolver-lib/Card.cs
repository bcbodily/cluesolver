using System;

namespace cluesolver
{
    /// <summary>
    /// Represents a single card, with a category and title
    /// </summary>
    readonly public struct Card : IEquatable<Card>, IComparable<Card>
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

        /// <summary>
        /// Returns whether another <see cref="Card"/> is equal to this value
        /// </summary>
        /// <param name="other">another <see cref="Card"/> to compare to this value</param>
        /// <returns>true if the other value is equal to this value; otherwise, false</returns>
        public bool Equals(Card other) =>
            this == other;

        /// <summary>
        /// Returns a hash code for this value
        /// </summary>
        /// <returns>a hash code value consistent for this value</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Category, Title);

        /// <summary>
        /// Returns whether another <see cref="object"/> is a <see cref="Card"/> equal to this value
        /// </summary>
        /// <param name="obj">an object to compare to this value</param>
        /// <returns>true if the object is a <see cref="Card"/> equal to this value; otherwise, false</returns>
        public override bool Equals(object obj) =>
            obj is Card other &&
            this == other;

        /// <summary>
        /// Returns a string representation of this value
        /// </summary>
        /// <returns>a string in the following format: <code>Category:Title</code></returns>
        public override string ToString() => $"{Title}";

        /// <summary>
        /// Compares this value with a specified <see cref="Card"/> and indicates whether this value preceds, follows, or appears in the same position in the sort order as ths specified <see cref="Card"/>
        /// </summary>
        /// <param name="other">the <see cref="Card"/> to compare with this value</param>
        /// <returns>A value less than zero if this value precedes the specified value, or a value greater than zero if this value follows the specified value; otherwise, zero</returns>
        public int CompareTo(Card other)
        {
            var catCompare = Category.CompareTo(other.Category);
            if (catCompare != 0) return catCompare;

            // else
            return Title.CompareTo(other.Title);
        }

        public static bool operator ==(Card lhs, Card rhs) =>
            lhs.Category.Equals(rhs.Category) &&
            lhs.Title.Equals(rhs.Title);

        public static bool operator !=(Card lhs, Card rhs) =>
            !lhs.Category.Equals(rhs.Category) ||
            !lhs.Title.Equals(rhs.Title);
    }
}
