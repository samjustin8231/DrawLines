using UnityEngine;
using System.Collections;

//游戏首页
public class Logo : MonoBehaviour {

	public float sleepTime = 5;
	// Use this for initialization
	void Start () {
		//5s之后跳转到LoadMainScene页面
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		Application.LoadLevel ("Main");
	}
	
}
