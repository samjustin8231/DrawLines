using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class GridCell : MonoBehaviour
{       
		/// <summary>
		/// The color of the top background.
		/// </summary>
		public Color topBackgroundColor;

		/// <summary>
		/// Whether the GridCell is used.
		/// </summary>
		public bool currentlyUsed;

		/// <summary>
		/// Whether the GridCell is empty.
		/// </summary>
		public bool isEmpty = true;

		/// <summary>
		/// The index of the GridCell in the Grid.
		/// </summary>
		public int index;

		/// <summary>
		/// The index of the traget(partner).
		/// </summary>
		public int tragetIndex = -1;

		/// <summary>
		/// The index of the grid line.
		/// </summary>
		public int gridLineIndex = -1;

		/// <summary>
		/// The index of the element(dots) pair.
		/// </summary>
		public int elementPairIndex = -1;

		/// <summary>
		/// The surrounded adjacents of the GridCell.
		/// (Contains the indexes of the adjacents (neighbours))
		/// </summary>
		public List<int> adjacents = new List<int> ();

		/// <summary>
		/// Define the adjacents of the GridCell.
		/// </summary>
		/// <param name="i">The index of the Row such that 0=< i < NumberOfRows </para>.</param>
		/// <param name="j">The index of the Column such that 0=< j < NumberOfColumns .</param>
		public void DefineAdjacents (int i, int j)
		{
				if (adjacents == null) {
						adjacents = new List<int> ();
				}

				AddAdjacent (new Vector2 (i, j + 1));//Right Adjacent
				AddAdjacent (new Vector2 (i, j - 1));//Left Adjacent
				AddAdjacent (new Vector2 (i - 1, j));//Upper Adjacent
				AddAdjacent (new Vector2 (i + 1, j));//Lower Adjacent
		}

		/// <summary>
		/// Adds the adjacent index (GridCell index) to the Adjacents List.
		/// </summary>
		/// <param name="adjacent">Adjacent vector (i,j).</param>
		private void AddAdjacent (Vector2 adjacent)
		{
				if ((adjacent.x >= 0 && adjacent.x < GameManager.numberOfRows) && (adjacent.y >= 0 && adjacent.y < GameManager.numberOfColumns)) {
						adjacents.Add ((int)(adjacent.x * GameManager.numberOfColumns + adjacent.y));//Convert from (i,j) to Array index
				}
		}

		/// <summary>
		/// Check if the given adjacent index is one of the Adjacents or Not.
		/// </summary>
		/// <param name="adjacentIndex">an Adjacent index.</param>
		public bool OneOfAdjacents (int adjacentIndex)
		{
				if (adjacents == null) {
						return false;
				}

				if (adjacents.Contains (adjacentIndex)) {
						return true;
				}
				return false;
		}

		/// <summary>
		/// Reset Attributes
		/// </summary>
		public void Reset ()
		{
				currentlyUsed = false;
				if (isEmpty) {
						tragetIndex = -1;
						gridLineIndex = -1;
				}
		}
}