using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 该类负责音效和背景音乐的slider的显示
 * slider的拖拽改变音量在UIEvent中
 * */
public class AudioSourceSlider : MonoBehaviour
{
	/// <summary>
	/// The name of the audio source holder.
	/// </summary>
	public string audioSourceHolderName;
	//scene中的AudioSource物体名称
	[Range (0, 1)]
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
		GameObject auidoSourceHolder = GameObject.Find (audioSourceHolderName);	//scenez中的AudioSource物体
		if (auidoSourceHolder != null) {
			//Get the slider reference
			Slider slider = GetComponent<Slider> ();
			if (slider != null) {
				AudioSource[] audioSources = auidoSourceHolder.GetComponents<AudioSource> ();
				///Apply the volume of the audio source on the slider
				if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length) {
					//根据当前音量的值更新slide ui
					slider.value = audioSources [audioSourceIndex].volume;
				}
			}
		} else {
			Debug.Log ("AudioSources holder is not found");
		}
	}
}