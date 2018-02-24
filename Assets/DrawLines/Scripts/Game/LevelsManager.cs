using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
		[SerializeField]
		public Sprite defaultSprite;
		public bool createRandomColor = true;
		public readonly static int rowsLimit = 12;
		public readonly static int colsLimit = 12;
		public int numberOfCols = 5;
		public int numberOfRows = 5;
		public List<Level> levels = new List<Level> ();
}