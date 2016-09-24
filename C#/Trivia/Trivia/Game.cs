using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Players _newPlayers = new Players();

        private readonly Places _newPlaces = new Places();

        // A player must answer the question represented by the category of the place.
        // Incorrect answers move players to the penalty box

        // When a player is in the penalty box, they must roll
        // an odd number to continue from their current place
        // i.e the place where the player answered incorrectly
        // Whilst in the penalty box, the player does not progress
        // so should probably not answer questions!
        private readonly bool[] _inPenaltyBox = new bool[6];

        // Needs to be an encapsulated collection of questions
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
            _newPlayers.Add(playerName, _newPlaces.StartingPlace());

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

            _newPlayers.CurrentPlayer().GoToPenaltyBox();

            _inPenaltyBox[_currentPlayer] = true;

            return ProcessSwitchingPlayers();
        }

        private void MovePlayerAndAskQuestion(int roll)
        {
            var currentPlayer = _newPlayers.CurrentPlayer();
            currentPlayer.CurrentPlace = _newPlaces.NextPlaceFor(
                currentPlayer.CurrentPlace, roll);

            OutputMessage(currentPlayer.Name
                          + "'s new location is "
                          + currentPlayer.CurrentPlace.Index);
            OutputMessage("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void ProcessCorrectAnswer()
        {
            OutputMessage("Answer was correct!!!!");
            _newPlayers.CurrentPlayer().WinsGoldCoin();
            OutputMessage(_newPlayers.CurrentPlayer().Name
                          + " now has "
                          + _newPlayers.CurrentPlayer().Purse
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
            return _newPlayers.CurrentPlayer().CurrentPlace.Category;
        }

        private void NextPlayer()
        {
            _newPlayers.NextPlayer();
            _currentPlayer++;
            if (_currentPlayer == _newPlayers.Count) _currentPlayer = 0;
        }


        private bool IsGameOngoing()
        {
            return !_newPlayers.CurrentPlayer().HasWon();
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
