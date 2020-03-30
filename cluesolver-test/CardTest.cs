using System;
using Xunit;

namespace cluesolver
{
    public class CardTest
    {
        /// <summary>
        /// Verifies <see cref="Card.Card(string, string)"/> properly sets <see cref="Card.Category"/> to the category value specified
        /// </summary>
        [Fact]
        public void constructor_Category_is_category()
        {
            var category = "person";
            var title = "my title";

            var actual = new Card(category: category, title: title).Category;
            var expected = category;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Card(string, string)"/> properly sets <see cref="Card.Title"/> to the title value specified
        /// </summary>
        [Fact]
        public void constructor_Title_is_title()
        {
            var category = "person";
            var title = "my title";

            var actual = new Card(category: category, title: title).Title;
            var expected = title;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(object)"/>, when the other value is an <see cref="Object"/> that is not a <see cref="Card"/>, properly returns false
        /// </summary>
        [Fact]
        public void Equals_other_is_object_and_not_Card_returns_false() {
            Object other = new Object();
            var instance = new Card();

            Assert.False(other is Card);

            var actual = instance.Equals(other);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Equals_other_is_object_and_Card_and_not_equal_returns_false() {
            var title1 = "title1";
            var title2 = "title2";
            var category = "category";

            var instance = new Card(category: category, title: title1);
            var card2 = new Card(category: category, title: title2);

            Object other = card2;

            Assert.True(other is Card);
            Assert.NotEqual(instance, card2);

            var actual = instance.Equals(other);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the equality operator properly returns true when <see cref="Card.Category"/> and <see cref="Card.Title"/> are equal for both values
        /// </summary>
        [Fact]
        public void operator_equality_Category_and_Title_is_equal_returns_true()
        {
            var category = "person";
            var title = "title";

            var card = new Card(category: category, title: title);
            var other = new Card(category: category, title: title);

            // verify the categories and titles are equal
            Assert.Equal(card.Category, other.Category);
            Assert.Equal(card.Title, other.Title);

            var actual = card == other;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the equality operator properly returns false when <see cref="Card.Category"/> is not equal for both values
        /// </summary>
        [Fact]
        public void operator_equality_Category_isNot_equal_returns_false()
        {
            var category1 = "person";
            var category2 = "weapon";
            var title = "title";

            var card = new Card(category: category1, title: title);
            var other = new Card(category: category2, title: title);

            // verify the categories are not equal
            Assert.NotEqual(card.Category, other.Category);

            var actual = card == other;
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the equality operator properly returns false when <see cref="Card.Title"/> is not equal for both values
        /// </summary>
        [Fact]
        public void operator_equality_Title_isNot_equal_returns_false()
        {
            var category = "person";
            var title1 = "title1";
            var title2 = "title2";

            var card = new Card(category: category, title: title1);
            var other = new Card(category: category, title: title2);

            // verify the titles are not equal
            Assert.NotEqual(card.Title, other.Title);

            var actual = card == other;
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the inequality operator, when <see cref="Card.Category"/> and <see cref="Card.Title"/> are equal for both values, properly returns false
        /// </summary>
        [Fact]
        public void operator_inequality_Category_and_Title_is_equal_returns_false()
        {
            var category = "person";
            var title = "title";

            var card = new Card(category: category, title: title);
            var other = new Card(category: category, title: title);

            // verify the categories and titles are equal
            Assert.Equal(card.Category, other.Category);
            Assert.Equal(card.Title, other.Title);

            var actual = card != other;
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the inequality operator, when <see cref="Card.Category"/> is not equal for both values, properly returns true
        /// </summary>
        [Fact]
        public void operator_inequality_Category_isNot_equal_returns_true()
        {
            var category1 = "person";
            var category2 = "weapon";
            var title = "title";

            var card = new Card(category: category1, title: title);
            var other = new Card(category: category2, title: title);

            // verify the categories are not equal
            Assert.NotEqual(card.Category, other.Category);

            var actual = card != other;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the inequality operator, when <see cref="Card.Title"/> is not equal for both values, properly returns true
        /// </summary>
        [Fact]
        public void operator_inequality_Title_isNot_equal_returns_true()
        {
            var category = "person";
            var title1 = "title1";
            var title2 = "title2";

            var card = new Card(category: category, title: title1);
            var other = new Card(category: category, title: title2);

            // verify the titles are not equal
            Assert.NotEqual(card.Title, other.Title);

            var actual = card != other;
            var expected = true;

            Assert.Equal(expected, actual);
        }

    }
}
