using System.Collections.Generic;

namespace Trivia
{
    public class Places
    {
        private readonly List<Place> _places = new List<Place>
        {
            new Place(Categories.Pop),
            new Place(Categories.Science),
            new Place(Categories.Sports),
            new Place(Categories.Rock),
            new Place(Categories.Pop),
            new Place(Categories.Science),
            new Place(Categories.Sports),
            new Place(Categories.Rock),
            new Place(Categories.Pop),
            new Place(Categories.Science),
            new Place(Categories.Sports),
            new Place(Categories.Rock)
        };

        private IEnumerator<Place> _placesEnumerator;
    }
}