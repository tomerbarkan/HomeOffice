﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Powerup : ScriptableObject
{
	[SerializeField] protected Sprite iconSprite;

    public virtual Sprite Sprite { get { return iconSprite; } }

	public abstract void ActivatePowerup(Character activator);
}
