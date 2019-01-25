using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaplerPowerup : Powerup
{
	public Throwable throwablePrefab;

	public override void ActivatePowerup(Character activator) {
		activator.SetThrowable(throwablePrefab);
	}
}
