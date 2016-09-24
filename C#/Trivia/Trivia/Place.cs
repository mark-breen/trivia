using System;

namespace Trivia
{
    public class Place
    {
        private readonly Guid _id;
        public int Index { get; }
        public string Category { get; }

        public Place(int index, Guid id, string category)
        {
            _id = id;
            Index = index;
            Category = category;
        }

        public override bool Equals(object obj)
        {
            var other = (Place) obj;
            return _id.Equals(other._id);
        }

        public override int GetHashCode()
        {
            return (_id.GetHashCode()*23) ^ (Category.GetHashCode()*17);
        }
    }
}