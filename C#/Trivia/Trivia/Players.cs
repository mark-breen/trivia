using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        private readonly List<Player> _players = new List<Player>(); 

        public int Count => _players.Count;

        public void Add(string playerName)
        {
            _players.Add(new Player(playerName));
        }

        public bool MinimumPlayerCountReached()
        {
            return Count > 1;
        }
    }
}