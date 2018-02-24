using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	public float sleepTime = 5;
	// Use this for initialization
	void Start () {
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		Application.LoadLevel ("Main");
	}
	
}
