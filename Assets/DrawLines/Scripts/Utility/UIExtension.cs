using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIExtension : MonoBehaviour
{
		/// <summary>
		/// Set the size for the UI element.
		/// </summary>
		/// <param name="trans">The Rect transform referenced.</param>
		/// <param name="newSize">The New size.</param>
		public static void SetSize (RectTransform trans, Vector2 newSize)
		{
				Vector2 oldSize = trans.rect.size;
				Vector2 deltaSize = newSize - oldSize;
				trans.offsetMin = trans.offsetMin - new Vector2 (deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
				trans.offsetMax = trans.offsetMax + new Vector2 (deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
		}
}