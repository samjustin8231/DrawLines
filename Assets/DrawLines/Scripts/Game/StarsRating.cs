using UnityEngine;
using System.Collections;

public class StarsRating
{
		/// <summary>
		/// The minumum Coefficient to calculate Stars Number
		/// </summary>
		private static float minCoefficient = 2f;

		/// <summary>
		/// The maximum Coefficient to calculate Stars Number
		/// </summary>
		private static float maxCoefficient = 3f;


		/// <summary>
		/// Gets the awesome dialog stars rating.
		/// </summary>
		/// <returns>The Awesome Dialog Stars Rating (Stars Number).</returns>
		/// <param name="time">Time.</param>
		/// <param name="movements">Movements.</param>
		/// <param name="gridSize">Grid size.</param>
		public static AwesomeDialog.StarsNumber GetAwesomeDialogStarsRating (int time, int movements, int gridSize)
		{
				float sum = time + movements;

				if (sum <= gridSize * minCoefficient) {
						return AwesomeDialog.StarsNumber.THREE;
				} else if (sum > gridSize * minCoefficient && sum < gridSize * maxCoefficient) {
						return AwesomeDialog.StarsNumber.TWO;
				}
				return AwesomeDialog.StarsNumber.ONE;
		}

		/// <summary>
		/// Gets the level stars rating.
		/// </summary>
		/// <returns>The Level Stars Rating (Stars Number).</returns>
		/// <param name="time">Time.</param>
		/// <param name="movements">Movements.</param>
		/// <param name="gridSize">Grid size.</param>
		public static TableLevel.StarsNumber GetLevelStarsRating (int time, int movements, int gridSize)
		{
				float sum = time + movements;
		
				if (sum <= gridSize * minCoefficient) {
						return TableLevel.StarsNumber.THREE;
				} else if (sum > gridSize * minCoefficient && sum < gridSize * maxCoefficient) {
						return TableLevel.StarsNumber.TWO;
				}
				return TableLevel.StarsNumber.ONE;
		}
}