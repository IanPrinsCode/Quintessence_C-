using System;

namespace Matrix
{
	class Program
	{
		static void Main(string[] args)
		{
			string input;
			int[,] matrix;

			Console.WriteLine("Enter numbers separated by spaces and hit enter to get their binary representations.");
			input = Console.ReadLine();

			while (!Test.IsValid(input))
			{
				input = Console.ReadLine();
			}

			matrix = Logic.OccupyMatrix(Logic.BuildMatrix(input));
			Logic.PrintMatrix(matrix);
		}
	}
}
