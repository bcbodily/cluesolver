using System;
using Xunit;

namespace cluesolver
{
    public class CardTest
    {
        /// <summary>
        /// Verifies <see cref="Card.CompareTo(Card)"/>, when <see cref="Card.Category"/> for the comparing value follows <see cref="Card.Category"/> for the value being compared to, properly returns a value greater than 0
        /// </summary>
        /// <param name="category1">the category to assign to the first value</param>
        /// <param name="category2">the category to assign to the second value</param>
        [Theory]
        [InlineData("b", "a")]
        [InlineData("c", "b")]
        public void CompareTo_category_follows_returns_greater_than_zero(string category1, string category2)
        {
            var card1 = new Card(category: category1, title: "title");
            var card2 = new Card(category: category2, title: "title");

            Assert.True(category1.CompareTo(category2) > 0);

            var actual = card1.CompareTo(card2) > 0;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.CompareTo(Card)"/>, when <see cref="Card.Category"/> for the comparing value appears in the same position as the value being compared to, and <see cref="Card.Title"/> for the comparing value appears in the same position as the value being compared to, properly returns 0
        /// </summary>
        /// <param name="title">the title to assign to both values</param>
        [Theory]
        [InlineData("a")]
        [InlineData("b")]

        public void CompareTo_Category_is_same_Title_is_same_returns_zero(string title)
        {
            var category = "category";

            var card1 = new Card(category: category, title: title);
            var card2 = new Card(category: category, title: title);

            Assert.True(card1.Category.CompareTo(card2.Category) == 0);

            Assert.True(card1.Title.CompareTo(card2.Title) == 0);

            var actual = card1.CompareTo(card2) == 0;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.CompareTo(Card)"/>, when <see cref="Card.Category"/> for the comparing value appears in the same position as the value being compared to, and <see cref="Card.Title"/> for the comparing value follows the value being compared to, properly returns a value greater than 0
        /// </summary>
        /// <param name="title1">the title to assign to the first value</param>
        /// <param name="title2">the title to assign to the second value</param>
        [Theory]
        [InlineData("b", "a")]
        [InlineData("c", "b")]

        public void CompareTo_Category_is_same_Title_follows_returns_greater_than_zero(string title1, string title2)
        {
            var category = "category";

            var card1 = new Card(category: category, title: title1);
            var card2 = new Card(category: category, title: title2);

            Assert.True(card1.Category.CompareTo(card2.Category) == 0);

            Assert.True(card1.Title.CompareTo(card2.Title) > 0);

            var actual = card1.CompareTo(card2) > 0;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.CompareTo(Card)"/>, when <see cref="Card.Category"/> for the comparing value appears in the same position as the value being compared to, and <see cref="Card.Title"/> for the comparing value precedes the value being compared to, properly returns a value less than 0
        /// </summary>
        /// <param name="title1">the title to assign to the first value</param>
        /// <param name="title2">the title to assign to the second value</param>
        [Theory]
        [InlineData("a", "b")]
        [InlineData("b", "c")]

        public void CompareTo_Category_is_same_Title_precedes_returns_less_than_zero(string title1, string title2)
        {
            var category = "category";

            var card1 = new Card(category: category, title: title1);
            var card2 = new Card(category: category, title: title2);

            Assert.True(card1.Category.CompareTo(card2.Category) == 0);

            Assert.True(card1.Title.CompareTo(card2.Title) < 0);

            var actual = card1.CompareTo(card2) < 0;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.CompareTo(Card)"/>, when <see cref="Card.Category"/> for the comparing value precedes <see cref="Card.Category"/> for the value being compared to, properly returns a value less than 0
        /// </summary>
        /// <param name="category1">the category to assign to the first value</param>
        /// <param name="category2">the category to assign to the second value</param>
        [Theory]
        [InlineData("a", "b")]
        [InlineData("b", "c")]
        public void CompareTo_category_precedes_returns_less_than_zero(string category1, string category2)
        {
            var card1 = new Card(category: category1, title: "title");
            var card2 = new Card(category: category2, title: "title");

            Assert.True(category1.CompareTo(category2) < 0);

            var actual = card1.CompareTo(card2) < 0;
            var expected = true;

            Assert.Equal(expected, actual);
        }

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
        /// Verifies <see cref="Card.Equals(Card)"/>, when <see cref="Card.Category"/> and <see cref="Card.Title"/> for the other value are equal, properly returns true
        /// </summary>
        [Fact]
        public void Equals_other_is_card_and_equal_returns_true()
        {
            var title = "title";
            var category = "category";

            var instance = new Card(category: category, title: title);
            var other = new Card(category: category, title: title);

            Assert.Equal(other.Category, instance.Category);
            Assert.Equal(other.Title, instance.Title);

            var actual = instance.Equals(other);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(Card)"/>, when <see cref="Card.Category"/> for the other value is not equal, properly returns false
        /// </summary>
        [Fact]
        public void Equals_other_is_card_Category_isNot_equal_returns_false()
        {
            var title = "title";
            var category1 = "category1";
            var category2 = "category2";

            var instance = new Card(category: category1, title: title);
            var other = new Card(category: category2, title: title);

            Assert.NotEqual(other.Category, instance.Category);

            var actual = instance.Equals(other);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(Card)"/>, when <see cref="Card.Title"/> for the other value is not equal, properly returns false
        /// </summary>
        [Fact]
        public void Equals_other_is_card_Title_isNot_equal_returns_false()
        {
            var title1 = "title1";
            var title2 = "title2";
            var category = "category";

            var instance = new Card(category: category, title: title1);
            var other = new Card(category: category, title: title2);

            Assert.NotEqual(other.Title, instance.Title);

            var actual = instance.Equals(other);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(object)"/>, when the other value is an <see cref="object"/> that is not a <see cref="Card"/>, properly returns false
        /// </summary>
        [Fact]
        public void Equals_other_is_object_and_not_Card_returns_false()
        {
            Object other = new Object();
            var instance = new Card();

            Assert.False(other is Card);

            var actual = instance.Equals(other);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(object)"/> when the other value is an <see cref="object"/> that is a <see cref="Card"/> and is equal, properly returns true
        /// </summary>
        [Fact]
        public void Equals_other_is_object_and_Card_and_is_equal_returns_true()
        {
            var title = "title1";
            var category = "category";

            var instance = new Card(category: category, title: title);
            var card2 = new Card(category: category, title: title);

            Object other = card2;

            Assert.True(other is Card);
            Assert.Equal(instance, card2);

            var actual = instance.Equals(other);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Card.Equals(object)"/> when the other value is an <see cref="object"/> that is a <see cref="Card"/> and is not equal, properly returns false
        /// </summary>
        [Fact]
        public void Equals_other_is_object_and_Card_and_not_equal_returns_false()
        {
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
        /// Verifies <see cref="Card.GetHashCode"/> properly returns a consistent hash code for equal <see cref="Card"/> values
        /// </summary>
        /// <param name="category">the value to assign to <see cref="Card.Category"/> for each <see cref="Card"/> value</param>
        /// <param name="title">the value to assign to <see cref="Card.Title"/> for each <see cref="Card"/> value</param>
        [Theory]
        [InlineData("cat1", "title1")]
        [InlineData("cat2", "title1")]
        [InlineData("cat1", "title2")]
        [InlineData("cat2", "title2")]
        public void GetHashCode_returns_same_code_for_same_values(string category, string title)
        {
            var card1 = new Card(category: category, title: title);
            var card2 = new Card(category: category, title: title);

            Assert.Equal(card2, card1);

            var actual = card2.GetHashCode();
            var expected = card1.GetHashCode();

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
        /// Verifies the equality operator, when the lhs argument is default and the rhs argument is default, properly returns true
        /// </summary>
        [Fact]
        public void operator_equality_lhs_is_default_rhs_is_default_returns_true()
        {
            Card lhs = new Card();
            Card rhs = new Card();

            var actual = lhs == rhs;
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the equality operator, when the lhs argument is default and the rhs argument is not, properly returns false
        /// </summary>
        [Fact]
        public void operator_equality_lhs_is_default_rhs_isNot_default_returns_false()
        {
            Card lhs = new Card();
            Card rhs = new Card("category", "title");

            var actual = lhs == rhs;
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the equality operator, when the lhs argument is not default and the lhs argument is default, properly returns false
        /// </summary>
        [Fact]
        public void operator_equality_lhs_isNot_default_rhs_is_default_returns_false()
        {
            Card lhs = new Card("category", "title");
            Card rhs = new Card();

            var actual = lhs == rhs;
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

        /// <summary>
        /// Verifies <see cref="Card.ToString"/> properly returns a string in the following format: <code>Category:Title</code>
        /// </summary>
        [Fact]
        public void ToString_returns_Title()
        {
            var category = "category";
            var title = "title";

            var actual = new Card(category: category, title: title).ToString();
            var expected = $"{category}:{title}";

            Assert.Equal(expected, actual);
        }
    }
}
