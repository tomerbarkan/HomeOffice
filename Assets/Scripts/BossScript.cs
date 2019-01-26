using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour 
{
	public static BossScript instance;

	public Animator bossAnimator;

	private void Awake() {
		instance = this;
	}

	public void Activate() {
		bossAnimator.SetTrigger("CallBoss");
	}
}
