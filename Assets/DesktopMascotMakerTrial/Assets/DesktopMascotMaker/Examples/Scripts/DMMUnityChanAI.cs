using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using DesktopMascotMaker;
using UnityEngine;

[RequireComponent (typeof (Animation))]
[RequireComponent (typeof (AudioSource))]
public class DMMUnityChanAI : MonoBehaviour
{
	// If you want to use this script with MascotMakerMulti,
	// Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
	//public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

	public enum UnityChanState
	{
		Idle,
		WalkToRight,
		WalkToLeft,
		Action,
	}

	public UnityChanState state;

	public Transform mainCamera;

	// For Walk
	public int walkSpeed = 1;

	// For Action
	private float actionTimer = 0;
	private AnimationClip animClip;
	private Animation anim;
	private AudioClip voice;
	private AudioSource audioSource;
	public List<AnimationClip> actionAnim;
	public List<AudioClip> actionVoice;

	void Start ()
	{
		if (mainCamera == null)
			Debug.LogError ("mainCamera is not assigned!", transform);

		anim = GetComponent<Animation> ();
		audioSource = GetComponent<AudioSource> ();

		actionTimer = 0;

		//		MascotMaker.Instance.OnLeftDoubleClick += DoAction;
	}

	void Update ()
	{
		switch (state)
		{
			case UnityChanState.Idle:
				anim.CrossFade ("WAIT02", 0.5f);
				break;
			case UnityChanState.WalkToLeft:
				transform.LookAt (mainCamera.right * 100 + mainCamera.forward * -80);
				MascotMaker.Instance.Left += walkSpeed;

				// Get center position of desktop mascot's form
				float leftPos = MascotMaker.Instance.Left + MascotMaker.Instance.Width / 2;

				if (leftPos > MascotMaker.Instance.ScreenWidth)
					state = UnityChanState.WalkToRight;

				anim.CrossFade ("WALK00_F", 0.5f);
				break;
			case UnityChanState.WalkToRight:
				transform.LookAt (mainCamera.right * -100 + mainCamera.forward * -80);
				MascotMaker.Instance.Left -= walkSpeed;

				// Get center position of desktop mascot's form
				float rightPos = MascotMaker.Instance.Left + MascotMaker.Instance.Width / 2;

				if (rightPos < 0)
					state = UnityChanState.WalkToLeft;

				anim.CrossFade ("WALK00_F", 0.5f);
				break;
			case UnityChanState.Action:
				actionTimer -= Time.deltaTime;

				if (actionTimer < 0)
					actionTimer = 0;

				if (actionTimer != 0)
				{
					anim.CrossFade (animClip.name, 0.5f);
				}
				else
				{
					state = UnityChanState.Idle;
				}
				break;
		}
	}

	void DoAction (object sender, MouseEventArgs e)
	{
		if (actionAnim != null && actionVoice != null)
		{
			if (actionAnim.Count == actionVoice.Count)
			{
				if (actionAnim.Count != 0)
				{
					state = UnityChanState.Action;

					int index = Random.Range (0, actionAnim.Count);

					animClip = actionAnim[index];
					voice = actionVoice[index];

					actionTimer = animClip.length - 0.5f;

					if (voice != null)
						audioSource.PlayOneShot (voice);
				}
			}
		}
	}

	public void DoWalk ()
	{
		int randomValue = Random.Range (0, 100);

		if (randomValue > 50)
			state = UnityChanState.WalkToRight;
		else
			state = UnityChanState.WalkToLeft;
	}

	public void DoIdle ()
	{
		state = UnityChanState.Idle;
	}
}