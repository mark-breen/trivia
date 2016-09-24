using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Trivia
{
    public class Places
    {
        private readonly IGameOutput _gameOutput;

        private readonly List<Place> _places = new List<Place>
        {
            new Place(0, Guid.NewGuid(), Categories.Pop),
            new Place(1, Guid.NewGuid(), Categories.Science),
            new Place(2, Guid.NewGuid(), Categories.Sports),
            new Place(3, Guid.NewGuid(), Categories.Rock),
            new Place(4, Guid.NewGuid(), Categories.Pop),
            new Place(5, Guid.NewGuid(), Categories.Science),
            new Place(6, Guid.NewGuid(), Categories.Sports),
            new Place(7, Guid.NewGuid(), Categories.Rock),
            new Place(8, Guid.NewGuid(), Categories.Pop),
            new Place(9, Guid.NewGuid(), Categories.Science),
            new Place(10, Guid.NewGuid(), Categories.Sports),
            new Place(11, Guid.NewGuid(), Categories.Rock)
        };

        public Places(IGameOutput gameOutput)
        {
            _gameOutput = gameOutput;
        }

        public Place StartingPlace()
        {
            return _places[0];
        }

        public Place NextPlaceFor(Place currentPlace, int diceRoll)
        {
            var indexOfCurrentPlace = _places.FindIndex(x => x.Equals(currentPlace));
            var newIndex = indexOfCurrentPlace + diceRoll;
            if (newIndex > 11)
            {
                newIndex = newIndex - 12;
            }
            return _places[newIndex];
        }
    }
}