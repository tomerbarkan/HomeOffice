﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupCollectible : MonoBehaviour, IHitable
{
	[SerializeField] protected Powerup powerup;

    public void Hit(Throwable throwable) {
		throwable.thrower.AddPowerup(powerup);
		Destroy(gameObject);
    }
}
