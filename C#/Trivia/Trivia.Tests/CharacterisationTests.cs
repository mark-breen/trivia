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
            private TestOutput _gameOutput;

            [SetUp]
            public void SetUp()
            {
                _gameOutput = new TestOutput();
                new Game(_gameOutput).Add("Bob");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_gameOutput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_NewGame_AddTwoPlayers
        {
            private TestOutput _testOuput;

            [SetUp]
            public void SetUp()
            {
                _testOuput = new TestOutput();
                var sut = new Game(_testOuput);
                sut.Add("Bob");
                sut.Add("Jane");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_testOuput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_NewGame_AddThreePlayers
        {
            private TestOutput _testOutput;

            [SetUp]
            public void SetUp()
            {
                _testOutput = new TestOutput();
                var sut = new Game(_testOutput);
                sut.Add("Bob");
                sut.Add("Jane");
                sut.Add("Fred");
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_testOutput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class GoldenMaster_NewGame_TwoPlayers
        {
            private TestOutput _testOutput;

            [SetUp]
            public void SetUp()
            {
                _testOutput = new TestOutput();
                var sut = new Game(_testOutput);
                sut.Add("Bob");
                sut.Add("Jane");

                // bob
                sut.Roll(1);
                sut.WrongAnswer();

                // jane
                sut.Roll(2);
                sut.WasCorrectlyAnswered();

                // bob
                sut.Roll(2);
                sut.WasCorrectlyAnswered();

                // jane
                sut.Roll(2);
                sut.WasCorrectlyAnswered();

                // bob
                sut.Roll(1);
                sut.WasCorrectlyAnswered();

                // jane
                sut.Roll(1);
                sut.WasCorrectlyAnswered();

                // bob
                sut.Roll(1);
                sut.WasCorrectlyAnswered();
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_testOutput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof (DiffReporter))]
        public class GoldenMaster_ThreePlayers_AllCorrectAnswers
        {
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

            private TestOutput _testOutput;

            [SetUp]
            public void SetUp()
            {
                _testOutput = new TestOutput();
                var sut = new Game(_testOutput);
                sut.Add("Bob");
                sut.Add("Jane");
                sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    sut.Roll(diceRolls.Current);
                    notAWinner = sut.WasCorrectlyAnswered();
                    if (!diceRolls.MoveNext())
                    {
                        diceRolls = _diceRolls.GetEnumerator();
                    }
                }
            }

            [Test]
            public void GetSnapshot()
            {
                Approvals.Verify(_testOutput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class GoldenMaster_ThreePlayers_CorrectAnswersOn4AndGreater
        {
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

            private TestOutput _testOutput;

            [SetUp]
            public void SetUp()
            {
                _testOutput = new TestOutput();
                var sut = new Game(_testOutput);
                sut.Add("Bob");
                sut.Add("Jane");
                sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    var roll = diceRolls.Current;
                    sut.Roll(roll);
                    if(roll >= 4)
                    {
                        notAWinner = sut.WasCorrectlyAnswered();
                    }
                    else
                    {
                        notAWinner = sut.WrongAnswer();
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
                Approvals.Verify(_testOutput.GetMessages());
            }
        }

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class GoldenMaster_ThreePlayers_CorrectAnswersOn3OrLess
        {
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

            private TestOutput _testOutput;

            [SetUp]
            public void SetUp()
            {
                _testOutput = new TestOutput();
                var sut = new Game(_testOutput);
                sut.Add("Bob");
                sut.Add("Jane");
                sut.Add("Fred");

                var diceRolls = _diceRolls.GetEnumerator();
                diceRolls.MoveNext();
                var notAWinner = true;
                while (notAWinner)
                {
                    var roll = diceRolls.Current;
                    sut.Roll(roll);
                    if (roll <= 3)
                    {
                        notAWinner = sut.WasCorrectlyAnswered();
                    }
                    else
                    {
                        notAWinner = sut.WrongAnswer();
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
                Approvals.Verify(_testOutput.GetMessages());
            }
        }
    }
}