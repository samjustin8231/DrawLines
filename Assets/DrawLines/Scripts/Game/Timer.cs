using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
		/// <summary>
		/// Text Component
		/// </summary>
		public Text uiText;

		/// <summary>
		/// The time in seconds.
		/// </summary>
		public static int timeInSeconds;

		/// <summary>
		/// Whether the Timer is running
		/// </summary>
		private bool isRunning;

		void Awake ()
		{
				if (uiText == null) {
					uiText = GetComponent<Text> ();
				}
				///Start the Timer
				Start ();
		}

		/// <summary>
		/// Start the Timer.
		/// </summary>
		public void Start ()
		{
				if (!isRunning) {
						isRunning = true;
						timeInSeconds = 0;
						StartCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Stop the Timer.
		/// </summary>
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Wait Coroutine.
		/// </summary>
		private IEnumerator Wait ()
		{
				while (isRunning) {
						timeInSeconds++;
						ApplyTime ();
						yield return new WaitForSeconds (1);
				}
		}

		/// <summary>
		/// Applies the time into TextMesh Component.
		/// </summary>
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				uiText.text = "Time : " + timeInSeconds;
		}
}