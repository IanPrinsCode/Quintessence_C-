using System;

namespace Matrix
{
    class Test
    {
		public static Boolean IsValid(string text)
		{
			int num;
			string[] elements = text.Split(' ');

			foreach (string element in elements)
			{
				try
				{
					num = Int32.Parse(element);
				}
				catch (FormatException)
				{
					Console.WriteLine("Invalid input. Retry.");
					return false;
				}

				if (num < 1 || num > 255)
				{
					Console.WriteLine("Invalid input. Retry.");
					return false;
				}
			}
			return true;
		}
	}
}
