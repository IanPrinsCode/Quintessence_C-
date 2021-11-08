using System.Collections.Generic;
using System.Threading;

namespace WasherDryer
{
    class DishRack
    {
        public Semaphore _rackFlow;
        public int _capacity;
        public List<string> _rackContents;

        public DishRack(int rackCapacity)
        {
            _rackFlow = new Semaphore(1, 1);
            _capacity = rackCapacity;
            _rackContents = new List<string>();
        }

        public bool IsEmpty()
        {
            return _rackContents.Count == 0;
        }

        public bool IsFull()
        {
            return _rackContents.Count == _capacity;
        }

        public void AddDish(string dish)
        {
            _rackContents.Add(dish);
        }

        public void RemoveDish(string dish)
        {
            _rackContents.Remove(dish);
        }
    }
}
