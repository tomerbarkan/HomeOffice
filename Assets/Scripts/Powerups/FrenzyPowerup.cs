using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrenzyPowerup : Powerup
{
	public float activeTime;
	public float cooldown;

	public override void ActivatePowerup(Character activator) {
		activator.SetOverrideCooldown(cooldown);
		activator.StartCoroutine(DelayCoroutine(activator, activeTime));
	}

	private IEnumerator DelayCoroutine(Character activator, float delay) {
		yield return new WaitForSeconds(delay);
		activator.CancelOverrideCooldown();
	}
}
