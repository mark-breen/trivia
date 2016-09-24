using System;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Trivia.Tests
{
    public class PlayerTests
    {
        [TestFixture]
        public class NameTests
        {
            [Test]
            public void NullName()
            {
                Assert.Throws<ArgumentException>(() => new Player(null, null));
            }

            [Test]
            public void EmptyName()
            {
                Assert.Throws<ArgumentException>(() => new Player(string.Empty, null));
            }

            [Test]
            public void WhitespaceName()
            {
                Assert.Throws<ArgumentException>(() => new Player(" ", null));
            }

            [Test]
            public void ValidName()
            {
                const string validName = "Some valid name";
                var sut = new Player(validName, new Place(1, Guid.Empty, "bob"));
                Assert.That(sut.Name, Is.EqualTo(validName));
            }
        }

        [Story]
        public class APlayerWithLessThan6CoinsHasNotWon
        {
            private Player _sut;
            private int _randomNumberOfCoinsLessThanSix;

            [Given]
            public void Given()
            {
                _sut = new Player("Some player", new Place(1, Guid.Empty, "bob"));
            }

            [When]
            public void When()
            {
                _randomNumberOfCoinsLessThanSix = new Random().Next(1, 5);
                for (var i = 0; i < _randomNumberOfCoinsLessThanSix; i++)
                {
                    _sut.WinsGoldCoin();
                }
            }

            [Then]
            public void ThenThePlayerHasNotWon()
            {
                Assert.That(_sut.HasWon(), Is.False);
            }

            [AndThen]
            public void AndThePlayerHasTheCorrectNumberOfCoins()
            {
                Assert.That(_sut.Purse, Is.EqualTo(_randomNumberOfCoinsLessThanSix));
            }

            [Test]
            public void RunTest()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class APlayerWith6CoinsHasWon
        {
            private Player _sut;

            [Given]
            public void Given()
            {
                _sut = new Player("Some player", new Place(1, Guid.Empty, "bob"));
            }

            [When]
            public void When()
            {
                for (var i = 0; i < 6; i++)
                {
                    _sut.WinsGoldCoin();
                }
            }

            [Then]
            public void ThenThePlayerHasNotWon()
            {
                Assert.That(_sut.HasWon(), Is.True);
            }

            [AndThen]
            public void AndThePlayerHasTheCorrectNumberOfCoins()
            {
                Assert.That(_sut.Purse, Is.EqualTo(6));
            }

            [Test]
            public void RunTest()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class APlayerCannotWinMoreThan6Coins
        {
            private Player _sut;

            [Given]
            public void Given()
            {
                _sut = new Player("Some player", new Place(1, Guid.Empty, "bob"));
            }

            [When]
            public void When()
            {
                for (var i = 0; i < new Random().Next(7, 100); i++)
                {
                    _sut.WinsGoldCoin();
                }
            }

            [Then]
            public void ThenThePlayerHasNotWon()
            {
                Assert.That(_sut.HasWon(), Is.True);
            }

            [AndThen]
            public void AndThePlayerHasTheCorrectNumberOfCoins()
            {
                Assert.That(_sut.Purse, Is.EqualTo(6));
            }

            [Test]
            public void RunTest()
            {
                this.BDDfy();
            }
        }
    }
}