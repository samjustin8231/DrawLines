using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
	private AsyncOperation async;
	
	/* ChangeMusicLevel和ChangeEffectsLevel通过拖拽赋值的方式与slide控件滑动过程绑定了 */
	//bg music音量
	public void ChangeMusicLevel (Slider slider)
	{
		if (slider == null) {
			return;
		}
		//直接利用当前的值设置AudioSources的音量
		GameObject.Find ("AudioSources").GetComponents<AudioSource> () [0].volume = slider.value;
	}

	//音效的音量
	public void ChangeEffectsLevel (Slider slider)
	{
		if (slider == null) {
			return;
		}
		GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1].volume = slider.value;
	}

	//显示reset对话框
	public void ShowResetGameConfirmDialog ()
	{
		GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
	}

	//exit dialog
	public void ShowExitConfirmDialog ()
	{
		GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
	}

	/// <summary>
	/// reset对话框中的button点击事件，只需要在拖拽的时候传递YesButton对象
	/// </summary>
	/// <param name="value">Value,拖拽的时候传递YesButton对象</param>
	public void ResetGameConfirmDialogEvent (GameObject value)
	{
		if (value == null) {
			return;
		}

		if (value.name.Equals ("YesButton")) {
			Debug.Log ("Reset Game Confirm Dialog : No button clicked");
			//重置游戏
			DataManager.ResetData ();
		} else if (value.name.Equals ("NoButton")) {
			Debug.Log ("Reset Game Confirm Dialog : No button clicked");
		}
		GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
	}

	//exit对话框中的button点击事件
	public void ExitConfirmDialogEvent (GameObject value)
	{
		if (value.name.Equals ("YesButton")) {
			Debug.Log ("Exit Confirm Dialog : Yes button clicked");
			//退出游戏
			Application.Quit ();
		} else if (value.name.Equals ("NoButton")) {
			Debug.Log ("Exit Confirm Dialog : No button clicked");
		}
		GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
	}

	//点击mission item
	public	void MissionButtonEvent (Object value)
	{
		if (value == null) {
			Debug.Log ("Event parameter value is undefined");
			return;
		}

		GameObject missionGameObject = (GameObject)value;
		Mission.wantedMission = missionGameObject.GetComponent<Mission> ();

		//进入levels界面
		LoadLevelsScene ();
	}

	//点击level item
	public void LevelButtonEvent (Object value)
	{
		if (value == null) {
			Debug.Log ("Event parameter value is undefined");
			return;
		}

		GameObject levelGameObject = (GameObject)value;
		TableLevel.wantedLevel = levelGameObject.GetComponent<TableLevel> ();
		LevelsTable.currentLevelID = TableLevel.wantedLevel.ID;

		//进入游戏主界面
		LoadGameScene ();
	}

	//后一关
	public	void GameNextButtonEvent ()
	{
		GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
	}

	//前一关
	public void GameBackButtonEvent ()
	{
		GameObject.Find ("GameScene").GetComponent<GameManager> ().PreviousLevel ();
	}

	//重置该关
	public void GameRefreshButtonEvent ()
	{
		GameObject.Find ("GameScene").GetComponent<GameManager> ().RefreshGrid ();
	}

	//结算界面next button
	public void AwesomeDialogNextButtonEvent ()
	{
		if (LevelsTable.currentLevelID == LevelsTable.tableLevels.Count) {	//最后一关，点击后返回到levels界面
			LoadLevelsScene ();
			return;
		}
		BlackArea.Hide ();
		GameObject.FindObjectOfType<AwesomeDialog> ().Hide ();
		GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
	}

	//进入游戏菜单页面
	public void LoadMainScene ()
	{
		//以协程的方式加载场景
		StartCoroutine (LoadSceneAsync ("Main"));
	}

	//how to play
	public void LoadHowToPlayScene ()
	{
		StartCoroutine (LoadSceneAsync ("HowToPlay"));
	}

	//进入missions页面
	public void LoadMissionsScene ()
	{
		StartCoroutine (LoadSceneAsync ("Missions"));
	}

	//进入设置页面
	public void LoadOptionsScene ()
	{
		StartCoroutine (LoadSceneAsync ("Options"));
	}

	//进入levels页面
	public void LoadLevelsScene ()
	{
		StartCoroutine (LoadSceneAsync ("Levels"));
	}

	//进入游戏主页面
	public void LoadGameScene ()
	{
		StartCoroutine (LoadSceneAsync ("Game"));
	}

	IEnumerator LoadSceneAsync (string sceneName)
	{
		if (!string.IsNullOrEmpty (sceneName)) {
			//async = Application.LoadLevelAsync (sceneName);
			async = SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Single);
			while (!async.isDone) {
				yield return 0;
			}
		}
	}
}