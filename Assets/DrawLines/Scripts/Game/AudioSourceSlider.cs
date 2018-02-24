using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioSourceSlider : MonoBehaviour
{
		/// <summary>
		/// The name of the audio source holder.
		/// </summary>
		public string audioSourceHolderName;
		[Range(0,1)]
		/// <summary>
		/// The index of the audio source.
		/// </summary>
		public int audioSourceIndex;

		// Use this for initialization
		void Start ()
		{
				if (string.IsNullOrEmpty (audioSourceHolderName)) {
						return;
				}

				///Find the audio source holder
				GameObject auidoSourceHolder = GameObject.Find (audioSourceHolderName);
				if (auidoSourceHolder != null) {
						//Get the slider reference
						Slider slider = GetComponent<Slider> ();
						if (slider != null) {
								AudioSource [] audioSources = auidoSourceHolder.GetComponents<AudioSource> ();
								///Apply the volume of the audio source on the slider
								if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length) {
										slider.value = audioSources [audioSourceIndex].volume;
								}
						}
				} else {
					Debug.Log("AudioSources holder is not found");
				}
		}
}