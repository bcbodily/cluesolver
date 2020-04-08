using System;

namespace cluesolver
{
    /// <summary>
    /// Data for item selected events
    /// </summary>
    /// <typeparam name="T">the type of item selected by the event</typeparam>
    public class ItemSelectedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="ItemSelectedEventArgs"/> with a specified item
        /// </summary>
        /// <param name="item">the item that was selected</param>
        public ItemSelectedEventArgs(T item)
        {
            Item = item;
        }

        /// <summary>
        /// The item that was selected
        /// </summary>
        /// <value>the item that was selected</value>
        public T Item { get; }
    }
}