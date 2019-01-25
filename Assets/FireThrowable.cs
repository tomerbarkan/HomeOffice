using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireThrowable : BasicThrowable
{
    public float timeToSetOnFire = 5;
    public override void OnHitCharacter(Character hit)
    {
        base.OnHitCharacter(hit);
        hit.SetOnFire(5);
    }
}
