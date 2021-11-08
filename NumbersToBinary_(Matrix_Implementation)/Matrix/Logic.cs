using System;
using System.Collections.Generic;

namespace Matrix
{
    class Logic
    {
		public static int[,] BuildMatrix(string input)
		{
			string[] stringList = input.Split(' ');
			int[,] matrix = new int[stringList.Length, 9];

			for (int i = 0; i < stringList.Length; i++)
			{
				matrix[i, 0] = Int32.Parse(stringList[i]);
			}
			return matrix;
		}

		public static void PrintMatrix(int[,] matrix)
		{
			for (int i = 0; i < matrix.GetLength(1); i++)
			{
				for (int j = 0; j < matrix.GetLength(0); j++)
				{
					Console.Write(matrix[j, i] + "\t");
				}
				Console.WriteLine();
			}
		}

		public static int[,] OccupyMatrix(int[,] matrix)
		{
			var indexBoxValues = new Dictionary<int, int>()
			{
				{1, 128 },
				{2, 64 },
				{3, 32 },
				{4, 16 },
				{5, 8 },
				{6, 4 },
				{7, 2 },
				{8, 1 }
			};

			for (int j = 0; j < matrix.GetLength(0); j++)
			{
				int target = matrix[j, 0];
				for (int i = 1; i < matrix.GetLength(1); i++)
				{
					if (target >= indexBoxValues[i])
					{
						matrix[j, i] = 1;
						target -= indexBoxValues[i];
					}
					else
						matrix[j, i] = 0;
				}
			}
			return matrix;
		}
	}
}
