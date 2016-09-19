using System;
using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        private readonly List<Player> _players = new List<Player>();

        private IEnumerator<Player> _playersEnumerator; 

        public int Count => _players.Count;

        public void Add(string playerName)
        {
            _players.Add(new Player(playerName));
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
            return _playersEnumerator.Current;
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