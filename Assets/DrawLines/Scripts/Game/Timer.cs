using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 自己实现的定时器类，使用协程实现
 * */
[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
	//显示倒计时的text控件
	public Text uiText;

	public static int timeInSeconds;

	//Whether the Timer is running
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
			ApplyTime ();	//更新ui
			yield return new WaitForSeconds (1);	//每秒执行一次
		}
	}

	/// <summary>
	/// 更新定时器ui
	/// </summary>
	private void ApplyTime ()
	{
		if (uiText == null) {
			return;
		}
		uiText.text = "Time : " + timeInSeconds;
	}
}