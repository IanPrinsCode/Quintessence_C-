using System.Threading;

namespace WasherDryer
{
    class Dryer
    {
        private DishRack _dishRack;
        private int _speed;
        private int _dishes;

        public Dryer(DishRack dishRack, int dryerSpeed, int dishesAmount)
        {
            _dishRack = dishRack;
            _speed = dryerSpeed;
            _dishes = dishesAmount;
        }

        public void Start()
        {
            string currentDish;
            for (int i = 1; i <= _dishes; i++)
            {
                currentDish = "#" + i;

                //this number (in seconds) can be adjusted to simulate checking
                //if the dishrack has dishes
                int checkRackDuration = 2;
                //check if dishes are available
                while (_dishRack.IsEmpty())
                    Thread.Sleep(checkRackDuration*1000);

                //removing dish
                _dishRack._rackFlow.WaitOne();
                _dishRack.RemoveDish(currentDish);
                Display.RemoveDish(currentDish);
                _dishRack._rackFlow.Release();

                //Drying process
                Thread.Sleep(_speed);
                Display.FinishDrying(currentDish);
            }
            Display.AllDishesDried();
        }
    }
}
