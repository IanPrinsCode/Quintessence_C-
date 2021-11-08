using System;
using System.Threading.Tasks;

namespace WasherDryer
{
    class Program
    {
        protected static void DoSimulation(Washer washer, Dryer dryer)
        {
            Task.Run(washer.Start);
            Task.Run(dryer.Start);

            //to keep app running until output finishes
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var reader = new InputReader();
            int dishesAmount, rackCapacity, washerSpeed, dryerSpeed;

            reader.GetInput(out dishesAmount, out rackCapacity, out washerSpeed, out dryerSpeed);

            var dishRack = new DishRack(rackCapacity);
            var washer = new Washer(dishRack, washerSpeed, dishesAmount);
            var dryer = new Dryer(dishRack, dryerSpeed, dishesAmount);

            DoSimulation(washer, dryer);
        }
    }
}
