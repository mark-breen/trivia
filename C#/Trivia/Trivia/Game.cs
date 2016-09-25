namespace Trivia
{
    public class Game
    {
        private readonly IGameOutput _gameOutput;

        private readonly Players _players;
        private readonly Places _places;
        private readonly Questions _questions;
        
        // A player must answer the question represented by the category of the place.
        // Incorrect answers move players to the penalty box

        // When a player is in the penalty box, they must roll
        // an odd number to continue from their current place
        // i.e the place where the player answered incorrectly
        // Whilst in the penalty box, the player does not progress
        // so should probably not answer questions!
        private readonly bool[] _inPenaltyBox = new bool[6];

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
            : this(new ConsoleOutput())
        {
        }

        public Game(IGameOutput gameOutput)
        {
            _gameOutput = gameOutput;
            _players = new Players(_gameOutput);
            _places = new Places(_gameOutput);
            _questions = new Questions(_gameOutput);
        }

        public bool IsPlayable()
        {
            return _players.MinimumPlayerCountReached();
        }

        public bool Add(string playerName)
        {
            _players.Add(playerName, _places.StartingPlace());

            _inPenaltyBox[HowManyPlayers()] = false;

            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            OutputMessage(_players.CurrentPlayer().Name + " is the current player");
            OutputMessage("They have rolled a " + roll);

            _isGettingOutOfPenaltyBox = roll % 2 != 0;

            if (_inPenaltyBox[_currentPlayer] && !_isGettingOutOfPenaltyBox)
            {
                OutputMessage(_players.CurrentPlayer().Name + " is not getting out of the penalty box");
                return;
            }

            if (_inPenaltyBox[_currentPlayer] && _isGettingOutOfPenaltyBox)
            {
                OutputMessage(_players.CurrentPlayer().Name + " is getting out of the penalty box");
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
            OutputMessage(_players.CurrentPlayer().Name + " was sent to the penalty box");

            _players.CurrentPlayer().GoToPenaltyBox();

            _inPenaltyBox[_currentPlayer] = true;

            return ProcessSwitchingPlayers();
        }

        private void MovePlayerAndAskQuestion(int roll)
        {
            var currentPlayer = _players.CurrentPlayer();
            currentPlayer.CurrentPlace = _places.NextPlaceFor(
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
            _players.CurrentPlayer().WinsGoldCoin();
            OutputMessage(_players.CurrentPlayer().Name
                          + " now has "
                          + _players.CurrentPlayer().Purse
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
            return _players.CurrentPlayer().CurrentPlace.Category;
        }

        private void NextPlayer()
        {
            _players.NextPlayer();
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }


        private bool IsGameOngoing()
        {
            return !_players.CurrentPlayer().HasWon();
        }

        private void AskQuestion()
        {
            _questions.AskQuestionFor(_players.CurrentPlayer());
        }

        protected virtual void OutputMessage(string message)
        {
            _gameOutput.OutputMessage(message);
        }
    }
}
