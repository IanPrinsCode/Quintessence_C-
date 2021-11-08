using System;

namespace WasherDryer
{
    class Display
    {
        public static void InputMessage()
        {
            Console.WriteLine("Please enter config values in the following format.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[no. of dishes][dishrack capacity][washer speed (seconds)][dryer speed (seconds)]\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Input: ");
        }

        public static void InputError()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid input!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void StartWash(string dish)
        {
            Console.WriteLine("---Washer is washing dish " + dish);
        }

        public static void AddDish(string dish)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---Washer added dish " + dish + " to rack.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void RemoveDish(string dish)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---Dryer removed dish " + dish + " from rack.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void FinishDrying(string dish)
        {
            Console.WriteLine("---Dryer is done with dish " + dish);
        }

        public static void AllDishesWashed()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("WASHER FINISHED ALL DISHES");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void AllDishesDried()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("DRYER FINISHED ALL DISHES");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
