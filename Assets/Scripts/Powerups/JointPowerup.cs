using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JointPowerup : Powerup
{
	public float activeTime;
	public float healAmount;

	public override void ActivatePowerup(Character activator) {
		activator.StartCoroutine(DelayCoroutine(activator, activeTime));
	}

	private IEnumerator DelayCoroutine(Character activator, float delay) {
		activator.SetSingleCooldown(delay);

        activator.canGetHit = false;
        activator.canShoot = false;

        activator.StartSmokeWeedAnimation();
        AudioManager.Instance.PlayJointMusic();

		while (delay > 0) {
			activator.Heal(Time.deltaTime / activeTime * healAmount);
			delay -= Time.deltaTime;
			yield return null;
		}

        activator.canGetHit = true;
        activator.canShoot = true;
    }
}
