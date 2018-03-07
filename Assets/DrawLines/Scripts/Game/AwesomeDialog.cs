using UnityEngine;
using System.Collections;

/**
 * 过关dialog
 * */
[DisallowMultipleComponent]
public class AwesomeDialog : MonoBehaviour
{
	private StarsNumber starsNumber;

	public AudioClip starSoundEffect;

	public Animator awesomeDialogAnimator;

	// First star fading animator.
	public Animator firstStarFading;
	// Second star fading animator.
	public Animator secondStarFading;
	//Third star fading animator.
	public Animator thirdStarFading;

	// Use this for initialization
	void Awake ()
	{
		///Setting up the references
		if (awesomeDialogAnimator == null) {
			awesomeDialogAnimator = GetComponent<Animator> ();
		}

		if (firstStarFading == null) {
			firstStarFading = GameObject.Find ("FirstStarFading").GetComponent<Animator> ();
		}

		if (secondStarFading == null) {
			secondStarFading = GameObject.Find ("SecondStarFading").GetComponent<Animator> ();
		}

		if (thirdStarFading == null) {
			thirdStarFading = GameObject.Find ("ThirdStarFading").GetComponent<Animator> ();
		}
	}

	/// <summary>
	/// When the GameObject becomes visible
	/// </summary>
	void OnEnable ()
	{
		///Hide the Awesome Dialog
		Hide ();
	}

	/// <summary>
	/// Show the Awesome Dialog.
	/// 显示dialog的动画
	/// </summary>
	public void Show ()
	{
		if (awesomeDialogAnimator == null) {
			return;
		}
		awesomeDialogAnimator.SetTrigger ("Running");
	}

	//恢复动画
	public void Hide ()
	{
		StopAllCoroutines ();
		awesomeDialogAnimator.SetBool ("Running", false);

		//三颗start的动画
		firstStarFading.SetBool ("Running", false);
		secondStarFading.SetBool ("Running", false);
		thirdStarFading.SetBool ("Running", false);
	}

	/// <summary>
	/// Fade stars Coroutine.
	/// </summary>
	/// <returns>The stars.</returns>
	public IEnumerator FadeStars ()
	{
		starsNumber = StarsRating.GetAwesomeDialogStarsRating (Timer.timeInSeconds, GameManager.movements, Mission.wantedMission.rowsNumber * Mission.wantedMission.colsNumber);

		float delayBetweenStars = 0.5f;
		if (starsNumber == StarsNumber.ONE) {//Fade with One Star
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			firstStarFading.SetTrigger ("Running");
		} else if (starsNumber == StarsNumber.TWO) {//Fade with Two Star
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			firstStarFading.SetTrigger ("Running");
			yield return new WaitForSeconds (delayBetweenStars);
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			secondStarFading.SetTrigger ("Running");
		} else if (starsNumber == StarsNumber.THREE) {//Fade with Three Star
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			firstStarFading.SetTrigger ("Running");
			yield return new WaitForSeconds (delayBetweenStars);
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			secondStarFading.SetTrigger ("Running");
			yield return new WaitForSeconds (delayBetweenStars);
			AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
			thirdStarFading.SetTrigger ("Running");
		}
		yield return 0;
	}

	public enum StarsNumber
	{
		ONE,
		TWO,
		THREE
	}
}