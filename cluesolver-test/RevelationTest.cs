using System;
using Xunit;

namespace cluesolver
{
    /// <summary>
    /// Unit tests for <see cref="Revelation"/>
    /// </summary>
    public class RevelationTest
    {
        /// <summary>
        /// Verifies <see cref="Revelation.Revelation(string, Card?)"/>, when the card is null, properly sets <see cref="Revelation.Card"/> to null
        /// </summary>
        [Fact]
        public void constructor_card_is_null_Card_is_null()
        {
            string player = null;
            Card? card = null;

            Assert.Null(card);

            var actual = new Revelation(player, card).Card;

            Assert.Null(actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Revelation(string, Card?)"/>, when the card is not null, properly sets <see cref="Revelation.Card"/> to the specified value
        /// </summary>
        [Fact]
        public void constructor_card_isNot_null_Card_is_card()
        {
            string player = "a player";
            Card? card = new Card("cat", "title");

            Assert.NotNull(card);

            var actual = new Revelation(player, card).Card;
            var expected = card;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Revelation(string, Card?)"/>, when the player is null, properly sets <see cref="Revelation.Player"/> to null
        /// </summary>
        [Fact]
        public void constructor_player_is_null_Player_is_null()
        {
            string player = null;
            Card? card = null;

            Assert.Null(player);

            var actual = new Revelation(player, card).Player;

            Assert.Null(actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Revelation(string, Card?)"/>, when the player is not null, properly sets <see cref="Revelation.Player"/> to the specified value
        /// </summary>
        [Fact]
        public void constructor_player_isNot_null_Player_is_player()
        {
            string player = "a player";
            Card? card = null;

            Assert.NotNull(player);

            var actual = new Revelation(player, card).Player;
            var expected = player;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(Revelation)"/>, when <see cref="Revelation.Card"/> and <see cref="Revelation.Player"/> are equal for the other value, properly returns true
        /// </summary>
        [Fact]
        public void Equals_Revelation_other_Card_and_Player_are_equals_returns_true()
        {
            var card = new Card("cat", "title");
            var player = "player";

            var Revelation1 = new Revelation(player, card);
            var Revelation2 = new Revelation(player, card);

            Assert.Equal(Revelation1.Card, Revelation2.Card);
            Assert.Equal(Revelation1.Player, Revelation2.Player);

            var actual = Revelation1.Equals(Revelation2);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(Revelation)"/>, when <see cref="Revelation.Card"/> is not equal for the other value, properly returns false
        /// </summary>
        [Fact]
        public void Equals_Revelation_other_Card_is_different_returns_false()
        {
            var card1 = new Card("cat", "title");
            var card2 = new Card("cat2", "title");
            var player = "player";

            var Revelation1 = new Revelation(player, card1);
            var Revelation2 = new Revelation(player, card2);

            Assert.NotEqual(Revelation1.Card, Revelation2.Card);

            var actual = Revelation1.Equals(Revelation2);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(Revelation)"/>, when <see cref="Revelation.Player"/> is not equal for the other value, properly returns false
        /// </summary>
        [Fact]
        public void Equals_Revelation_other_Player_is_different_returns_false()
        {
            var card = new Card("cat", "title");
            var player1 = "player1";
            var player2 = "player2";

            var Revelation1 = new Revelation(player1, card);
            var Revelation2 = new Revelation(player2, card);

            Assert.NotEqual(Revelation1.Player, Revelation2.Player);

            var actual = Revelation1.Equals(Revelation2);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(object)"/> when the other object is null, properly return false
        /// </summary>
        [Fact]
        public void Equals_Object_other_is_null_returns_false()
        {
            var revelation = new Revelation("player", new Card());
            object obj = null;

            Assert.Null(obj);

            var actual = revelation.Equals(obj);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(object)"/>, when the other object is a <see cref="Revelation"/> for which <see cref="Revelation.Card"/> is not equal, properly return false
        /// </summary>
        [Fact]
        public void Equals_Object_other_is_Revelation_Card_isNot_equal_returns_false()
        {
            var card1 = new Card("cat", "title");
            Card card2 = new Card();
            var player = "player";

            var revelation = new Revelation(player, card1);
            object obj = new Revelation(player, card2);

            Assert.True(obj is Revelation);
            Assert.NotEqual(revelation.Card, ((Revelation)(obj)).Card);

            var actual = revelation.Equals(obj);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(object)"/>, when the other object is a <see cref="Revelation"/> for which <see cref="Revelation.Player"/> is not equal, properly return false
        /// </summary>
        [Fact]
        public void Equals_Object_other_is_Revelation_Player_isNot_equal_returns_false()
        {
            Card card = new Card();
            var player1 = "player1";
            var player2 = "player2";

            var revelation = new Revelation(player1, card);
            object obj = new Revelation(player2, card);

            Assert.True(obj is Revelation);
            Assert.NotEqual(revelation.Player, ((Revelation)(obj)).Player);

            var actual = revelation.Equals(obj);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies <see cref="Revelation.Equals(object)"/>, when the other object is not a <see cref="Revelation"/>, properly returns false
        /// </summary>
        [Fact]
        public void Equals_Object_other_is_not_Revelation_returns_false()
        {
            var revelation = new Revelation("player", new Card());
            object obj = "string";

            Assert.False(obj is Revelation);

            var actual = revelation.Equals(obj);
            var expected = false;

            Assert.Equal(expected, actual);
        }
    }
}