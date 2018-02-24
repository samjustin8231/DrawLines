using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Destroy : MonoBehaviour
{
		/// <summary>
		/// Destroy time.
		/// </summary>
		public float time;

		// Use this for initialization
		void Start ()
		{
				///Destry the current gameobject
				Destroy (gameObject, time);
		}
}
