using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        private readonly List<Player> _players = new List<Player>();

        private IEnumerator<Player> _playersEnumerator;
        private IGameOutput _gameOutput;

        public Players(IGameOutput gameOutput)
        {
            _gameOutput = gameOutput;
        }

        public int Count => _players.Count;

        public void Add(string playerName, Place startingPlace)
        {
            _players.Add(new Player(playerName, startingPlace));
            _gameOutput.OutputMessage(playerName + " was added");
            _gameOutput.OutputMessage("They are player number " + _players.Count);
        }

        public bool MinimumPlayerCountReached()
        {
            return Count > 1;
        }

        public Player CurrentPlayer()
        {
            if (_playersEnumerator == null)
            {
                InitialiseEnumerator();
            }
            return _playersEnumerator?.Current;
        }

        public void NextPlayer()
        {
            if (_playersEnumerator == null)
            {
                InitialiseEnumerator();
                return;
            }
            if (!_playersEnumerator.MoveNext())
            {
                InitialiseEnumerator();
            }
        }

        private void InitialiseEnumerator()
        {
            _playersEnumerator = _players.GetEnumerator();
            _playersEnumerator.MoveNext();
        }
    }
}