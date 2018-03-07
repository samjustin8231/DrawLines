using UnityEngine;
using System.Collections;

/**
 * 实现弹出框的背面阴暗层
 * */
//对一个MonoBehaviour的子类使用这个属性，那么在同一个GameObject上面，最多只能添加一个该Class的实例。尝试添加多个的时候，会出现下面的提示。
[DisallowMultipleComponent]
public class BlackArea : MonoBehaviour
{
	private static Animator blackAreaAnimator;

	// Use this for initialization
	void Awake ()
	{
		///Setting up the references
		if (blackAreaAnimator == null) {
			blackAreaAnimator = GetComponent<Animator> ();
		}
	}

	/// <summary>
	/// When the GameObject becomes visible
	/// </summary>
	void OnEnable ()
	{
		///Hide the Black Area
		Hide ();
	}

	///Show the Black Area
	public static void Show ()
	{
		if (blackAreaAnimator == null) {
			return;
		}
		blackAreaAnimator.SetTrigger ("Running");
	}

	///Hide the Black Area
	public static void Hide ()
	{
		if (blackAreaAnimator != null)
			blackAreaAnimator.SetBool ("Running", false);
	}
}