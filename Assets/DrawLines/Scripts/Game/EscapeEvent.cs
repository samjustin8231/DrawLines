using UnityEngine;
using System.Collections;

public class EscapeEvent : MonoBehaviour
{
		/// <summary>
		/// The name of the scene to be loaded.
		/// </summary>
		public string sceneName;

		/// <summary>
		/// Whether to leave the application on escape click.
		/// </summary>
		public bool leaveTheApplication;

		/// <summary>
		/// The async operation instance.
		/// </summary>
		private AsyncOperation async;

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						OnClick ();
				}
		}

		/// <summary>
		/// On Escape click event.
		/// </summary>
		public void OnClick ()
		{

				if (leaveTheApplication) {
						GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
				} else {
						if (!string.IsNullOrEmpty (sceneName)) {
								StartCoroutine ("LoadSceneAsync");
						}
				}
		}

		/// <summary>
		/// Loads the scene Async.
		/// </summary>
		IEnumerator LoadSceneAsync ()
		{
				if (!string.IsNullOrEmpty (sceneName)) {
						async = Application.LoadLevelAsync (sceneName);
						while (!async.isDone) {
								yield return 0;
						}
				}
		}
}
