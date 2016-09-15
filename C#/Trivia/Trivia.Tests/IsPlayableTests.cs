using System.Security.Policy;
using NUnit.Framework;
using TestStack.BDDfy;
using UglyTrivia;

namespace Trivia.Tests
{
    public class IsPlayableTests
    {
        [Story]
        public class NoPlayers_False
        {
            private Game _sut;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                // No players added
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(0));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.False);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class OnePlayer_False
        {
            private Game _sut;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                _sut.add("one player");
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(1));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.False);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class TwoPlayers_True
        {
            private Game _sut;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                _sut.add("one player");
                _sut.add("two players");
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(2));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.True);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class ThreePlayers_True
        {
            private Game _sut;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                _sut.add("one player");
                _sut.add("two players");
                _sut.add("three players");
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(3));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.True);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class FourPlayers_True
        {
            private Game _sut;
            private int _noOfPlayers;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
                _noOfPlayers = 4;
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                for (int i = 1; i <= _noOfPlayers; i++)
                {
                    _sut.add(i.ToString());
                }
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(_noOfPlayers));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.True);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        [Story]
        public class FivePlayers_True
        {
            private Game _sut;
            private int _noOfPlayers;

            [Given]
            public void GivenAGame()
            {
                _sut = new Game();
                _noOfPlayers = 5;
            }

            [When]
            public void WhenThePlayersAreAdded()
            {
                for (int i = 1; i <= _noOfPlayers; i++)
                {
                    _sut.add(i.ToString());
                }
            }

            [Then]
            public void ThenTheCorrectNumberOfPlayersAreRecorded()
            {
                Assert.That(_sut.howManyPlayers(), Is.EqualTo(_noOfPlayers));
            }

            [AndThen]
            public void ThenTheGameIsInTheCorrectPlayableState()
            {
                Assert.That(_sut.isPlayable, Is.True);
            }

            [Test]
            public void RunTests()
            {
                this.BDDfy();
            }
        }

        // TODO Fix bug that stops 6th player being added - add() should be:
        //           places[howManyPlayers() - 1] = 0;
        //           purses[howManyPlayers() - 1] = 0;
        //           inPenaltyBox[howManyPlayers() - 1] = false;
        //[Story]
        //public class SixPlayers_True
        //{
        //    private Game _sut;
        //    private int _noOfPlayers;

        //    [Given]
        //    public void GivenAGame()
        //    {
        //        _sut = new Game();
        //        _noOfPlayers = 6;
        //    }

        //    [When]
        //    public void WhenThePlayersAreAdded()
        //    {
        //        for (int i = 1; i <= _noOfPlayers; i++)
        //        {
        //            _sut.add(i.ToString());
        //        }
        //    }

        //    [Then]
        //    public void ThenTheCorrectNumberOfPlayersAreRecorded()
        //    {
        //        Assert.That(_sut.howManyPlayers(), Is.EqualTo(_noOfPlayers));
        //    }

        //    [AndThen]
        //    public void ThenTheGameIsInTheCorrectPlayableState()
        //    {
        //        Assert.That(_sut.isPlayable, Is.True);
        //    }

        //    [Test]
        //    public void RunTests()
        //    {
        //        this.BDDfy();
        //    }
        //}
    }
}