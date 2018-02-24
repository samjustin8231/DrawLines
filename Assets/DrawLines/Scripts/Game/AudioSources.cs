using UnityEngine;
using System.Collections;

public class AudioSources : MonoBehaviour {

	/// <summary>
	/// The loading canvas instance.
	/// </summary>
	private static AudioSources audioSourcesInstance;
	
	// Use this for initialization
	void Awake ()
	{
		if (audioSourcesInstance == null) {
			audioSourcesInstance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
