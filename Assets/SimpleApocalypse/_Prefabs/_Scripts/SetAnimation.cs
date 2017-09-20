using UnityEngine;
using System.Collections;

public class SetAnimation : MonoBehaviour {

	[SerializeField]
	private Animator animation;

	void Start()
	{
		animation.SetBool ("Static_b", true);
	}

	void Update()
	{
		animation.SetFloat ("Speed_f", 1);
	}
}
