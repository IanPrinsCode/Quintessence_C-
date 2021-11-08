using System.Threading;

namespace WasherDryer
{
    class Washer
    {
        private DishRack _dishRack;
        private int _speed;
        private int _dishes;

        public Washer(DishRack dishRack, int washerSpeed, int dishesAmount)
        {
            _dishRack = dishRack;
            _speed = washerSpeed;
            _dishes = dishesAmount;
        }

        public void Start()
        {
            string currentDish;
            for (int i = 1; i <= _dishes; i++)
            {
                currentDish = "#" + i;

                //washing process
                Display.StartWash(currentDish);
                Thread.Sleep(_speed);

                //this number (in seconds) can be adjusted to simulate checking
                //if the dishrack has space for dishes to add
                int checkRackDuration = 2;
                //check if rack has space
                while (_dishRack.IsFull())
                    Thread.Sleep(checkRackDuration*1000);

                //adding dish
                _dishRack._rackFlow.WaitOne();
                Display.AddDish(currentDish);
                _dishRack.AddDish(currentDish);
                _dishRack._rackFlow.Release();
            }
            Display.AllDishesWashed();
        }
    }
}
