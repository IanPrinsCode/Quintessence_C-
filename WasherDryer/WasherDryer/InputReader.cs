using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WasherDryer
{
    class InputReader
    {
        private string _input;
        private Dictionary<int, int> outVariables = new Dictionary<int, int>()
            {
                { 0,0 }, //dishesAmount 
                { 1,0 }, //rackCapacity
                { 2,0 }, //washerSpeed
                { 3,0 } //dryerSpeed
            };

        public InputReader()
        {
            _input = "";
        }

        private Boolean IsValidInput()
        {
            var pattern = new Regex(@"\[(.*?)\]");
            for (int i = 0; i < 4; i++)
            {
                if (pattern.IsMatch(_input) 
                    && int.TryParse(pattern.Match(_input).Value.Trim('[').Trim(']'), out int value) 
                    && Validate.IsValidRange(value))
                {
                    outVariables[i] = value;
                    var currentReplace = new Regex(Regex.Escape("["+value+"]"));
                    _input = currentReplace.Replace(_input, String.Empty, 1);
                }
                else
                {
                    Display.InputError();
                    return false;
                }
            }
            return true;
        }

        public void GetInput(out int dishesAmount, out int rackCapacity, out int washerSpeed, out int dryerSpeed)
        {
            do
            {
                Display.InputMessage();
                _input = Console.ReadLine();
                Console.Clear();
            } while (!IsValidInput());
            dishesAmount = outVariables[0];
            rackCapacity = outVariables[1];
            washerSpeed = outVariables[2]*1000;
            dryerSpeed = outVariables[3]*1000;
        }
    }
}
