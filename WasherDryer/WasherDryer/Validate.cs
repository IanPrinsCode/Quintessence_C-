using System;
using System.Linq;

namespace WasherDryer
{
    class Validate
    {
        public static Boolean IsValidRange(int param)
        {
            return Enumerable.Range(1, 100).Contains(param);
        }
    }
}
