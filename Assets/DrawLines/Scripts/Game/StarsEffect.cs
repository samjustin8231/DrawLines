using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class StarsEffect : MonoBehaviour
{
		/// <summary>
		/// The position of Stars Effect in the World Space.
		/// </summary>
		private Vector3 tempPosition;

		/// <summary>
		/// The stars effect prefab.
		/// </summary>
		public GameObject starsEffectPrefab;

		/// <summary>
		/// The star effect Z position.
		/// </summary>
		[Range(-50,50)]
		public float starEffectZPosition = -5;

		/// <summary>
		/// Create the stars effect.
		/// </summary>
		public void CreateStarsEffect ()
		{
				tempPosition = transform.position;
				tempPosition.z = starEffectZPosition;
				GameObject starsEffect = Instantiate (starsEffectPrefab, tempPosition, Quaternion.identity) as GameObject;
				starsEffect.transform.parent = transform;//setting up Stars Effect Parent
		}
}