using System;
using Xunit;

namespace cluesolver
{
    /// <summary>
    /// Unit tests for <see cref="ItemSelectedEventArgs"/>
    /// </summary>
    public class ItemSelectedEventArgsTest
    {
        /// <summary>
        /// Verifies <see cref="ItemSelectedEventArgs.ItemSelectedEventArgs(T)"/>, when the specified item is null, properly sets <see cref="ItemSelectedEventArgs.Item"/> to null
        /// </summary>
        [Fact]
        public void constructor_item_is_null_Item_is_null()
        {
            object item = null;
            Assert.Null(item);

            Assert.Null(new ItemSelectedEventArgs<object>(item).Item);
        }

        /// <summary>
        /// Verifies <see cref="ItemSelectedEventArgs.ItemSelectedEventArgs(T)"/>, when the specified item is not null, properly sets <see cref="ItemSelectedEventArgs.Item"/> to the item specified in the constructor
        /// </summary>
        [Fact]
        public void constructor_item_isNot_null_sets_Item()
        {
            object item = new Object();
            Assert.NotNull(item);

            var actual = new ItemSelectedEventArgs<object>(item).Item;
            var expected = item;

            Assert.Same(expected, actual);
        }
    }
}