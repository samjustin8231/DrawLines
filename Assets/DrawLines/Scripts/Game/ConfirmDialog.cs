using UnityEngine;
using System.Collections;

public class ConfirmDialog : MonoBehaviour
{
		public Animator animator;
		public Animator blackAreaAnimator;

		void Start ()
		{
				if (animator == null) {
						animator = GetComponent<Animator> ();
				}

				if (blackAreaAnimator == null) {
						blackAreaAnimator = GameObject.Find ("BlackArea").GetComponent<Animator> ();
				}
		}

		public void Show ()
		{
				blackAreaAnimator.SetTrigger ("Running");
				animator.SetBool ("Off", false);
				animator.SetTrigger ("On");
		}

		public void Hide ()
		{
				blackAreaAnimator.SetBool ("Running", false);
				animator.SetBool ("On", false);
				animator.SetTrigger ("Off");
		}

		private void ResetAnimationParameters ()
		{
				if (animator == null) {
						return;
				}
				animator.SetBool ("On", false);
				animator.SetBool ("Off", false);
		}
}