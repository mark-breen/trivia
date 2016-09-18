using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace Trivia.Tests
{
    public class CharacterisationTests
    {
        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_NewGame_AddOnePlayer
        {
            private TestableGame _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_NewGame_AddTwoPlayers
        {
            private TestableGame _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
                _sut.Add("Jane");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_NewGame_AddThreePlayers
        {
            private TestableGame _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
                _sut.Add("Jane");
                _sut.Add("Fred");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_ThreePlayers_AllCorrectAnswers
        {
            private TestableGame _sut;

            private List<int> _diceRolls = new List<int>
            {
                2,
                4,
                6,
                1,
                3,
                5,
                3
            };

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
                _sut.Add("Jane");
                _sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    _sut.Roll(diceRolls.Current);
                    notAWinner = _sut.WasCorrectlyAnswered();
                    if (!diceRolls.MoveNext())
                    {
                        diceRolls = _diceRolls.GetEnumerator();
                    }
                }
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class GoldenMaster_ThreePlayers_CorrectAnswersOn4AndGreater
        {
            private TestableGame _sut;

            private List<int> _diceRolls = new List<int>
            {
                2,
                4,
                6,
                1,
                3,
                5,
                3
            };

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
                _sut.Add("Jane");
                _sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    var roll = diceRolls.Current;
                    _sut.Roll(roll);
                    if(roll >= 4)
                    {
                        notAWinner = _sut.WasCorrectlyAnswered();
                    }
                    else
                    {
                        notAWinner = _sut.WrongAnswer();
                    }
                    if (!diceRolls.MoveNext())
                    {
                        diceRolls = _diceRolls.GetEnumerator();
                    }
                }
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class GoldenMaster_ThreePlayers_CorrectAnswersOn3OrLess
        {
            private TestableGame _sut;

            private List<int> _diceRolls = new List<int>
            {
                2,
                4,
                6,
                1,
                3,
                5,
                3
            };

            [SetUp]
            public void SetUp()
            {
                _sut = new TestableGame();
                _sut.Add("Bob");
                _sut.Add("Jane");
                _sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    var roll = diceRolls.Current;
                    _sut.Roll(roll);
                    if (roll <= 3)
                    {
                        notAWinner = _sut.WasCorrectlyAnswered();
                    }
                    else
                    {
                        notAWinner = _sut.WrongAnswer();
                    }
                    if (!diceRolls.MoveNext())
                    {
                        diceRolls = _diceRolls.GetEnumerator();
                    }
                }
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_sut.GetMessages());
            }
        }
    }
}