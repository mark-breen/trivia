using System;

namespace Trivia
{
    public class Player
    {
        public string Name { get; }

        public int Purse { get; private set; }

        public bool IsInPenaltyBox { get; private set; }

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A player must have a name", nameof(name));
            }
            Name = name;
        }

        public void GoToPenaltyBox()
        {
            IsInPenaltyBox = true;
        }

        public void EscapeFromPenaltyBox(int diceRoll)
        {
            IsInPenaltyBox = false;
        }

        public void WinsGoldCoin()
        {
            if(Purse < 6)
            {
                Purse++;
            }
        }

        public bool HasWon()
        {
            return Purse == 6;
        }
    }
}