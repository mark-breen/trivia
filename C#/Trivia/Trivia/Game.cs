using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Players _newPlayers = new Players();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return _newPlayers.MinimumPlayerCountReached();
        }

        public bool Add(string playerName)
        {
            _newPlayers.Add(playerName);

            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            OutputMessage(playerName + " was added");
            OutputMessage("They are player number " + HowManyPlayers());
            return true;
        }

        public int HowManyPlayers()
        {
            return _newPlayers.Count;
        }

        public void Roll(int roll)
        {
            OutputMessage(_newPlayers.CurrentPlayer().Name + " is the current player");
            OutputMessage("They have rolled a " + roll);

            _isGettingOutOfPenaltyBox = roll % 2 != 0;

            if (_inPenaltyBox[_currentPlayer] && !_isGettingOutOfPenaltyBox)
            {
                OutputMessage(_newPlayers.CurrentPlayer().Name + " is not getting out of the penalty box");
                return;
            }

            if (_inPenaltyBox[_currentPlayer] && _isGettingOutOfPenaltyBox)
            {
                OutputMessage(_newPlayers.CurrentPlayer().Name + " is getting out of the penalty box");
            }
            MovePlayerAndAskQuestion(roll);
        }


        public bool WasCorrectlyAnswered()
        {
            if (!_inPenaltyBox[_currentPlayer] || _isGettingOutOfPenaltyBox)
            {
                ProcessCorrectAnswer();
            }
            return ProcessSwitchingPlayers();
        }

        public bool WrongAnswer()
        {
            OutputMessage("Question was incorrectly answered");
            OutputMessage(_newPlayers.CurrentPlayer().Name + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            return ProcessSwitchingPlayers();
        }

        private void MovePlayerAndAskQuestion(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

            OutputMessage(_newPlayers.CurrentPlayer().Name
                          + "'s new location is "
                          + _places[_currentPlayer]);
            OutputMessage("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void ProcessCorrectAnswer()
        {
            OutputMessage("Answer was correct!!!!");
            _purses[_currentPlayer]++;
            OutputMessage(_newPlayers.CurrentPlayer().Name
                          + " now has "
                          + _purses[_currentPlayer]
                          + " Gold Coins.");
        }

        private bool ProcessSwitchingPlayers()
        {
            var isGameOngoing = IsGameOngoing();

            NextPlayer();

            return isGameOngoing;
        }

        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        private void NextPlayer()
        {
            _newPlayers.NextPlayer();
            _currentPlayer++;
            if (_currentPlayer == _newPlayers.Count) _currentPlayer = 0;
        }


        private bool IsGameOngoing()
        {
            return _purses[_currentPlayer] != 6;
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                OutputMessage(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                OutputMessage(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                OutputMessage(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                OutputMessage(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        // Testing seam
        protected virtual void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

}
