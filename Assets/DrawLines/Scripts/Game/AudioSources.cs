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

			//当加载一个新关卡时，所有场景中所有的物体被销毁，然后新关卡中的物体被加载进来。为了保持在加载新关卡时物体不被销毁，使用DontDestroyOnLoad保持，如果物体是一个组件或游戏物体，它的整个transform层次将不会被销毁，全部保留下来。
			//加载新场景的时候使目标物体不被自动销毁
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
