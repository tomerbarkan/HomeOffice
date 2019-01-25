using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitThrowable : BasicThrowable
{

    public Throwable additionalThrowablePrefab;
    public int additioalThrowables;

    public override void Throw(Vector2 force, Character thrower)
    {
        base.Throw(force, thrower);

        Throwable[] instadThrowables = new Throwable[additioalThrowables];

        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(additionalThrowablePrefab);
        spawnedThrowable.transform.position = transform.position + (Vector3.up * 0.5f);
        spawnedThrowable.transform.forward = force;
        spawnedThrowable.throwableRigidbody.velocity = force;

        spawnedThrowable = Throwable.Instantiate<Throwable>(additionalThrowablePrefab);
        spawnedThrowable.transform.position = transform.position + (Vector3.down * 0.5f);
        spawnedThrowable.transform.forward = force;
        spawnedThrowable.throwableRigidbody.velocity = force;

    }
}
