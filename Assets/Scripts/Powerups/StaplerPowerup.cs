using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaplerPowerup : Powerup
{
	public Throwable throwablePrefab;
	public float activeTime;

	public override void ActivatePowerup(Character activator) {
		activator.SetThrowable(throwablePrefab);
		activator.StartCoroutine(DelayCoroutine(activator, activeTime));
	}

	private IEnumerator DelayCoroutine(Character activator, float delay) {
		yield return new WaitForSeconds(delay);
		if (activator.ActiveThrowable == throwablePrefab) {
			activator.SetThrowable(null);
		}
	}
}
